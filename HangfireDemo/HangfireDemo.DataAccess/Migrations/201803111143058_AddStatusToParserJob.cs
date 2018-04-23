namespace HangfireDemo.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToParserJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParserJobs", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParserJobs", "Status");
        }
    }
}
