using Microsoft.AspNetCore.Mvc;
using APIProdutos.Repository;

namespace APIProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        public List<Produto> ProdutoList { get; set; }
        public ProdutoRepository repositoryProduto;
        public ProdutoController(IConfiguration _configuration)
        {
            ProdutoList = new List<Produto>();
            repositoryProduto = new ProdutoRepository(_configuration);
        }

        [HttpGet("/produto/{descricao}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Produto> GetProduto(string descricao)
        {
            if (repositoryProduto.GetProdutoPorDescricao(descricao).Count == 0)
            {
                return NotFound();
            }
            return Ok(repositoryProduto.GetProdutoPorDescricao(descricao));
        }

        [HttpGet("/produto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Produto>> GetProdutos()
        {
            return Ok(repositoryProduto.GetProdutos());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Produto> PostProduto(Produto produto)
        {
            if (!repositoryProduto.InsertProdutos(produto))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(PostProduto), produto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateProduto(long id, Produto produto)
        {
            if (!repositoryProduto.UpdateProduto(produto, id))
                return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Produto> DeleteProduto(long id)
        {
            if (!repositoryProduto.DeleteProduto(id))
                return NotFound();
            return NoContent();
        }
    }
}