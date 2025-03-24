using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using drz.NanoInstallerFromIni.Enum;
using drz.NanoInstallerFromIni.Ini;

namespace drz.NanoInstallerFromIni.Instances
{   
    /// <summary>
    /// все про аддоны
    /// </summary>
    internal class InstanceAddonCfg
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceAddonCfg"/> class.
        /// </summary>
        /// <param name="_addonDir">The addon dir.</param>
        /// <param name="_appTypeEnum">The application type enum.</param>
        /// <param name="_searchOption">The search option.</param>
        public InstanceAddonCfg(string _addonDir, AppTypeEnum _appTypeEnum = AppTypeEnum.APP_PACKAGE, SearchOption _searchOption = SearchOption.AllDirectories)
        {
            addonDir = _addonDir;
            appTypeEnum = _appTypeEnum;
            searchOption = _searchOption;
        }

        /// <summary>
        /// Gets the addon ext.
        /// </summary>
        /// <value>
        /// The addon ext.
        /// </value>
        string addonExt
        {
            get
            {
                return ConvertType.AddonExt(appTypeEnum);
            }
        }

        List<AddonCfg> _addonCfgs;

        /// <summary>
        /// Gets the addon CFGS.
        /// </summary>
        /// <value>
        /// The addon CFGS.
        /// </value>
        internal List<AddonCfg> AddonCfgs
        {
            get
            {
                _addonCfgs = new List<AddonCfg>();
                DirectoryInfo directory = new DirectoryInfo(addonDir);
                if (directory.Exists)
                {
                    FileInfo[] fileInfos = directory.GetFiles($"*.{addonExt}", searchOption);

                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        AddonCfg addonCfg = new AddonCfg();
                        addonCfg.Name = fileInfo.Name;
                        addonCfg.AddonPath = fileInfo.FullName;
                        addonCfg.addonExt = addonExt;
                        addonCfg.appTypeEnum = appTypeEnum;

                        _addonCfgs.Add(addonCfg);
                    }
                    return _addonCfgs;
                }
                else
                {
                    return _addonCfgs;
                }
            }
        }

        /// <summary>
        /// Gets or sets the search option.
        /// </summary>
        /// <value>
        /// The search option.
        /// </value>
        SearchOption searchOption { get; set; }

        /// <summary>
        /// Gets or sets the addon dir.
        /// </summary>
        /// <value>
        /// The addon dir.
        /// </value>
        string addonDir { get; set; }

        /// <summary>
        /// Gets or sets the application type enum.
        /// </summary>
        /// <value>
        /// The application type enum.
        /// </value>
        AppTypeEnum appTypeEnum { get; set; }



    }

    /// <summary>
    /// Cfg Addon
    /// </summary>
    internal class AddonCfg
    {
        internal string Name { get; set; }

        internal bool IsAdd { get; set; }

        internal string AddonPath { get; set; }

        internal string addonExt { get; set; }

        internal AppTypeEnum appTypeEnum { get; set; }
    }

}
