using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using HangfireDemo.Domains;
using HangfireDemo.Logic.Logics;
using HangfireDemo.Logic.Repositories;
using HangfireDemo.Logic.Services.HtmlService;

namespace HangfireDemo.Logic.ParserJobs
{
    public class ParserJobsLogic : BaseLogic, IParserJobsLogic
    {
        private Lazy<IParserJobsRepository> _parserJobsRepository;

        protected IParserJobsRepository ParserJobsRepository
        {
            get { return _parserJobsRepository.Value; }
        }

        private Lazy<IBackgroundJobClient> _backgroundJobClient;

        protected IBackgroundJobClient BackgroundJobClient
        {
            get { return _backgroundJobClient.Value; }
        }

        private Lazy<IHtmlService> _htmlService;

        protected IHtmlService HtmlService
        {
            get { return _htmlService.Value; }
        }

        private Lazy<IBatchJobClient> _batchJobClient;

        protected IBatchJobClient BatchJobClient
        {
            get { return _batchJobClient.Value; }
        }

        public ParserJobsLogic(Lazy<IParserJobsRepository> parserJobsRepository,
               Lazy<IBackgroundJobClient> backgroundJobClient,
               Lazy<IHtmlService> htmlService,
                Lazy<IBatchJobClient> batchJobClient)
        {
            _parserJobsRepository = parserJobsRepository;
            _backgroundJobClient = backgroundJobClient;
            _htmlService = htmlService;
            _batchJobClient = batchJobClient;
        }

        public IEnumerable<ParserJob> GetAll()
        {
            return ParserJobsRepository.GetAll();
        }

        public void Add(ParserJob job)
        {
            job.Status = ParserJobStatus.Pending;
            ParserJobsRepository.Add(job);
            ParserJobsRepository.SaveChanges();

            if (job.IsCritical)
            {
                BackgroundJobClient.Enqueue(() => ParseCriticalUrl(job.Id));
            }
            else
            {
                BackgroundJobClient.Enqueue(() => ParseUrl(job.Id));
            }
        }

        [Queue(JobsQueues.Default)]
        public void ParseUrl(int id)
        {
            var job = ParserJobsRepository.GetById(id);

            var document = HtmlService.Load(job.Url);

            job.Count = HtmlService.CountText(document, job.Pattern);
            job.Status = ParserJobStatus.Processing;

            ParserJobsRepository.SaveChanges();

            var childUrls = HtmlService.GetUrls(document);

            var jobBatchId = StartBatchJobs(id, childUrls, job.IsCritical, job.Pattern, 1, job.Depth);

            if (job.Depth == 1)
            {
                BatchJobClient.AwaitBatch(jobBatchId, x =>
                {
                    if (job.IsCritical)
                    {
                        x.Enqueue(() => SetCriticalJobStatus(job.Id, ParserJobStatus.Completed));
                    }
                    else
                    {
                        x.Enqueue(() => SetJobStatus(job.Id, ParserJobStatus.Completed));
                    }
                });
            }
        }

        private string StartBatchJobs(int id, IEnumerable<string> childUrls, bool isCritical, string pattern, int depth, int maxDepth)
        {
            return BatchJobClient.StartNew(x =>
            {
                foreach (var url in childUrls)
                {
                    if (isCritical)
                    {
                        x.Enqueue(() => ParseCriticalChildUrl(id, url, isCritical, pattern, depth, maxDepth));
                    }
                    else
                    {
                        x.Enqueue(() => ParseChildUrl(id, url, isCritical, pattern, depth, maxDepth));
                    }
                }
            });
        }

        [Queue(JobsQueues.Critical)]
        public void ParseCriticalUrl(int id)
        {
            ParseUrl(id);
        }

        [Queue(JobsQueues.Default)]
        public void ParseChildUrl(int id, string url, bool isCritical, string pattern, int depth, int maxDepth)
        {
            var job = ParserJobsRepository.GetById(id);

            if (maxDepth <= depth)
            {
                return;
            }

            var document = HtmlService.Load(url);

            var count = HtmlService.CountText(document, pattern);

            ParserJobsRepository.UpdateCount(job, count);

            var childUrls = HtmlService.GetUrls(document);

            var jobBatchId = StartBatchJobs(id, childUrls, isCritical, pattern, depth + 1, maxDepth);

            if (depth + 1 > maxDepth)
            {
                BatchJobClient.AwaitBatch(jobBatchId, x =>
                {
                    if (isCritical)
                    {
                        x.Enqueue(() => SetCriticalJobStatus(id, ParserJobStatus.Completed));
                    }
                    else
                    {
                        x.Enqueue(() => SetJobStatus(id, ParserJobStatus.Completed));
                    }
                });
            }
        }

        [Queue(JobsQueues.Critical)]
        public void ParseCriticalChildUrl(int id, string url, bool isCritical, string pattern, int depth, int maxDepth)
        {
            ParseChildUrl(id, url, isCritical, pattern, depth, maxDepth);
        }

        [Queue(JobsQueues.Critical)]
        public void SetJobStatus(int id, ParserJobStatus status)
        {
            var job = ParserJobsRepository.GetById(id);

            job.Status = status;

            ParserJobsRepository.SaveChanges();
        }

        [Queue(JobsQueues.Default)]
        public void SetCriticalJobStatus(int id, ParserJobStatus status)
        {
            SetJobStatus(id, status);
        }
    }
}
