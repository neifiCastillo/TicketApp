namespace SistemaTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "IdEmpleado", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "Empleado_Id", c => c.Int());
            CreateIndex("dbo.Tickets", "Empleado_Id");
            AddForeignKey("dbo.Tickets", "Empleado_Id", "dbo.Empleadoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "Empleado_Id", "dbo.Empleadoes");
            DropIndex("dbo.Tickets", new[] { "Empleado_Id" });
            DropColumn("dbo.Tickets", "Empleado_Id");
            DropColumn("dbo.Tickets", "IdEmpleado");
        }
    }
}
