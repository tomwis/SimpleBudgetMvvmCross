using MvvmCross.Forms.Core;
using SimpleBudgetMvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleBudgetMvvmCross.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthEditView : MvxContentPage<MonthEditViewModel>
    {
        public MonthEditView()
        {
            InitializeComponent();
        }
    }
}