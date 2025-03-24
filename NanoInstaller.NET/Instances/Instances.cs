using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using drz.NanoInstallerFromIni.Enum;
using drz.NanoInstallerFromIni.Ini;

namespace drz.NanoInstallerFromIni.Instances
{
    /// <summary>
    /// все про нано
    /// </summary>
    internal class InstanceNano
    {
        internal class nanoCfg
        {
            public string CfgPath{ get; set; }
            public string CfgName{ get; set; }

        }

        //можно расширить и здесь жэж получать имена нанокадов дабы показать их юзеру в установщике

        /// <summary>
        /// имя конфига, прибито наногвоздями
        /// </summary>
        private readonly string _cfgFileName = "cfg.ini";

        /// <summary>
        /// Запрет устанавливать аддоны написанные на .NET на net framework.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is dot net; otherwise, <c>false</c>.
        /// </value>
        bool isDotNet { get; set; }


        List<nanoCfg> _nanoCfgPaths;
        /// <summary>
        /// пути к конфигам нано (не проверяем установлен или нет)
        /// </summary>
        /// <value>
        /// The nano CFG paths.
        /// </value>
        internal List<nanoCfg> nanoCfgs //? учитывать версию нано <23 net framework >=23 NET isDotNet
        {
            get
            {
                _nanoCfgPaths = new List<nanoCfg>();

                DirectoryInfo directory = new DirectoryInfo(AppNanoDir);
                if (directory.Exists)
                {
                    FileInfo[] fileInfos = directory.GetFiles(_cfgFileName, SearchOption.AllDirectories);

                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string fulFileNameCfg = fileInfo.FullName;

                        string[] ArrApp = fulFileNameCfg.Split('\\');

                        string AppFulName = ArrApp[ArrApp.Length - 3];

                        string[] ArrVersion = AppFulName.Split(" ");

                        string Appversion = ArrVersion[ArrVersion.Length - 1];

                        Match match = Regex.Match(Appversion, @"(\d+)");

                        int AppVersionNumber = 0;

                        if (match.Success)
                        {
                            bool isInt = int.TryParse(match.Groups[1].Value, out AppVersionNumber);
                        }
                        if ((AppVersionNumber < 23 && isDotNet) || AppVersionNumber < 20)
                        {//если .Net пропуск всего младше 23
                         //конфигурации младше 20 вообще не учитываем

                            continue;
                        }

                        nanoCfg nanoCfgPath = new nanoCfg();
                        nanoCfgPath.CfgPath = fulFileNameCfg;
                        nanoCfgPath.CfgName = AppFulName;
                        
                        _nanoCfgPaths.Add(nanoCfgPath);
                    }

                    return _nanoCfgPaths;
                }
                else
                {
                    return _nanoCfgPaths;
                }

            }
        }

        /// <summary>
        /// нанокаталог, в нанокоде прибит гвоздями
        /// </summary>
        /// <value>
        /// The application nano dir.
        /// </value>
#if DEBUG
        string AppNanoDir = @"d:\@Developers\В работе\!Текущее\Programmers\!NET\NanoInstaller\@res\Nanosoft\";
#else
        string AppNanoDir => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nanosoft");
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceNano"/> class.
        /// </summary>
        /// <param name="_isDotNet">if set to <c>true</c> [is dot net].</param>
        public InstanceNano(bool _isDotNet = false)
        {

            isDotNet = _isDotNet;

        }
    }

    /// <summary>
    /// все про аддоны
    /// </summary>
    internal class InstanceAddon
    {

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

        /// <summary>
        /// Gets the addon paths.
        /// </summary>
        /// <value>
        /// The addon paths.
        /// </value>
        internal List<string> AddonPaths
        {
            get
            {
                DirectoryInfo directory = new DirectoryInfo(addonDir);
                if (directory.Exists)
                {
                    FileInfo[] files = directory.GetFiles($"*.{addonExt}", searchOption);

                    return (from p in files//? заглушка
                            select p.FullName).ToList();
                }
                else
                {
                    return new List<string>();
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

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceAddon"/> class.
        /// </summary>
        /// <param name="_addonDir">The addon dir.</param>
        /// <param name="_appTypeEnum">The application type enum.</param>
        /// <param name="_searchOption">The search option.</param>
        public InstanceAddon(string _addonDir, AppTypeEnum _appTypeEnum = AppTypeEnum.APP_PACKAGE, SearchOption _searchOption = SearchOption.AllDirectories)
        {
            addonDir = _addonDir;
            appTypeEnum = _appTypeEnum;
            searchOption = _searchOption;
        }

    }




}
