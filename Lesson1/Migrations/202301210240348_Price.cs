namespace Lesson1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Price : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        ResortId = c.Int(nullable: false),
                        DAY1Hour = c.Decimal(nullable: true, precision: 18, scale: 2),
                        DAY2Hours = c.Decimal(nullable: true, precision: 18, scale: 2),
                        DAY3Hours = c.Decimal(nullable: true, precision: 18, scale: 2),
                        DAY4Hours = c.Decimal(nullable: true, precision: 18, scale: 2),
                        NIGHT1Hour = c.Decimal(nullable: true, precision: 18, scale: 2),
                        NIGHT2Hours = c.Decimal(nullable: true, precision: 18, scale: 2),
                        NIGHT3Hours = c.Decimal(nullable: true, precision: 18, scale: 2),
                        NIGHT4Hours = c.Decimal(nullable: true, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PriceId);
            
            AlterColumn("dbo.Resorts", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resorts", "Contact", c => c.Int(nullable: false));
            DropTable("dbo.Prices");
        }
    }
}
