namespace HangfireDemo.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParserJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Pattern = c.String(),
                        Count = c.Int(nullable: false),
                        IsCritical = c.Boolean(nullable: false),
                        ScheduleType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParserJobs");
        }
    }
}
