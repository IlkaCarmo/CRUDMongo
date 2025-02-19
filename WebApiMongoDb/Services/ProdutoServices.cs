using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApiMongoDb.Models;

namespace WebApiMongoDb.Services
{
    public class Produtoservices
    {
        private readonly IMongoCollection<Produto> _produtoCollection;

        public Produtoservices(IOptions<ProdutoDatabaseSettings> produtoservices)
        {
            var mongoClient = new MongoClient(produtoservices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(produtoservices.Value.DatabaseName);

            _produtoCollection = mongoDatabase.GetCollection<Produto>(produtoservices.Value.ProdutoCollectionName);
        }

        public async Task<List<Produto>> GetProdutosAsync() => 
            await _produtoCollection.Find(produto => true).ToListAsync();

        public async Task<Produto> GetProdutoAsync(string id) =>
            await _produtoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Produto produto) =>
            await _produtoCollection.InsertOneAsync(produto);

        public async Task UpdateAsync(string id, Produto produto) =>
            await _produtoCollection.ReplaceOneAsync(x => x.Id == id, produto);

        public async Task DeleteAsync(string id) =>
            await _produtoCollection.DeleteOneAsync(x => x.Id == id);

    }
}
