using SQLite;
using System.IO;
using Xamarin.Essentials;

namespace ExpenseTracker.Model
{
    class DatabaseModel
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var libFolder = FileSystem.AppDataDirectory;
            var dbName = "Expenses.db3";
            var dbPath = Path.Combine(libFolder, dbName);
            return new SQLiteAsyncConnection(dbPath);
        }
    }
}
