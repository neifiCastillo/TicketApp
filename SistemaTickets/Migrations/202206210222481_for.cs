namespace SistemaTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _for : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empleadoes", "NombreCompleto", c => c.String());
            DropColumn("dbo.Empleadoes", "Nombre");
            DropColumn("dbo.Empleadoes", "Apellido");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Empleadoes", "Apellido", c => c.String());
            AddColumn("dbo.Empleadoes", "Nombre", c => c.String());
            DropColumn("dbo.Empleadoes", "NombreCompleto");
        }
    }
}
