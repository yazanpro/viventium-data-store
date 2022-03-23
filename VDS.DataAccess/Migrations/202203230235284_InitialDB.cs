namespace VDS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyCode = c.String(maxLength: 255),
                        CompanyDescription = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeNumber = c.String(maxLength: 15),
                        EmployeeFirstName = c.String(maxLength: 255),
                        EmployeeLastName = c.String(maxLength: 255),
                        EmployeeEmail = c.String(maxLength: 320),
                        EmployeeDepartment = c.String(maxLength: 255),
                        HireDate = c.DateTime(),
                        ManagerEmployeeNumber = c.String(maxLength: 15),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
        }
    }
}
