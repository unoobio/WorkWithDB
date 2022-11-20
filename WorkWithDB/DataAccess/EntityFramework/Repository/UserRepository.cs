using Microsoft.EntityFrameworkCore;
using WorkWithDB.DataAccess.EntityFramework.Entity;
using WorkWithDB.DataAccess.EntityFramework.Repository.Interfaces;

namespace WorkWithDB.DataAccess.EntityFramework.Repository
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EBayMarketContext db) : base(db)
        {

        }
    }
}
