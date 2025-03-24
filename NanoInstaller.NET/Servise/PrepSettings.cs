using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.NanoInstallerFromIni.Ini;

using static drz.NanoInstallerFromIni.Instances.InstanceNano;

namespace drz.NanoInstallerFromIni.Servise
{
    /// <summary>
    /// класс подготовки данных инсталятора
    /// </summary>
    internal class PrepSettings
    {

        internal PrepSettings(List<nanoCfg> _nanoCfgPaths, List<string> _addonPaths, bool _isAdd)
        {
            nanoCfgPaths = _nanoCfgPaths;

            addonPaths = _addonPaths;

            isAdd = _isAdd;
        }

        List<string> addonPaths;

        bool isAdd;

        List<nanoCfg> nanoCfgPaths { get; set; }


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
                foreach (var nanoCfgPath in nanoCfgPaths)
                {
                    AppSet appSet = new AppSet();
                    appSet.NanoCfgPath = nanoCfgPath.CfgPath;

                    foreach (string addonPath in addonPaths)
                    {
                        Addon addon = new Addon()
                        {
                            AddonPath = addonPath,
                            IsAdd = isAdd,
                            Name = Path.GetFileNameWithoutExtension(addonPath)
                        };
                        appSet.Addons.Add(addon);

                    }
                    appSet.IsAdd = isAdd;
                    _setupSets.AppSets.Add(appSet);

                }

                return _setupSets;
            }

        }

    }

}
