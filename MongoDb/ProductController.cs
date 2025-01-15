using MongoDB.Driver;

namespace MongoDb___Workshop;

public class ProductController
{
    private ProductRepository _repository;

    public ProductController()
    {
        _repository = new ProductRepository();
    }

    public async Task AddProduct()
    {
        try
        {
            Console.WriteLine("Adicionar produto");
            Console.WriteLine("----------------");

            Produto novoProduto = new Produto
            {
                Id = Guid.NewGuid().ToString(),
                Nome = Utils.ReadFromConsole("Digite o nome do produto: "),
                Preco = Decimal.Parse(Utils.ReadFromConsole("Digite o preço do produto: "))
            };
            await _repository.Insert(novoProduto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task ListProducts()
    {
        try
        {
            List<Produto> produtos = await _repository.List();
            Utils.ExibirComoTabela(produtos);
            //produtos.ForEach(p => Console.WriteLine($"{p.Id} - {p.Nome}: {p.Preco:C}"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task GetByName(string produto)
    {
        try
        {
            var filter = Builders<Produto>.Filter.Eq(p => p.Nome, produto);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    public async Task UpdateProduct()
    {
        try
        {
            await ListProducts();
            
            Produto antigo = await _repository.GetFirstByName(Utils.ReadFromConsole("Digite o nome do produto atual: "));
            
            Produto novoProduto = new Produto
            {
                Nome = Utils.ReadFromConsole($"Digite o novo nome do produto (Antigo: {antigo.Nome}): "),
                Preco = Decimal.Parse(Utils.ReadFromConsole($"Digite o novo preço do produto (Antigo: {antigo.Preco}): "))
            };
            await _repository.Update(antigo.Nome, novoProduto);
            Console.WriteLine("Produto atualizado com sucesso");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task DeleteProduct()
    {
        await ListProducts();
        await _repository.Delete( Utils.ReadFromConsole("Digite o nome do produto atual: "));
    }
}