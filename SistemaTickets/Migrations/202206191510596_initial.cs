namespace SistemaTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empleadoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cedula = c.String(nullable: false, maxLength: 11),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        Estatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sistemas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        Estatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descripcion = c.String(),
                        FechaVencimiento = c.DateTime(nullable: false),
                        Prioridad = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        Estatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreUsuario = c.String(),
                        PasswordSalt = c.Binary(),
                        PasswordHash = c.Binary(),
                        Rol = c.Int(nullable: false),
                        SistemaId = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(nullable: false),
                        Estatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sistemas", t => t.SistemaId, cascadeDelete: true)
                .Index(t => t.SistemaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "SistemaId", "dbo.Sistemas");
            DropIndex("dbo.Usuarios", new[] { "SistemaId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Tickets");
            DropTable("dbo.Sistemas");
            DropTable("dbo.Empleadoes");
        }
    }
}
