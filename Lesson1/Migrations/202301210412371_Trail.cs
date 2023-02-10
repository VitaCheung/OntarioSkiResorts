namespace Lesson1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trails",
                c => new
                    {
                        TrailId = c.Int(nullable: false, identity: true),
                        ResortId = c.Int(nullable: false),
                        BeginnerTrails = c.Int(nullable: false),
                        IntermediateTrails = c.Int(nullable: false),
                        AdvancedTrails = c.Int(nullable: false),
                        TerrainPark = c.Int(nullable: false),
                        TubingPark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrailId)
                .ForeignKey("dbo.Resorts", t => t.ResortId, cascadeDelete: true)
                .Index(t => t.ResortId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trails", "ResortId", "dbo.Resorts");
            DropIndex("dbo.Trails", new[] { "ResortId" });
            DropTable("dbo.Trails");
        }
    }
}
