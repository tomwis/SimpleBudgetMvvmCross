using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Uwp;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform.Platform;
using MvvmCross.Uwp.Platform;
using SimpleBudgetMvvmCross.Core.PlatformServices;
using SimpleBudgetMvvmCross.UWP.PlatformServices;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace SimpleBudgetMvvmCross.UWP
{
    public class Setup : MvxFormsWindowsSetup
    {
        public Setup(Frame rootFrame, LaunchActivatedEventArgs e) : base(rootFrame, e)
        {
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.RegisterSingleton<IFileService>(new FileService());
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.CoreApp();
        }

        protected override MvvmCross.Forms.Core.MvxFormsApplication CreateFormsApplication()
        {
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions()
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.All
            };
        }
    }
}
