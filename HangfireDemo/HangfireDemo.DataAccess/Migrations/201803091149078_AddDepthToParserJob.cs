namespace HangfireDemo.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepthToParserJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParserJobs", "Depth", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParserJobs", "Depth");
        }
    }
}
