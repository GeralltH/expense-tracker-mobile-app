using ExpenseTracker.Model;
using ExpenseTracker.Service;
using SQLite;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpenseTracker.ViewModel
{
    public class ExpenseDetailPageViewModel : INotifyPropertyChanged
    {
        private readonly ExpenseDBOps _expenseStore;
        private SQLiteAsyncConnection _connection;
        private DatabaseModel _dbModel;
        private ImageSource _expenseImage;
        private Expense _savedExpense;
        private Expense _expense;
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand ChoosePhotoCommand { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ExpenseDetailPageViewModel(Expense expense = null)
        {
            _dbModel = new DatabaseModel();
            _connection = _dbModel.GetConnection();
            _connection.CreateTableAsync<Expense>();

            _expenseStore = new ExpenseDBOps();

            SaveCommand = new Command(async () => await Save());
            CancelCommand = new Command(cancelEdit);

            ChoosePhotoCommand = new Command(async () => await ChoosePhoto());

            //create empty expense for details if we cancel 
            _savedExpense = new Expense();

            if (expense == null)
            {
                _expense = new Expense();
            }
            else
            {
                _expense = expense;
                _expenseImage = ImageSource.FromFile(_expense.ReceiptImagePath);
                onPropertyChanged(nameof(ReceiptImage));
                //save details so we have a backup if the user cancels the edit
                backupExpense();
            }
        }

        void cancelEdit()
        {
            restoreExpense();
            return;
        }

        async Task ChoosePhoto()
        {
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                var libFolder = FileSystem.AppDataDirectory;
                var imgName = _expense.Id + ".png";

                string fileName = Path.Combine(libFolder, imgName);

                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream);
                }

                _expense.ReceiptImagePath = fileName;
                _expenseImage = ImageSource.FromFile(_expense.ReceiptImagePath);
                onPropertyChanged(nameof(ReceiptImage));
            }
        }

        //Need to do this shit 
        async Task Save()
        {
            backupExpense();
            if (string.IsNullOrWhiteSpace(_expense.Title) || string.IsNullOrWhiteSpace(_expense.Summary))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter all details", "Ok");
                return;
            }

            if (_expense.Id == 0)
            {
                await _expenseStore.AddExpense(_expense);
                MessagingCenter.Send(this, "ExpenseAdded", _expense);
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                await _expenseStore.UpdateExpense(_expense);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private void backupExpense()
        {
            _savedExpense.Title = _expense.Title;
            _savedExpense.Claimed = _expense.Claimed;
            _savedExpense.Amount = _expense.Amount;
            _savedExpense.VATComponent = _expense.VATComponent;
            _savedExpense.ReceiptDate = _expense.ReceiptDate;
            _savedExpense.DateAdded = _expense.DateAdded;
            _savedExpense.DatePaid = _expense.DatePaid;
            _savedExpense.Summary = _expense.Summary;
            _savedExpense.ReceiptImagePath = _expense.ReceiptImagePath;
        }

        private void restoreExpense()
        {
            Title = _savedExpense.Title;
            Claimed = _savedExpense.Claimed;
            Amount = _savedExpense.Amount;
            VATComponent = _savedExpense.VATComponent;
            ReceiptDate = _savedExpense.ReceiptDate;
            DateAdded = _savedExpense.DateAdded;
            DatePaid = _savedExpense.DatePaid;
            Summary = _savedExpense.Summary;
            _expense.ReceiptImagePath = _savedExpense.ReceiptImagePath;
            _expenseImage = ImageSource.FromFile(_expense.ReceiptImagePath);
            onPropertyChanged(nameof(ReceiptImage));
        }

        public int Id
        {
            get { return _expense.Id; }
            set { _expense.Id = value; }
        }

        public string Title
        {
            get { return _expense.Title; }
            set
            {
                _expense.Title = value;
                onPropertyChanged();
            }
        }

        public bool Claimed
        {
            get { return _expense.Claimed; }
            set
            {
                _expense.Claimed = value;
                onPropertyChanged();
            }
        }

        public decimal Amount
        {
            get { return _expense.Amount; }
            set
            {
                _expense.Amount = value;
                onPropertyChanged();
            }
        }

        public bool VATComponent
        {
            get { return _expense.VATComponent; }
            set
            {
                _expense.VATComponent = value;
                onPropertyChanged();
            }
        }

        public DateTime ReceiptDate
        {
            get { return _expense.ReceiptDate; }
            set
            {
                _expense.ReceiptDate = value;
                onPropertyChanged();
            }
        }

        public DateTime DateAdded
        {
            get { return _expense.DateAdded; }
            set
            {
                _expense.DateAdded = value;
                onPropertyChanged();
            }
        }

        public DateTime DatePaid
        {
            get { return _expense.DatePaid; }
            set
            {
                _expense.DatePaid = value;
                onPropertyChanged();
            }
        }

        public string Summary
        {
            get { return _expense.Summary; }
            set
            {
                _expense.Summary = value;
                onPropertyChanged();
            }
        }

        public ImageSource ReceiptImage
        {
            get { return _expenseImage; }
            private set { }
        }
    }
}
