using System.Xml.XPath;

public class AutoIncrement
{
    private static int currentID = 0;

    public int GenerateID()
    {
        currentID++;
        return currentID;
    }
}
public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string DueDate { get; set; }

    public TodoItem()
    {
        AutoIncrement idGenerator = new AutoIncrement();
        Id = idGenerator.GenerateID();
    }
}

public class TodoRepository
{
    private List<TodoItem> _todoItems;

    public TodoRepository()
    {
        _todoItems = new List<TodoItem>();
    }

    public TodoItem GetTodoItem(int id)
    {
        return _todoItems.FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<TodoItem> GetTodoItems()
    {
        return _todoItems;
    }
    public void SearchTodoItem()
    {
        Console.Clear();

        Console.WriteLine("Informe uma palavra-chave para a busca:");
        string? search = Console.ReadLine();

        var existingItem = _todoItems.Where(x => x.Title.StartsWith(search)).ToList();
        if (existingItem != null)
        {
            Console.WriteLine("\r\n--- Resultado da Busca ---\r\n");
            foreach (var item in existingItem)
            {
                if (item.IsCompleted == false)
                {

                    Console.WriteLine($"ID: {item.Id} \r\nTitilo: {item.Title} \r\nDescrição: {item.Description}\r\nDueDate: {item.DueDate} \r\nFinalizada: {item.IsCompleted} \r\n");
                }
            }

        }
        else
        {
            Console.WriteLine("\r\n--- Nenhuma tarefa encontrada ---\r\n");

        }
    }

    public void AddTodoItem()
    {

        // Clear the Console
        Console.Clear();
        Console.WriteLine("\r\n --- Cadastro de Tarefa --- \r\n ");
        Console.WriteLine("Informe o titulo da tarefa");
        string? title = Console.ReadLine();

        Console.WriteLine("Informe a descrição da tarefa");
        string? description = Console.ReadLine();

        Console.WriteLine("Informe a data de vencimento da tarefa (DD/MM/YY)");
        string? duedate = Console.ReadLine();

        _todoItems.Add(new TodoItem { Title = title, Description = description, DueDate = duedate });

        Console.WriteLine("\r\n Tarefa Adicionada !! \r\n");

    }

    public void UpdateTodoItem()
    {
        // Clear the Console
        Console.Clear();
        ListTodoItems();
        Console.WriteLine("\r\n Informer o ID da tarefa que deseja atualizar");
        int Idtask = int.Parse(Console.ReadLine());


        var existingItem = GetTodoItem(Idtask);

        if (existingItem != null)
        {

            Console.WriteLine("\r\n --- Atualização de Tarefa --- \r\n ");
            Console.WriteLine("Informe o titulo da tarefa");
            string? title = Console.ReadLine();

            Console.WriteLine("Informe a descrição da tarefa");
            string? description = Console.ReadLine();

            Console.WriteLine("Informe a data de vencimento da tarefa");
            string? duedate = Console.ReadLine();

            existingItem.Title = title;
            existingItem.Description = description;
            existingItem.DueDate = duedate;

            Console.WriteLine("\r\n Tarefa atualizada !! \r\n");

        }
    }



    public void ListTodoItems()
    {
        // Clear the Console
        Console.Clear();
        var items = GetTodoItems();

        Console.WriteLine("\r\n--- Tarefas Pendentes --- \r\n");
        foreach (var item in items)
        {
            if (item.IsCompleted == false)
            {

                Console.WriteLine($"ID: {item.Id} \r\nTitilo: {item.Title} \r\nDescrição: {item.Description}\r\nDueDate: {item.DueDate} \r\nFinalizada: {item.IsCompleted} \r\n");
            }
        }
        Console.WriteLine("\r\n--- Tarefas Concluídas --- \r\n");
        foreach (var item in items)
        {
            if (item.IsCompleted == true)
            {

                Console.WriteLine($"ID: {item.Id} \r\nTitilo: {item.Title} \r\nDescrição: {item.Description}\r\nDueDate: {item.DueDate} \r\nFinalizada: {item.IsCompleted} \r\n");
            }
        }
    }

     public void StatisticTodoItems()
    {
        // Clear the Console
        Console.Clear();
        var items = GetTodoItems();
        int taskPending = 0;
        int taskComplete = 0;

        Console.WriteLine("\r\n--- Informações sobre as Tarefas --- \r\n");
        Console.WriteLine($"Total de Tarefas: {items.Count()}");
        foreach (var item in items)
        {
            if (item.IsCompleted == false)
            {
                taskPending++;
            }else{
                taskComplete++;
            }
        }
        Console.WriteLine($"Total de Tarefas Pendentes: {taskPending}");
        Console.WriteLine($"Total de Tarefas Concluidas: {taskComplete}\r\n");
        Console.WriteLine("Tarefas mais recente:");
        Console.WriteLine($"ID: {items.ToList()[0].Id} \r\nTitilo: {items.ToList()[0].Title} \r\nDescrição: {items.ToList()[0].Description}\r\nDueDate: {items.ToList()[0].DueDate} \r\nFinalizada: {items.ToList()[0].IsCompleted} \r\n");
        Console.WriteLine("\r\nTarefas mais Antiga:");
        Console.WriteLine($"ID: {items.ToList()[items.Count() -1 ].Id} \r\nTitilo: {items.ToList()[items.Count() -1 ].Title} \r\nDescrição: {items.ToList()[items.Count() -1 ].Description}\r\nDueDate: {items.ToList()[items.Count() -1 ].DueDate} \r\nFinalizada: {items.ToList()[items.Count() -1 ].IsCompleted} \r\n");


        Console.WriteLine("\r\n");


        
    }
    
    public void DeleteTodoItem()
    {

        // Clear the Console
        Console.Clear();
        ListTodoItems();
        Console.WriteLine("\r\n Informer o ID da tarefa que deseja excluir");
        int Idtask = int.Parse(Console.ReadLine());

        var existingItem = GetTodoItem(Idtask);

        if (existingItem != null)
        {
            _todoItems.Remove(existingItem);
            Console.WriteLine("\r\n Tarefa removida !! \r\n");

        }
        else
        {
            Console.WriteLine("ID Inválido");

        }
    }
    public void CompleteTodoItem()
    {

        // Clear the Console
        Console.Clear();
        ListTodoItems();
        Console.WriteLine("\r\n Informer o ID da tarefa que deseja concluir");
        int Idtask = int.Parse(Console.ReadLine());

        var existingItem = GetTodoItem(Idtask);

        if (existingItem != null)
        {
            existingItem.IsCompleted = true;

            Console.WriteLine("\r\n Tarefa concluida !! \r\n");
        }
        else
        {
            Console.WriteLine("ID Inválido");

        }
    }
}




namespace Application
{

