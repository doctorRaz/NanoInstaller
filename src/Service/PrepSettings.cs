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

        internal PrepSettings(List<AppCfg> _appCfg, List<AddonCfg> _addonCfg)
        {
            appCfgs = _appCfg;

            addonCfg = _addonCfg;
                      
        }

        List<AddonCfg> addonCfg { get; set; }

       

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
  
                foreach (AppCfg appCfg in appCfgs)
                {
                    AppSet appSet = new AppSet();
                  
                    appSet.AppCfg = appCfg;
                    
                    appSet.AddonCfgs = addonCfg;
                  
                    _setupSets.AppSets.Add(appSet);
                }

                return _setupSets;
            }

        }

    }

}
