using Microsoft.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;



//Dapper micro ORM => extension method = ir no nut e instalar a extensao do Dapper (vem para nos ajudar a fazer de forma mais simples)


//se voce for fazer conexao com o SQL tem que usar no nutget um SQLClient 


#region DAPPER

//// no dapper nao precisa abrir conexao
//using (var conn = new SqlConnection("Data Source=localhost,1423;User ID=sa;Password=Senha@123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
//{
//    string query = @"Select TOP(1000) [Id], [Nome] from[Cliente].[dbo].[Produtos]";
//    //Select TOP(1000) [Id], [Nome], [Cor] from[Cliente].[dbo].[Produtos]
//    var listaProdutos = conn.Query<Produtos>(query);
//}

//public class Produtos
//{
//    public int Id { get; set; } 
//    public string ?Nome { get; set; }

//}
#endregion


#region Contrib
//Contrib
//using (var conn = new SqlConnection("Data Source=localhost,1423;User ID=sa;Password=Senha@123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
//{
//    //CRUD  > r (READ) > LISTA
//    var categorias = conn.GetAll<Categoria>();
//    // CRUD > R - SELECIONAR
//    var categoria = conn.Get<Categoria>(2);
//    //CRUD > C > CRIAR
//    conn.Insert<Categoria>(new Categoria() { Nome = "3" });
//    conn.Insert<Categoria>(new Categoria() { Nome = "4" });

//    // CRUD > U> UPDATE
//    conn.Update<Categoria>(new Categoria() { Id = 3, Nome = "34" });

//    // CRUD > D> DELETE
//    conn.Delete<Categoria>(new Categoria() { Id = 4, Nome = "4"});
//}

//[Table("[Cliente].[dbo].Categorias")]
//public class Categoria
//{
//    [Key]
//    public int Id { get; set; }
//    public string ? Nome { get; set; }

//}
#endregion 

#region Multiplas Execuções
//using (var conn = new SqlConnection("Data Source=localhost,1423;User ID=sa;Password=Senha@123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
//{  //inserir uma linha
//    string query = $@"insert into Cliente.dbo.Categorias (Nome) values (@nome)";
//    //    DynamicParameters param = new DynamicParameters();
//    //    param.Add("nome", "333");

//    ////    int linhasafetadas = conn.Execute(query, param, commandType: System.Data.CommandType.Text);

//    //    if (linhasafetadas == 0)
//    //    {
//    //        Console.WriteLine("Falhou");

//    //    }
//    //    else
//    //    {
//    //        Console.WriteLine($"Quantidade de linhas afetadas: {linhasafetadas}");
//    //    }
//    //}
//    //Multiplas Execuções

//    int linhasafetadas = conn.Execute(query,
//        new[]
//        {
//            new { nome = "44" },
//            new { nome = "55" },
//            new { nome = "88" },
//            new { nome = "00" }
//        }
//        );
//    if (linhasafetadas == 0)

//    {
//        Console.WriteLine("Falhou");
//    }
//    else
//    {
//        Console.WriteLine($"Quantidade de linhas afetadas: {linhasafetadas}");

//    }
//}
#endregion
using (var conn = new SqlConnection("Data Source=localhost,1423;User ID=sa;Password=Senha@123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
{
  //  string query = "select * from [Cliente].[dbo].[Categorias]";
    //var linhas = conn.Query<Categorias>(query);

    //foreach (var item in linhas)
    //{
    //    //Console.WriteLine(item.Id + " | "+ item.Nome);
    //}
    //query = "select * from [Cliente].[dbo].[Categorias] where id = 2";
    //var linha = conn.QueryFirst<Categorias>(query);
    //Console.WriteLine(linha.Id + " | " + linha.Nome);

    string query = @"select * from [Cliente].[dbo].[Categorias];
        select * from [Cliente].[dbo].[Categorias] where id = 2";

    using (var multi = conn.QueryMultiple(query, commandType: System.Data.CommandType.Text))
    {
        var resultado1 = multi.Read<Categorias>();
        var resultado2 = multi.ReadFirst<Categorias>();

        foreach (var item in resultado1)
        {
            Console.WriteLine(item.Id + " | "+ item.Nome);
        }
        Console.WriteLine(resultado2.Id + " | " + resultado2.Nome);
    }
} 
   class Categorias
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public override string ToString()
    {
        return Id + " " + Nome;
    }
}
