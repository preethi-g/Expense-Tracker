namespace ExpenseTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseReports",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpenseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.Incomes",
                c => new
                    {
                        S_no = c.Int(nullable: false, identity: true),
                        IncomeMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IncomeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.S_no);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Incomes");
            DropTable("dbo.ExpenseReports");
        }
    }
}
