namespace Lesson1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resortprice : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Prices", "ResortId");
            AddForeignKey("dbo.Prices", "ResortId", "dbo.Resorts", "ResortId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prices", "ResortId", "dbo.Resorts");
            DropIndex("dbo.Prices", new[] { "ResortId" });
        }
    }
}
