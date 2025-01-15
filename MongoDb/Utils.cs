namespace MongoDb___Workshop;

public static class Utils
{
    public static string ReadFromConsole(string message)
    {
        Console.WriteLine(message);
        return Console.ReadLine();
    }
    
    public static void ExibirComoTabela<T>(List<T> lista)
    {
        if (lista == null || !lista.Any())
        {
            Console.WriteLine("A lista está vazia.");
            return;
        }

        // Obter propriedades do tipo T
        var propriedades = typeof(T).GetProperties();

        // Obter os nomes das colunas
        var cabecalhos = propriedades.Select(p => p.Name).ToArray();

        // Calcular o tamanho máximo de cada coluna
        var tamanhosColunas = cabecalhos.Select(h => h.Length).ToArray();

        foreach (var item in lista)
        {
            for (int i = 0; i < propriedades.Length; i++)
            {
                var valor = propriedades[i].GetValue(item)?.ToString() ?? string.Empty;
                tamanhosColunas[i] = Math.Max(tamanhosColunas[i], valor.Length);
            }
        }

        // Imprimir cabeçalho
        var linhaCabecalho = $"|{string.Join(" | ", cabecalhos.Select((h, i) => h.PadRight(tamanhosColunas[i])))}|";
        string separadorLinha = $"|{new string('-', linhaCabecalho.Length - 2)}|";
        Console.WriteLine(separadorLinha);
        Console.WriteLine(linhaCabecalho);
        Console.WriteLine(separadorLinha);
        
        // Imprimir linhas
        foreach (var item in lista)
        {
            var linha = string.Join(" | ", propriedades.Select((p, i) =>
            {
                var valor = p.GetValue(item)?.ToString() ?? string.Empty;
                return valor.PadRight(tamanhosColunas[i]);
            }));

            Console.WriteLine($"|{linha}|");
        }
        Console.WriteLine(separadorLinha);
    }
}