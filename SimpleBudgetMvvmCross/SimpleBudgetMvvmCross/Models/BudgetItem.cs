using MvvmCross.Core.ViewModels;
using SimpleBudgetMvvmCross.Core.Models.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBudgetMvvmCross.Core.Models
{
    [Table("BudgetItem")]
    public class BudgetItem : MvxNotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        public BudgetItemType Type { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
