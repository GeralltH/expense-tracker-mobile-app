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

            if (Title.Equals("New Expense"))
            {
                dateAddedPicker.Date = DateTime.Now;
                dateAddedPicker.IsEnabled = false;

                claimedLayout.IsVisible = false;
                claimedLayout.HeightRequest = 0;

                unpaid.IsVisible = true;
                datePaidPicker.IsVisible = false;
            }

            if (Title.Equals("Edit Expense"))
            {
                dateAddedPicker.IsEnabled = false;
            }

            double fullReceiptAmount = Convert.ToDouble(receiptAmount.Text);
            double receiptVATAmount = fullReceiptAmount * 0.8;
            string VAT = receiptVATAmount.ToString();
            VATAmount.Text = "Excl. VAT: £" + receiptVATAmount.ToString("F");



            string datePaid = datePaidPicker.Date.ToString();
            //if (datePaid.Equals("1/1/0001 12:00:00 AM"))
            //{
            //    saveButton.IsEnabled = false;
            //}
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