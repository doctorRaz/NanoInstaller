using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.NanoInstallerFromIni.Ini;
using drz.NanoInstallerFromIni.Instances;

using static drz.NanoInstallerFromIni.Instances.InstanceAppCfg;

namespace drz.NanoInstallerFromIni.Servise
{
    /// <summary>
    /// класс подготовки данных инсталятора
    /// </summary>
    internal class PrepSettings
    {

        internal PrepSettings(List<AppCfg> _appCfg, List<AddonCfg> _addonCfg, bool _isAdd)
        {
            appCfgs = _appCfg;

            addonCfg = _addonCfg;

            isAdd = _isAdd;
        }

        List<AddonCfg> addonCfg { get; set; }

        bool isAdd;

        List<AppCfg> appCfgs { get; set; }


        SetupSet _setupSets;

        /// <summary>
        /// Gets the setup sets.
        /// </summary>
        /// <value>
        /// The setup sets.
        /// </value>
        public SetupSet SetupSets
        {
            get
            {
                //all setup
                _setupSets = new SetupSet();
                _setupSets.IsAdd = isAdd;
                foreach (AppCfg appCfg in appCfgs)
                {
                    AppSet appSet = new AppSet();
                    appSet.AppCfg = appCfg;
                    appSet.AddonCfgs = addonCfg;
                    appSet.IsAdd = isAdd;


                    //foreach (string addonPath in addonCfg)
                    //{
                    //    Addon addon = new Addon()
                    //    {
                    //        AddonPath = addonPath,
                    //        IsAdd = isAdd,
                    //        Name = Path.GetFileNameWithoutExtension(addonPath)
                    //    };
                    //    appSet.AddonCfgs.Add(addon);

                    //}

                    _setupSets.AppSets.Add(appSet);
                }

                return _setupSets;
            }

        }

    }

}
