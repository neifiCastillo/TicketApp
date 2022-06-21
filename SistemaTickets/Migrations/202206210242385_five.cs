namespace SistemaTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class five : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "NombreEmpleado", c => c.String());
            DropColumn("dbo.Tickets", "IdEmpleado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "IdEmpleado", c => c.Int(nullable: false));
            DropColumn("dbo.Tickets", "NombreEmpleado");
        }
    }
}
