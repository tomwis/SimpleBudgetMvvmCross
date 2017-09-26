using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using SimpleBudgetMvvmCross.Core.Models;
using SimpleBudgetMvvmCross.Core.Services;
using MvvmCross.Core.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SimpleBudgetMvvmCross.Core.Models.Enums;
using SimpleBudgetMvvmCross.Core.Helpers;
using SimpleBudgetMvvmCross.Core.Resources;
using System;
using System.Globalization;

namespace SimpleBudgetMvvmCross.Core.ViewModels
{
    public class MonthEditViewModel : MvxViewModel<MonthItem, MonthItem>
    {
        public IDbService DbService { get; set; }
        public IMvxNavigationService NavigationService { get; set; }
        bool _isEditing;

        public override Task Initialize(MonthItem parameter)
        {
            FillData();
            if (parameter != null)
            {
                _isEditing = true;
                MonthItem = parameter;
                var (_, budgetItems) = DbService.LoadMonth(parameter.Month, parameter.Year);

                foreach (var group in BudgetItems)
                {
                    foreach (var item in budgetItems.Where(s => s.Type == group.Type))
                    {
                        group.Add(item);
                    }
                }
            }
            else
            {
                _isEditing = false;
                var now = DateTime.Now;
                MonthItem = new MonthItem
                {
                    Month = now.Month,
                    Year = now.Year
                };
            }

            SelectedMonth = Months.FirstOrDefault(s => s.Number == MonthItem.Month);
            PropertyChanged += MonthEditViewModel_PropertyChanged;
            MonthItem.PropertyChanged += MonthItem_PropertyChanged;
            foreach (var group in BudgetItems)
            {
                foreach (var item in group)
                {
                    item.PropertyChanged += BudgetItem_PropertyChanged;
                }
            }
            return Task.FromResult(true);
        }

        void FillData()
        {
            BudgetItems = new ObservableCollection<BudgetGroup>
            {
                new BudgetGroup { Title = AppResources.RecurringItemsGroupLabel, Type = BudgetItemType.Recurring },
                new BudgetGroup { Title = AppResources.OneTimeItemsGroupLabel, Type = BudgetItemType.OneTime }
            };
            int yearsToShow = 10;
            Years = Enumerable.Range(DateTime.Now.Year - yearsToShow, yearsToShow + 1).ToList();
            Months = new List<MonthDisplay>(DateTimeFormatInfo.CurrentInfo.MonthNames.Where(s => !string.IsNullOrEmpty(s)).Select((s, i) => new MonthDisplay(s, i + 1)));
        }

        private void BudgetItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(BudgetItem.Amount):
                    CalculateMoneyLeft();
                    break;
            }
        }

        private void MonthItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MonthItem.LastMonthEarnings):
                    CalculateMoneyLeft();
                    break;
            }
        }

        void CalculateMoneyLeft()
        {
            var moneyLeft = MonthItem.LastMonthEarnings;
            foreach (var group in BudgetItems)
            {
                foreach (var item in group)
                {
                    moneyLeft -= item.Amount;
                }
            }
            MonthItem.MoneyLeft = moneyLeft;
        }

        public override async void ViewDisappearing()
        {
            PropertyChanged -= MonthEditViewModel_PropertyChanged;
            MonthItem.PropertyChanged -= MonthItem_PropertyChanged;
            foreach (var group in BudgetItems)
            {
                foreach (var item in group)
                {
                    item.PropertyChanged -= BudgetItem_PropertyChanged;
                }
            }
            await Close(MonthItem);
            base.ViewDisappearing();
        }

        private void MonthEditViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedMonth):
                    if (SelectedMonth != null)
                    {
                        MonthItem.Month = SelectedMonth.Number;
                    }
                    break;
            }
        }

        public IMvxCommand AddExpeseCmd => new MvxCommand<BudgetItemType>(t =>
        {
            var newItem = new BudgetItem { Type = t };
            newItem.PropertyChanged += BudgetItem_PropertyChanged;
            BudgetItems.First(s => s.Type == t).Add(newItem);
        });

        public IMvxCommand DeleteExpeseCmd => new MvxCommand<BudgetItem>(b =>
        {
            var group = BudgetItems.First(s => s.Type == b.Type);
            b.PropertyChanged -= BudgetItem_PropertyChanged;
            group.Remove(b);
            CalculateMoneyLeft();
        });

        public IMvxCommand SaveCmd => new MvxCommand(async () =>
        {
            var items = new List<BudgetItem>(BudgetItems[0]);
            items.AddRange(BudgetItems[1]);

            foreach (var item in items)
            {
                item.Month = MonthItem.Month;
                item.Year = MonthItem.Year;
            }

            if (!_isEditing && DbService.HasMonth(MonthItem.Month, MonthItem.Year))
            {
                await App.Current.MainPage.DisplayAlert("", AppResources.MothAlreadyExistsMessage, AppResources.Ok);
            }
            else
            {
                MonthItem.BackgroundColor = ColorHelper.GenerateRandomPleasingColor();
                DbService.SaveMonth(MonthItem, items, _isEditing);
                await Close(MonthItem);
            }
        });
        
        private MonthItem _monthItem;
        public MonthItem MonthItem
        {
            get { return _monthItem; }
            set { SetProperty(ref _monthItem, value); }
        }

        private ObservableCollection<BudgetGroup> _budgetItems;
        public ObservableCollection<BudgetGroup> BudgetItems
        {
            get { return _budgetItems; }
            set { SetProperty(ref _budgetItems, value); }
        }

        private List<int> _years;
        public List<int> Years
        {
            get { return _years; }
            set { SetProperty(ref _years, value); }
        }

        private List<MonthDisplay> _months;
        public List<MonthDisplay> Months
        {
            get { return _months; }
            set { SetProperty(ref _months, value); }
        }

        private MonthDisplay _selectedMonth;
        public MonthDisplay SelectedMonth
        {
            get { return _selectedMonth; }
            set { SetProperty(ref _selectedMonth, value); }
        }
    }
}