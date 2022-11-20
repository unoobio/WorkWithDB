using WorkWithDB.DataAccess.EntityFramework.Entity;
using WorkWithDB.DataAccess.EntityFramework.Repository.Interfaces;

namespace WorkWithDB.DataAccess.EntityFramework.Repository
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(EBayMarketContext db) : base(db)
        {

        }
    }
}
