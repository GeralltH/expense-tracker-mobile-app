using ExpenseTracker.ViewModel;
using Xamarin.Forms;
using ExpenseTracker.View;
using ExpenseTracker.Model;
using System.Collections.Generic;

namespace ExpenseTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            ViewModel = new MainPageViewModel();
            InitializeComponent();
        }

        public MainPageViewModel ViewModel
        {
            get { return BindingContext as MainPageViewModel; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        void OnExpenseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectExpenseCommand.Execute(e.SelectedItem);

            ListView list = sender as ListView;
            list.SelectedItem = -1;
        }

        //Trying to work out how to filter the list 
        //private void ClaimedSwitch_OnChanged(object sender, ToggledEventArgs e)
        //{
        //    List<Expense> tmpList = new List<Expense>();

        //    if (claimedSwitch.IsEnabled)
        //    {

        //        foreach (Expense expense in Expense)
        //        {
        //            if (expense.Claimed == true)
        //            {
        //                tmpList.Add(expense);
        //            }
        //        }
        //        expenseListView.ItemsSource = tmpList;
        //    }
        //    else
        //    {
        //        expenseListView.ItemsSource = { Binding Expenses};
        //    }
        //}

        //public List<Expense> FilterExpenses(bool claimed)
        //{
        //    return expenseList.Where(Expense.claimed == "True").ToList();
        //}
        //https://stackoverflow.com/questions/33449149/filtering-list-of-objects-based-on-string-and-bool-in-c-sharp

    }
}