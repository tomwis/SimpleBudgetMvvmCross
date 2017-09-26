using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using SimpleBudgetMvvmCross.Core.Resources;
using SimpleBudgetMvvmCross.Core.ViewModels;

namespace SimpleBudgetMvvmCross.Core
{
    public class CoreApp : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            
            RegisterAppStart<ViewModels.DashboardViewModel>();
        }
    }
}
