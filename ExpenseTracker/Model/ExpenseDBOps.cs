using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace ExpenseTracker.Model
{
    public class ExpenseDBOps
    {
        private SQLiteAsyncConnection _connection;
        private DatabaseModel _dbModel;

        public ExpenseDBOps()
        {
            _dbModel = new DatabaseModel();
            _connection = _dbModel.GetConnection();
            _connection.CreateTableAsync<Expense>();
        }

        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await _connection.Table<Expense>().OrderBy(x=>x.DateAdded).ToListAsync();
        }

        public async Task DeleteExpense(Expense expense)
        {
            await _connection.DeleteAsync(expense);
        }

        public async Task AddExpense(Expense expense)
        {
            await _connection.InsertAsync(expense);
        }

        public async Task UpdateExpense(Expense expense)
        {
            await _connection.UpdateAsync(expense);
        }

        public async Task<Expense> GetExpense(int id)
        {
            return await _connection.FindAsync<Expense>(id);
        }
    }
}