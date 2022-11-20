using WorkWithDB.DataAccess.EntityFramework.Entity;

namespace WorkWithDB.AppLogic
{
    internal interface ITableService
    {
        void AddMarket(Market market);
        void AddUser(User user);
        void AddUser(Product product);
        IEnumerable<Market> GetMarkets();
    }
}