namespace WorkWithDB.DataAccess.Npgsql.Interfaces
{
    internal interface ITableReader
    {
        void ReadTable(string tableName);
    }
}