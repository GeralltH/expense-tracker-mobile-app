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
                
        //        foreach (Expense expense in list)
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
    }
}