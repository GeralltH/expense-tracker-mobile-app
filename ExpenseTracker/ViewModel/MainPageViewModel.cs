using ExpenseTracker.Model;
using ExpenseTracker.View;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseTracker.ViewModel
{
    public class MainPageViewModel
    {
        private ExpenseDBOps _expenseStore;
        private bool _isDataLoaded;
        public ObservableCollection<ExpenseDetailPageViewModel> Expenses { get; private set; }
        = new ObservableCollection<ExpenseDetailPageViewModel>();
        public ObservableCollection<ExpenseDetailPageViewModel> _expenses { get; set; }
        = new ObservableCollection<ExpenseDetailPageViewModel>();

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddExpenseCommand { get; private set; }
        public ICommand SelectExpenseCommand { get; private set; }
        public ICommand DeleteExpenseCommand { get; private set; }
        public ICommand ClaimedExpensesCommand { get; private set; }


        public MainPageViewModel()
        {
            _expenseStore = new ExpenseDBOps();

            LoadDataCommand = new Command(async () => await LoadData());
            AddExpenseCommand = new Command(async () => await AddExpense());
            SelectExpenseCommand = new Command<ExpenseDetailPageViewModel>(async c => await SelectExpense(c));
            DeleteExpenseCommand = new Command<ExpenseDetailPageViewModel>(async c => await DeleteExpense(c));
            ClaimedExpensesCommand = new Command(ClaimedExpenses);



            MessagingCenter.Subscribe<ExpenseDetailPageViewModel, Expense>
                (this, "ExpenseAdded", OnExpenseAdded);
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;
            _isDataLoaded = true;
            var expenses = await _expenseStore.GetExpensesAsync();
            //foreach (var expense in expenses)
            //    Expenses.Add(new ExpenseDetailPageViewModel(expense));

            foreach (var expense in expenses)
            {
                ExpenseDetailPageViewModel newExpense = new ExpenseDetailPageViewModel(expense);
                _expenses.Add(newExpense);
                Expenses.Add(newExpense);
            }
        }

        private async Task AddExpense()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ExpenseDetailPage(new ExpenseDetailPageViewModel()));
        }

        private async Task SelectExpense(ExpenseDetailPageViewModel expense)
        {
            if (expense == null)
                return;

            await Application.Current.MainPage.Navigation.PushAsync(new ExpenseDetailPage(expense));
        }

        private async Task DeleteExpense(ExpenseDetailPageViewModel expenseDetailPageViewModel)
        {
            if (await Application.Current.MainPage.DisplayAlert("Warning", $"Are you sure you want to delete \"{expenseDetailPageViewModel.Title}\"?", "Yes", "No"))
            {
                Expenses.Remove(expenseDetailPageViewModel);
                _expenses.Remove(expenseDetailPageViewModel);
                var expense = await _expenseStore.GetExpense(expenseDetailPageViewModel.Id);
                await _expenseStore.DeleteExpense(expense);
            }
        }

        private void ClaimedExpenses()
        {
            foreach(var expense in _expenses)
            {
                if(expense.Claimed == false)
                {
                    Expenses.Remove(expense);
                }
            }
        }

        private void OnExpenseAdded(ExpenseDetailPageViewModel source, Expense expense)
        {
            Expenses.Add(new ExpenseDetailPageViewModel(expense));
            _expenses.Add(new ExpenseDetailPageViewModel(expense));
        }
    }
}