
public class Product
{
    public string code { get; private set; }
    public string name { get; private set; }
    public string qtdStock { get; private set; }
    public double price { get; private set; }

}


public class Stock
{
    private List<(string code, string name, int? qtdStock, double price)> _productList;

    public Stock()
    {
        _productList = new();
    }
    public void SearchProduct()
    {
        Console.Clear();

        Console.WriteLine("Informe o código do produto: ");
        string? search = Console.ReadLine();

        var existingItem = _productList.Where(x => x.code == search);
        if (existingItem != null)
        {
            Console.WriteLine("\r\n--- Resultado da Busca ---\r\n");
            foreach (var item in existingItem)
            {
                Console.WriteLine($"Código: {item.code} \r\nNome: {item.name} \r\nQTD Em estoque: {item.qtdStock}\r\nPreço: {item.price}\r\n");
            }

        }
        else
        {
            Console.WriteLine("\r\n--- Nenhuma produto encontrado ---\r\n");

        }
    }

    public void AddProduct()
    {

        int _qtd = 0;
        double _price = 0;
        // Clear the Console
        Console.Clear();
        Console.WriteLine("\r\n --- Cadastro de Produto --- \r\n ");

        Console.WriteLine("Informe o código do produto: ");
        string? _code = Console.ReadLine();
        Console.WriteLine("Informe o nome do produto: ");
        string? _name = Console.ReadLine();

        try
        {
            Console.WriteLine("Informe a quantidade: ");
            _qtd = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe o preço: ");
            _price = double.Parse(Console.ReadLine());
        }
        catch (System.Exception)
        {

            Console.WriteLine("\n\nVocê informou um valor que não é válido !\nInforme apenas valores numéricos!\n\n");
            return;
        }


        try
        {
            _productList.Add((code: _code, name: _name, qtdStock: _qtd, price: _price));
            Console.WriteLine("\r\n Produto Adicionado!! \r\n");

        }
        catch (System.Exception)
        {

            Console.WriteLine("\r\n Oppsss, Não foi possível adicionar o produto\nTente novamente !! \r\n");
        }


    }

    public void ListProductsInStock()
    {
        // Clear the Console
        Console.Clear();

        Console.WriteLine("\r\n--- Lista de Produtos --- \r\n");
        _productList.ForEach(item => Console.WriteLine($"Código: {item.code} \r\nNome: {item.name} \r\nQTD Em estoque: {item.qtdStock}\r\nPreço: {item.price}\r\n"));
    }

    public void IncrementQtd()
    {
        int? _qtd = null;
        Console.Clear();

        Console.WriteLine("Informe o código do produto: ");
        string? search = Console.ReadLine();



        try
        {

            var existingItem = _productList.FirstOrDefault(x => x.code == search);
            Console.WriteLine($"Quantidade atual: {existingItem.qtdStock}\r\n");
            Console.WriteLine("Informe a quantidade que deseja adicionar: ");
            _qtd = int.Parse(Console.ReadLine());

            int index = _productList.FindIndex(tuple => tuple.code == search);

            _qtd += _productList[index].qtdStock;
            _productList[index] = (_productList[index].code, _productList[index].name, _qtd, _productList[index].price);
            Console.WriteLine("\n\nAdicionado com sucesso!\n\n");


        }
        catch (System.Exception)
        {

            Console.WriteLine("\n\nVocê informou um valor que não é válido !\nInforme apenas valores numéricos!\n\n");
            return;
        }


    }


