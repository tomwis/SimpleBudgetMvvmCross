using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform.IoC;
using SimpleBudgetMvvmCross.Core.Helpers;
using SimpleBudgetMvvmCross.Core.Models;
using SimpleBudgetMvvmCross.Core.Resources;
using SimpleBudgetMvvmCross.Core.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleBudgetMvvmCross.Core.ViewModels
{
    public class DashboardViewModel : MvxViewModel
    {
        public IDbService DbService { get; set; }
        public IMvxNavigationService NavigationService { get; set; }

        public override void Start()
        {
            base.Start();
            var months = DbService.LoadAllMonths();
            InitMonths(months);
        }

        void InitMonths(IEnumerable<MonthItem> list)
        {
            foreach (var month in list)
            {
                month.MonthName = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[month.Month - 1];
            }
            Months = new MvxObservableCollection<MonthItem>(list.OrderByDescending(s => s.Year).ThenByDescending(m => m.Month));
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            PropertyChanged += DashboardViewModel_PropertyChanged;
        }

        public override void ViewDisappearing()
        {
            PropertyChanged -= DashboardViewModel_PropertyChanged;
            base.ViewDisappearing();
        }

        private async void DashboardViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedMonth):
                    if (SelectedMonth != null)
                    {
                        var result = await NavigationService.Navigate<MonthEditViewModel, MonthItem, MonthItem>(SelectedMonth);
                        SelectedMonth = null;
                        var months = DbService.LoadAllMonths();
                        InitMonths(months);
                    }
                    break;
            }
        }

        public IMvxAsyncCommand AddMonthCmd => new MvxAsyncCommand(async () =>
        {
            var result = await NavigationService.Navigate<MonthEditViewModel, MonthItem, MonthItem>((MonthItem)null);
            var months = DbService.LoadAllMonths();
            InitMonths(months);
        });
        
        private MvxObservableCollection<MonthItem> _months;
        public MvxObservableCollection<MonthItem> Months
        {
            get { return _months; }
            set { SetProperty(ref _months, value); }
        }

        private MonthItem _selectedMonth;
        public MonthItem SelectedMonth
        {
            get { return _selectedMonth; }
            set { SetProperty(ref _selectedMonth, value); }
        }

        public string Today
        {
            get
            {
                var now = DateTime.Now;
                var daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                return $"{AppResources.DaysToPayout}: {daysInMonth - now.Day + 1}";
            }
        }
    }
}
