using MvvmCross.Core.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBudgetMvvmCross.Core.Models
{
    [Table("MonthItem")]
    public class MonthItem : MvxNotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed(Name = "MonthId", Order = 1, Unique = true)]
        public int Year { get; set; }
        [Indexed(Name = "MonthId", Order = 2, Unique = true)]
        public int Month { get; set; }

        private double _lastMonthEarnings;
        public double LastMonthEarnings
        {
            get { return _lastMonthEarnings; }
            set { SetProperty(ref _lastMonthEarnings, value); }
        }

        private double _moneyLeft;
        public double MoneyLeft
        {
            get { return _moneyLeft; }
            set { SetProperty(ref _moneyLeft, value); }
        }

        public string MonthName { get; set; }
        public string BackgroundColor { get; set; } = "#00000000";
    }
}
