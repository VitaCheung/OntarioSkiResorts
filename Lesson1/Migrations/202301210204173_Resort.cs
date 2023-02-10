namespace Lesson1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resort : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resorts",
                c => new
                    {
                        ResortId = c.Int(nullable: false, identity: true),
                        ResortName = c.String(),
                        address = c.String(),
                        description = c.String(),
                        NumberOfTrailsOpen = c.String(),
                        Contact = c.Int(nullable: false),
                        WheelchairAccessible = c.Boolean(nullable: false),
                        NumberOfRestaurants = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResortId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Resorts");
        }
    }
}
