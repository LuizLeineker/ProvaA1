using Microsoft.EntityFrameworkCore;

//CONFIGURAÇÃO COM BANCO DE DADOS
//1 - Instalar as bibliotecas
//2 - Criar a classe de dados
//3 - Criar a herança com a biblioteca
//4 - Indicar as classes de modelo que vão 
//virar tabelas no banco de dados
//5 - Sobrescrever o método de configuração, com banco
//utilizado e a string de conexão



public class AppDataContext : DbContext
{
    public DbSet<Folha> Folhas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=MatheusLuiz.db");
    }

}