using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Forms.Droid;
using MvvmCross.Platform.IoC;
using SimpleBudgetMvvmCross.Droid.PlatformServices;
using SimpleBudgetMvvmCross.Core.PlatformServices;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Localization;

namespace SimpleBudgetMvvmCross.Droid
{
    public class Setup : MvxFormsAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
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
