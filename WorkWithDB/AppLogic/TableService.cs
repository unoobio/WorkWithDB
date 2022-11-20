using WorkWithDB.DataAccess.EntityFramework.Entity;
using WorkWithDB.DataAccess.EntityFramework.Repository.Interfaces;

namespace WorkWithDB.AppLogic
{
    internal class TableService : ITableService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMarketRepository _marketRepository;
        private readonly IProductRepository _productRepository;

        public TableService(IUserRepository userRepository,
            IMarketRepository marketRepository,
            IProductRepository productRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _marketRepository = marketRepository ?? throw new ArgumentNullException(nameof(marketRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public void AddUser(User user)
        {
            ArgumentNullException.ThrowIfNull(user);
            _userRepository.Add(user);
            _userRepository.Save();
        }

        public void AddMarket(Market market)
        {
            ArgumentNullException.ThrowIfNull(market);
            _marketRepository.Add(market);
            _marketRepository.Save();
        }

        public IEnumerable<Market> GetMarkets()
        {
            IEnumerable<Market> markets = _marketRepository.GetAll();
            return markets;
        }

        public void AddUser(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            _productRepository.Add(product);
            _productRepository.Save();
        }
    }
}
