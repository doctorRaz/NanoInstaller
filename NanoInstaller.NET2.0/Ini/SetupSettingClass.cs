using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.NanoInstallerFromIni.Enum;
using drz.NanoInstallerFromIni.Instances;

using static drz.NanoInstallerFromIni.Instances.InstanceAppCfg;

namespace drz.NanoInstallerFromIni.Ini
{
    #region SetupSettings

    internal class SetupSet
    {
        internal SetupSet()
        {
            AppSets = new List<AppSet>();
        }

        internal List<AppSet> AppSets { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is added addon.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is added addon; otherwise, <c>false</c>.
        /// </value>
        internal bool IsAdd { get; set; }
    }
    internal class AppSet
    {
        internal AppSet()
        {
            AddonCfgs = new List<AddonCfg>();
        }

        /// <summary>
        /// Gets or sets the cfg App nano.
        /// </summary>
        /// <value>
        /// The path CFG inis.
        /// </value>
        internal AppCfg AppCfg { get; set; }

        /// <summary>
        /// Gets or sets the addon paths.
        /// </summary>
        /// <value>
        /// The addon paths.
        /// </value>
        internal List<AddonCfg> AddonCfgs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is added addon.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is added addon; otherwise, <c>false</c>.
        /// </value>
        internal bool IsAdd { get; set; }
    }

    //internal class AddonCfg
    //{
    //    internal string Name { get; set; }

    //    internal bool IsAdd { get; set; }

    //    internal string AddonPath { get; set; }

    //    string addonExt { get; set; }

    //     AppTypeEnum appTypeEnum { get; set; }
    //}

    #endregion
}
