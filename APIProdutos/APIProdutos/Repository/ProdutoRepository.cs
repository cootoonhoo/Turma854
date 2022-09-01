using System.Data.SqlClient;
using Dapper;

namespace APIProdutos.Repository
{
    public class ProdutoRepository
    {
        private readonly IConfiguration _configuration;
        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Produto> GetProdutos()
        {
            var query = "SELECT * FROM Produtos";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<Produto>(query).ToList();
        }

        public bool InsertProdutos(Produto produto)
        {
            var query = "INSERT INTO Produtos VALUES(@descricao,@preco, @qntd)";

            var Parameters = new DynamicParameters();
            Parameters.Add("descricao", produto.Descricao);
            Parameters.Add("preco", produto.Preco);
            Parameters.Add("qntd", produto.Quantidade);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, Parameters) == 1;
        }

        public bool DeleteProduto(long id)
        {
            var query = "DELETE FROM Produtos WHERE id = @id";

            var Parameters = new DynamicParameters();
            Parameters.Add("id", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, Parameters) == 1;
        }

        public List<Produto> GetProdutoPorDescricao(string descricao)
        {
            var query = "SELECT * FROM Produtos as Prod WHERE Prod.descricao = @descricao";
            var Parameters = new DynamicParameters();
            Parameters.Add("descricao", descricao);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Produto>(query, Parameters).ToList();
        }

        public bool UpdateProduto(Produto produto, long Id)
        {
            var query = "UPDATE Produtos SET descricao = @desc, preco = @prco, quantidade = @qntd WHERE Produtos.Id = @id";
            var Parameters = new DynamicParameters();
            Parameters.Add("desc", produto.Descricao);
            Parameters.Add("prco", produto.Preco);
            Parameters.Add("qntd", produto.Quantidade);
            Parameters.Add("id", produto.Id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, Parameters) == 1;
        }
    }
}
