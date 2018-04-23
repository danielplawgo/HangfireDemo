using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HangfireDemo.Domains;
using HangfireDemo.Logic.Logics;
using HangfireDemo.Web.ViewModels.ParserJobs;

namespace HangfireDemo.Web.Controllers
{
    public class ParserJobsController : Controller
    {
        private Lazy<IParserJobsLogic> _parserJobsLogic;

        protected IParserJobsLogic ParserJobsLogic
        {
            get { return _parserJobsLogic.Value; }
        }

        private Lazy<IMapper> _mapper;

        protected IMapper Mapper
        {
            get { return _mapper.Value; }
        }

        public ParserJobsController(Lazy<IParserJobsLogic> parserJobsLogic,
               Lazy<IMapper> mapper)
        {
            _parserJobsLogic = parserJobsLogic;
            _mapper = mapper;
        }

        // GET: ParserJobs
        public ActionResult Index()
        {
            var viewModel = new IndexViewModel();

            viewModel.Items = Mapper.Map<List<IndexItemViewModel>>(ParserJobsLogic.GetAll());

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new ParserJobViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(ParserJobViewModel viewModel)
        {
            var job = Mapper.Map<ParserJob>(viewModel);

            ParserJobsLogic.Add(job);

            return RedirectToAction("Index");
        }
    }
}