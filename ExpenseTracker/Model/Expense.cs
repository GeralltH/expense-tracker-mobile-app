using SQLite;
using System;

namespace ExpenseTracker.Model
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(20)]
        public bool Claimed { get; set; }
        public decimal Amount { get; set; }
        public bool VATComponent { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DatePaid { get; set; }
        public string Summary { get; set; }
        [MaxLength(255)]
        public string ReceiptImagePath { get; set; }
    }
}