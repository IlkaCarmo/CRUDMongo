using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMongoDb.Models;
using WebApiMongoDb.Services;

namespace WebApiMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly Produtoservices _produtoservices;

        public ProdutosController(Produtoservices produtoservices)
        {
            _produtoservices = produtoservices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetProdutos()
        {
            var produtos = await _produtoservices.GetProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(string id)
        {
            var produto = await _produtoservices.GetProdutoAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> CreateProduto(Produto produto)
        {
            await _produtoservices.CreateAsync(produto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(string id, Produto produto)
        {
            var produtoAtual = await _produtoservices.GetProdutoAsync(id);
            if (produtoAtual == null)
            {
                return NotFound();
            }
            await _produtoservices.UpdateAsync(id, produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(string id)
        {
            var produto = await _produtoservices.GetProdutoAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            await _produtoservices.DeleteAsync(id);
            return NoContent();
        }
    }
}
