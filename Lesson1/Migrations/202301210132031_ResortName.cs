namespace Lesson1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResortName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ResortName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ResortName");
        }
    }
}
