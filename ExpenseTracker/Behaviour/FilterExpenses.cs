using Xamarin.Forms;

namespace ExpenseTracker.Behaviour
{
    public class FilterExpenses : Behavior<ListView>
    {
        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
        }

        //Trying to work out how to filter the list 
        //private void Switch_OnChanged(object sender, ToggledEventArgs e)
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
