using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDB.DataAccess.Npgsql.Interfaces
{
    internal interface ITableCreator
    {
        void CreateTables();
    }
}
