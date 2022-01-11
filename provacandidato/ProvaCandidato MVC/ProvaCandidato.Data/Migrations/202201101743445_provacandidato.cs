namespace ProvaCandidato.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class provacandidato : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        codigo = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.codigo);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        codigo = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 50),
                        data_nascimento = c.DateTime(),
                        codigo_cidade = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.codigo)
                .ForeignKey("dbo.Cidade", t => t.codigo_cidade, cascadeDelete: true)
                .Index(t => t.codigo_cidade);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "codigo_cidade", "dbo.Cidade");
            DropIndex("dbo.Cliente", new[] { "codigo_cidade" });
            DropTable("dbo.Cliente");
            DropTable("dbo.Cidade");
        }
    }
}
