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

            DateTime dateToday = DateTime.Now;

            dateAddedPicker.MinimumDate = dateToday;
            //datePaidPicker.MaximumDate = dateToday;

            if (Title.Equals("New Expense"))
            {
                //dateAddedPicker.Date = DateTime.Now;
                //dateAddedPicker.IsVisible = false;
                //dateAddedLabel.IsVisible = true;

                claimedLayout.IsVisible = false;
                claimedLayout.HeightRequest = 0;

                datePaidLabel.IsVisible = false;
                datePaidPicker.IsEnabled = false;

                datePaidPicker.IsEnabled = false;
            }

            if (Title.Equals("Edit Expense"))
            {
                //If claim is paid, disables all options
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



            //Works out VAT Amount
            double fullReceiptAmount = Convert.ToDouble(receiptAmount.Text);
            double receiptVATAmount = fullReceiptAmount * 0.8;
            string VAT = receiptVATAmount.ToString();
            VATAmount.Text = "Excl. VAT: £" + receiptVATAmount.ToString("F");

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