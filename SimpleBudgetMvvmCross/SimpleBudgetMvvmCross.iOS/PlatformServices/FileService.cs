using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SimpleBudgetMvvmCross.iOS.PlatformServices;
using SimpleBudgetMvvmCross.Core.PlatformServices;
using SimpleBudgetMvvmCross.Core.Config;
using System.IO;

namespace SimpleBudgetMvvmCross.iOS.PlatformServices
{
    public class FileService : IFileService
    {
        public string DbPath
        {
            get
            {
                var personalDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return Path.Combine(personalDir, Consts.DbName);
            }
        }
    }
}