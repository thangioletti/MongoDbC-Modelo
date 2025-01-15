using MongoDB.Driver;

namespace MongoDb___Workshop;

public class ProductRepository
{
    private MongoDbRepository<Produto> _repository;

    public ProductRepository()
    {
        
        _repository = new MongoDbRepository<Produto>(
            "MinhaBaseDeDados",
            "Produtos"
        );
    }

    public async Task<Produto> Insert(Produto produto)
    {
        await _repository.InsertAsync(produto);
        return produto;
    }

    public async Task<Produto> GetFirstByName(string nomeProduto) 
    {
        var filter = Builders<Produto>.Filter.Eq(p => p.Nome, nomeProduto);
        List<Produto> produtos = await _repository.FindAsync(filter);
        return produtos.FirstOrDefault();
    }
    
    public async Task<List<Produto>> List()
    {
        var filtro = Builders<Produto>.Filter.Empty; // Busca todos
        return await _repository.FindAsync(filtro);
    }
    
    

    public async Task<Produto> Update(string nomeAntigo, Produto produto)
    {
        var filter = Builders<Produto>.Filter.Eq(p => p.Nome, nomeAntigo);
        var update = Builders<Produto>.Update.Set(p => p.Nome, produto.Nome).Set(p => p.Preco, produto.Preco);

        await _repository.UpdateAsync(filter, update);
        return produto;
    }
    

    public async Task Delete(string nomeProduto)
    {
        var filtroDeletar = Builders<Produto>.Filter.Eq(p => p.Nome, nomeProduto);
        await _repository.DeleteAsync(filtroDeletar);
    }
}