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
    /// все про нано
    /// </summary>
    internal class InstanceAppCfg
    {
        /// <summary>
        /// конфигурации нано версий
        /// </summary>
        internal class AppCfg
        {
            /// <summary>
            /// Gets or sets the CFG App path.
            /// </summary>
            /// <value>
            /// The CFG path.
            /// </value>
            public string CfgPath { get; set; }

            /// <summary>
            /// Gets or sets the name of the App CFG.
            /// </summary>
            /// <value>
            /// The name of the CFG.
            /// </value>
            public string CfgName { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is add.
            /// </summary>
            /// <value>
            ///   <c>true</c> менять конфигурацию приложения; не менять, <c>false</c>.
            /// </value>
            public bool IsAdd{ get; set; }

        }

        /// <summary>
        /// имя конфига, прибито наногвоздями
        /// </summary>
        private readonly string _cfgFileName = "cfg.ini";

        /// <summary>
        /// Запрет устанавливать аддоны написанные на .NET на net framework.
        /// </summary>
        /// <value>
        ///   <c>true</c> если аддоны NET6 для всех нано новее 22; иначе все версии новее нано 20, <c>false</c>.
        /// </value>
        bool isDotNet { get; set; }


        List<AppCfg> _nanoCfgPaths;
        /// <summary>
        /// пути к конфигам нано (не проверяем установлен или нет)
        /// </summary>
        /// <value>
        /// The nano CFG paths.
        /// </value>
        internal List<AppCfg> AppCfgs  
        {
            get
            {
                _nanoCfgPaths = new List<AppCfg>();

                DirectoryInfo directory = new DirectoryInfo(AppNanoDir);
                if (directory.Exists)
                {
                    FileInfo[] fileInfos = directory.GetFiles(_cfgFileName, SearchOption.AllDirectories);

                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string fulFileNameCfg = fileInfo.FullName;

                        string[] ArrApp = fulFileNameCfg.Split('\\');

                        string AppFulName = ArrApp[ArrApp.Length - 3];//name

                        string[] ArrVersion = AppFulName.Split(new string[] { " " } ,StringSplitOptions.RemoveEmptyEntries);

                        string Appversion = ArrVersion[ArrVersion.Length - 1];//version

                        Match match = Regex.Match(Appversion, @"(\d+)");

                        int AppVersionNumber = 0;

                        if (match.Success)
                        {
                            //пропускать не требуется, если false AppVersionNumber=0
                            bool isInt = int.TryParse(match.Groups[1].Value, out AppVersionNumber);
                        }
                        if ((AppVersionNumber < 23 && isDotNet) || AppVersionNumber < 20)
                        {//если .Net пропуск всего младше 23
                         //конфигурации младше 20 вообще не учитываем

                            continue;
                        }

                        AppCfg nanoCfgPath = new AppCfg();
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
        /// Initializes a new instance of the <see cref="InstanceAppCfg"/> class.
        /// </summary>
        /// <param name="_isDotNet">if set to <c>true</c> [is dot net].</param>
        public InstanceAppCfg(bool _isDotNet = false)
        {

            isDotNet = _isDotNet;

        }
    }
     
}
