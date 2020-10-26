namespace ProvaEMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfiguracaoInicalBD : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Idade = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 60),
                        Endereco_Rua = c.String(nullable: false, maxLength: 80),
                        Endereco_Numero = c.Int(nullable: false),
                        Endereco_Cep = c.String(nullable: false, maxLength: 50),
                        Endereco_Complemento = c.String(maxLength: 85),
                        Endereco_Bairro = c.String(nullable: false, maxLength: 50),
                        Endereco_Cidade = c.String(nullable: false, maxLength: 60),
                        Endereco_Estado = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contatoes",
                c => new
                    {
                        ContatoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Numero = c.String(nullable: false),
                        Tipo = c.String(nullable: false),
                        Cliente_Id = c.Int(),
                        Fornecedor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ContatoId)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id)
                .ForeignKey("dbo.Fornecedors", t => t.Fornecedor_Id)
                .Index(t => t.Cliente_Id)
                .Index(t => t.Fornecedor_Id);
            
            CreateTable(
                "dbo.Fornecedors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrazoEntrega = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 60),
                        Endereco_Rua = c.String(nullable: false, maxLength: 80),
                        Endereco_Numero = c.Int(nullable: false),
                        Endereco_Cep = c.String(nullable: false, maxLength: 50),
                        Endereco_Complemento = c.String(maxLength: 85),
                        Endereco_Bairro = c.String(nullable: false, maxLength: 50),
                        Endereco_Cidade = c.String(nullable: false, maxLength: 60),
                        Endereco_Estado = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contatoes", "Fornecedor_Id", "dbo.Fornecedors");
            DropForeignKey("dbo.Contatoes", "Cliente_Id", "dbo.Clientes");
            DropIndex("dbo.Contatoes", new[] { "Fornecedor_Id" });
            DropIndex("dbo.Contatoes", new[] { "Cliente_Id" });
            DropTable("dbo.Fornecedors");
            DropTable("dbo.Contatoes");
            DropTable("dbo.Clientes");
        }
    }
}
