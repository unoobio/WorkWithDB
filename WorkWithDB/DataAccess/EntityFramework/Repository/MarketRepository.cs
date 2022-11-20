using Microsoft.EntityFrameworkCore;
using WorkWithDB.DataAccess.EntityFramework.Entity;
using WorkWithDB.DataAccess.EntityFramework.Repository.Interfaces;

namespace WorkWithDB.DataAccess.EntityFramework.Repository
{
    internal class MarketRepository : Repository<Market>, IMarketRepository
    {
        public MarketRepository(EBayMarketContext db) : base(db)
        {

        }
    }
}
