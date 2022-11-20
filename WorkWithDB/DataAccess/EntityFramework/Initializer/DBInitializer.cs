using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkWithDB.DataAccess.EntityFramework.Entity;
using WorkWithDB.DataAccess.EntityFramework.Repository.Interfaces;

namespace WorkWithDB.DataAccess.EntityFramework.Initializer
{
    internal class DBInitializer : IDBInitializer
    {
        private readonly EBayMarketContext _dbContext;
        private readonly IMarketRepository _marketRepository;
        private readonly ILogger<DBInitializer> _logger;

        public DBInitializer(EBayMarketContext dbContext, IMarketRepository marketRepository, ILogger<DBInitializer> logger)
        {
            _dbContext = dbContext;
            _marketRepository = marketRepository;
            _logger = logger;
        }

        public void Initialize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() != 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "");
            }

            int marketsCount = _marketRepository.GetAll().Count();
            if (marketsCount == 0)
            { 
                _marketRepository.Add(new Market
                {
                    Name = "Games"
                });
                _marketRepository.Add(new Market
                {
                    Name = "Tools"
                });
                _marketRepository.Save();
            }
        }
    }
}
