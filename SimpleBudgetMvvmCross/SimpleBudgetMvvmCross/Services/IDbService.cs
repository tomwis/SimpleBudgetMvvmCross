using SimpleBudgetMvvmCross.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBudgetMvvmCross.Core.Services
{
    public interface IDbService
    {
        List<MonthItem> LoadAllMonths();
        (MonthItem monthItem, List<BudgetItem> budgetItems) LoadMonth(int month, int year);
        void SaveMonth(MonthItem monthItem, List<BudgetItem> budgetItems, bool isEditing);
        bool HasMonth(int month, int year);
    }
}
