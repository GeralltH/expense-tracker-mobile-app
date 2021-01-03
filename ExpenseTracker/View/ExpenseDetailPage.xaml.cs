using ExpenseTracker.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace ExpenseTracker.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseDetailPage : ContentPage
    {
        public ExpenseDetailPage(ExpenseDetailPageViewModel expenseDetailPageViewModel)
        {
            InitializeComponent();
            ViewModel = expenseDetailPageViewModel;
            Title = (expenseDetailPageViewModel.Summary == null) ? "New Expense" : "Edit Expense";

            //Sets the date added to today 
            dateAddedPicker.MinimumDate = DateTime.Now;

            if (Title.Equals("New Expense"))
            {
                //disables the claimed switch and the date picker for date paid
                claimedSwitch.IsEnabled = false;
                datePaidPicker.IsEnabled = false;
            }

            if (Title.Equals("Edit Expense"))
            {
                //If claim is paid, makes all options uneditable
                if (claimedSwitch.IsToggled == true)
                {
                    titleEntry.IsEnabled = false;
                    claimedSwitch.IsEnabled = false;
                    VATSwitch.IsEnabled = false;
                    receiptAmount.IsEnabled = false;
                    receiptDatePicker.IsEnabled = false;
                    dateAddedPicker.IsEnabled = false;
                    datePaidPicker.IsEnabled = false;
                    summaryEntry.IsEnabled = false;
                    pickPhotoButton.IsEnabled = false;
                }

                
            }

            //Works out VAT Amount and changes the label
            double receiptVATAmount = Convert.ToDouble(receiptAmount.Text) - (Convert.ToDouble(receiptAmount.Text) / 1.2);
            VATAmount.Text = "incl. £" + receiptVATAmount.ToString("F") + " VAT";
        }

        public ExpenseDetailPageViewModel ViewModel
        {
            get { return BindingContext as ExpenseDetailPageViewModel; }
            set { BindingContext = value; }
        }

        protected override void OnDisappearing()
        {
            ViewModel.CancelCommand.Execute(null);
            base.OnDisappearing();
        }
    }
}