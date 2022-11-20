using WorkWithDB.AppLogic;
using WorkWithDB.DataAccess.EntityFramework.Entity;

namespace WorkWithDB.UI
{
    internal class InsertDialog
    {
        private readonly ITableService _tableService;

        public InsertDialog(ITableService tableService)
        {
            _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
        }
        
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Do you want to insert row (Y/N):");
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.KeyChar == 'Y' || key.KeyChar == 'y')
                {
                    bool succeed = false;
                    do
                    {
                        Console.WriteLine($"Enter table name for insert row. Tables: users, markets, products.");
                        var tableName = Console.ReadLine();
                        succeed = InsertRow(tableName);
                    }
                    while (!succeed);
                }
                else
                    break;
            }
        }

        private bool InsertRow(string tableName)
        {
            switch (tableName)
            {
                case "users":
                    InsertUser();
                    break;
                case "products":
                    InsertProduct();
                    break;
                case "markets":
                    InsertMarket();
                    break;
                default:
                    Console.WriteLine("Please write correct table name:");
                    return false;
            }
            return true;
        }

        private void InsertUser()
        {
            Console.WriteLine("Enter column values.");
            Console.WriteLine("First name:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Last name:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Email:");
            var email = Console.ReadLine();
            Console.WriteLine("Phone number:");
            var phoneNumber = Console.ReadLine();
            User user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };
            _tableService.AddUser(user);
        }

        private void InsertProduct()
        {
            Console.WriteLine("Enter column values.");
            Console.WriteLine("Title:");
            var title = Console.ReadLine();
            Console.WriteLine("Description:");
            var description = Console.ReadLine();
            Console.WriteLine("Picture url:");
            var pictureUrl = Console.ReadLine();
            Console.WriteLine("Price:");
            var price = double.Parse(Console.ReadLine());

            var markets = _tableService.GetMarkets();
            Console.WriteLine($"Choose 'Market id' from: \n{string.Join(Environment.NewLine, markets.Select(m => $"{m.Id} {m.Name}"))}");


            var marketId = int.Parse(Console.ReadLine());
            Product user = new Product
            {
                Title = title,
                Description = description,
                PictureUrl = pictureUrl,
                Price = price,
                Market = markets.First(market => market.Id == marketId)
            };
            _tableService.AddUser(user);
        }


        private void InsertMarket()
        {
            Console.WriteLine("Enter column values.");
            Console.WriteLine("Name:");
            var name = Console.ReadLine();

            Market market = new Market
            {
                Name = name
            };
            _tableService.AddMarket(market);
        }
    }
}
