
using MongoDb___Workshop;

int opcao = 1;

while (opcao > 0)
{
    Console.WriteLine("CRUD Produto Mongo DB");
    Console.WriteLine("--------------");
    Console.WriteLine("1 - Cadastrar");
    Console.WriteLine("2 - Visualizar");
    Console.WriteLine("3 - Editar");
    Console.WriteLine("4 - Deletar");
    Console.WriteLine("0 - Sair");
    opcao = int.Parse(Console.ReadLine());

    ProductController controller = new ProductController();
    Console.Clear();
    
    switch (opcao)
    {
        case 1:
            await controller.AddProduct();
            break;
        case 2:
            await controller.ListProducts();
            break;
        case 3:
            await controller.UpdateProduct();
            break;
        case 4:
            await controller.DeleteProduct();
            break;
        case 0:
            Console.WriteLine("Adeus!");
            break;
        default: 
            Console.WriteLine("Opção inválida");
            break;
        
    }

    if (opcao != 0)
    {
        Console.WriteLine("Precione enter para continuar");
        Console.ReadLine();
        Console.Clear();
    }

}