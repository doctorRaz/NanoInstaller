using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.NanoInstallerFromIni.Ini;

namespace drz.NanoInstallerFromIni
{
    internal class Repository

    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="nanoCfgPaths">The nano CFG paths.</param>
        /// <param name="addonPaths">The addon paths.</param>
        /// <param name="IsAdd">if set to <c>true</c> [is add].</param>
        public Repository(List<string> nanoCfgPaths, List<string> addonPaths, bool IsAdd)
        {

            //all setup
            SetupSet setupSets = new SetupSet();

            foreach (string nanoCfgPath in nanoCfgPaths)
            {
                AppSet appSet = new AppSet();
                appSet.NanoCfgPath = nanoCfgPath;

                foreach (string addonPath in addonPaths)
                {
                    Addon addon = new Addon()
                    { 
                    AddonPath=addonPath,
                    IsAdd=IsAdd,
                    Name=Path.GetFileNameWithoutExtension(addonPath)
                    };
                    appSet.Addons.Add(addon);

                }
                appSet.IsAdd = IsAdd;
                setupSets.AppSets.Add(appSet);

            }


            NanoInstaller nanoInstaller = new NanoInstaller(setupSets);
            nanoInstaller.Install();//пошли работать
        }
    }
}