    public void DecrementQtd()
    {
        int? _qtd = null;
        Console.Clear();

        Console.WriteLine("Informe o código do produto: ");
        string? search = Console.ReadLine();


        try
        {
            var existingItem = _productList.FirstOrDefault(x => x.code == search);

            Console.WriteLine($"Quantidade atual: {existingItem.qtdStock}\r\n");
            Console.WriteLine("Informe a quantidade que deseja remover: ");
            _qtd = int.Parse(Console.ReadLine());

            if (_qtd <= existingItem.qtdStock)
            {
                int? newQtd = existingItem.qtdStock - _qtd;
                int index = _productList.FindIndex(tuple => tuple.code == search);
                _productList[index] = (_productList[index].code, _productList[index].name, newQtd, _productList[index].price);
                Console.WriteLine("\n\nRemovido com sucesso!\n\n");

            }
            else
            {
                Console.WriteLine("\n\nVocê esta tentado retirar uma quantidade maior do que a existente no estoque !\nTente com outro valor!\n\n");
                return;
            }

        }
        catch (System.Exception)
        {

            Console.WriteLine("\n\nVocê informou um valor que não é válido !\nInforme apenas valores numéricos!\n\n");
            return;
        }


    }


    public void Report()
    {
        int opcao;
        do
        {
        Console.Clear();

            Console.WriteLine("1 - Listar por quantidade");
            Console.WriteLine("2 - Listar entre valores");
            Console.WriteLine("3 - Listar Total ");
            Console.WriteLine("0 - P/ Sair");
            Console.WriteLine("Sua escolha: ");
            opcao = int.Parse(Console.ReadLine());

        } while (opcao < 0 || opcao > 3);

        switch (opcao)
        {
            case 1:
                Console.WriteLine("Informe a quantidade minima: ");
                int min = int.Parse(Console.ReadLine());
                var result = _productList.Where(tuple => tuple.qtdStock < min).ToList();

                Console.WriteLine($"\n\nProdutos com estoque menor ou igual a {min} \n\n");

                foreach (var item in result)
                {
                    Console.WriteLine($"Código: {item.code} \r\nNome: {item.name} \r\nQTD Em estoque: {item.qtdStock}\r\nPreço: {item.price}\r\n");

                }

                break;
            case 2:
                Console.WriteLine("Informe a quantidade minima: ");
                int _min = int.Parse(Console.ReadLine());
                Console.WriteLine("Informe a quantidade maxima: ");
                int _max = int.Parse(Console.ReadLine());

                var _result = _productList.Where(tuple => tuple.qtdStock >= _min && tuple.qtdStock <= _max).ToList();

                Console.WriteLine($"\n\nProdutos com estoque entre {_min} e {_max} \n\n");

                foreach (var item in _result)
                {
                    Console.WriteLine($"Código: {item.code} \r\nNome: {item.name} \r\nQTD Em estoque: {item.qtdStock}\r\nPreço: {item.price}\r\n");

                }

                break;
        }
    }
}

//show only items in a list of tuples with quantity less than 5 in C# using linq ?
namespace Application
{

    class Program
    {

        static void Main(string[] args)
        {
            var stock = new Stock();
            int? opcao = null;

            do
            {
                try
                {

                    Console.WriteLine("1 - P/ Adicionar Produto");
                    Console.WriteLine("2 - P/ Consultar Produto");
                    Console.WriteLine("3 - P/ Listar Produto");
                    Console.WriteLine("4 - P/ Registrar entrada");
                    Console.WriteLine("5 - P/ Registrar saida");
                    Console.WriteLine("6 - P/ Relatórios");
                    Console.WriteLine("0 - P/ Sair");
                    Console.WriteLine("Sua escolha: ");
                    opcao = int.Parse(Console.ReadLine());

                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine("Você informou um valor que não é válido !\nInforme apenas valores numéricos entre 0 e 4 !\n");
                }

                switch (opcao)
                {
                    case 1:
                        stock.AddProduct();

                        break;
                    case 2:
                        stock.SearchProduct();
                        break;
                    case 3:
                        stock.ListProductsInStock();
                        break;
                    case 4:
                        stock.IncrementQtd();
                        break;
                    case 5:
                        stock.DecrementQtd();
                        break;
                    case 6:
                        stock.Report();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Você informou um valor fora do intervalo !\nInforme apenas valores numéricos entre 0 e 4 !\n\n");
                        break;

                }
            } while (opcao != 0);

        }
    }
}


