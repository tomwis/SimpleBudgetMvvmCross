using SimpleBudgetMvvmCross.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBudgetMvvmCross.Core.Models
{
    public class BudgetGroup : ObservableCollection<BudgetItem>
    {
        public string Title { get; set; }
        public BudgetItemType Type { get; set; }
    }
}
