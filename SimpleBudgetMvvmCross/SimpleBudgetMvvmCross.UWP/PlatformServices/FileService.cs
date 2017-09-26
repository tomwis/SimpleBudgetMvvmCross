using SimpleBudgetMvvmCross.Core.Config;
using SimpleBudgetMvvmCross.Core.PlatformServices;
using SimpleBudgetMvvmCross.UWP.PlatformServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBudgetMvvmCross.UWP.PlatformServices
{
    public class FileService : IFileService
    {
        public string DbPath
        {
            get
            {
                var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return Path.Combine(localFolder.Path, Consts.DbName);
            }
        }
    }
}
