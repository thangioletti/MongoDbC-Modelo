using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class MongoDbRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbRepository(string databaseName, string collectionName)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDbConnection"));
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<T>(collectionName);
    }

    // Insert: Insere um documento
    public async Task InsertAsync(T entity)
    {
        try
        {
            await _collection.InsertOneAsync(entity);
            Console.WriteLine("Documento inserido com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao inserir documento: {ex.Message}");
        }
    }

    // Update: Atualiza um documento com base em um filtro
    public async Task UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
    {
        try
        {
            var result = await _collection.UpdateOneAsync(filter, update);
            if (result.ModifiedCount > 0)
                Console.WriteLine("Documento atualizado com sucesso.");
            else
                Console.WriteLine("Nenhum documento foi atualizado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar documento: {ex.Message}");
        }
    }

    // Find: Busca documentos com base em um filtro
    public async Task<List<T>> FindAsync(FilterDefinition<T> filter)
    {
        try
        {
            return await _collection.Find(filter).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar documentos: {ex.Message}");
            return new List<T>();
        }
    }

    // Delete: Remove documentos com base em um filtro
    public async Task DeleteAsync(FilterDefinition<T> filter)
    {
        try
        {
            var result = await _collection.DeleteOneAsync(filter);
            if (result.DeletedCount > 0)
                Console.WriteLine("Documento deletado com sucesso.");
            else
                Console.WriteLine("Nenhum documento foi deletado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao deletar documento: {ex.Message}");
        }
    }
}