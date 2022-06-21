namespace SistemaTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuarios", "SistemaId", "dbo.Sistemas");
            DropIndex("dbo.Usuarios", new[] { "SistemaId" });
            DropColumn("dbo.Usuarios", "SistemaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "SistemaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Usuarios", "SistemaId");
            AddForeignKey("dbo.Usuarios", "SistemaId", "dbo.Sistemas", "Id", cascadeDelete: true);
        }
    }
}