    class Program
    {

        public static int Menu()
        {
            int opcao;
            do
            {
                Console.WriteLine("1 - P/ Adicionar Tarefa");
                Console.WriteLine("2 - P/ Marcar Tarefa como Concluída");
                Console.WriteLine("3 - P/ Listar Tarefas");
                Console.WriteLine("4 - P/ Excluir Tarefa");
                Console.WriteLine("5 - P/ Buscar Tarefa");
                Console.WriteLine("6 - P/ Atualizar Tarefa");
                Console.WriteLine("7 - P/ Exibir Estatísticas");
                Console.WriteLine("0 - P/ Sair");

                opcao = int.Parse(Console.ReadLine());
            } while (opcao < 0 || opcao > 7);

            return opcao;
        }
        static void Main(string[] args)
        {
            var repository = new TodoRepository();
            int op;


            do
            {
                op = Menu();

                switch (op)
                {
                    case 1:
                        repository.AddTodoItem();

                        break;
                    case 2:
                        repository.CompleteTodoItem();

                        break;
                    case 3:
                        repository.ListTodoItems();

                        break;
                    case 4:
                        repository.DeleteTodoItem();

                        break;
                    case 5:
                        repository.SearchTodoItem();

                        break;
                    case 6:
                        repository.UpdateTodoItem();

                        break;
                    case 7:
                        repository.StatisticTodoItems();

                        break;
                    default:

                        break;

                }
            } while (op != 0);

        }
    }
}
