using System.Data;
using static WorkWithDB.DataAccess.Npgsql.SqlScriptExecutor;

namespace WorkWithDB.DataAccess.Npgsql.Interfaces
{
    public interface ISqlScriptExecutor
    {
        void ExecuteScript(string sqlScript, params IDbDataParameter[] sqlParams);
        List<Row> GetRowValues(string sqlScript, params IDbDataParameter[] sqlParams);
    }
}