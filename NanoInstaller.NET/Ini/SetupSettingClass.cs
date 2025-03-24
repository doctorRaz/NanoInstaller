using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Addons = new List<Addon>();
        }

        /// <summary>
        /// Gets or sets the path CFG inis.
        /// </summary>
        /// <value>
        /// The path CFG inis.
        /// </value>
        internal string NanoCfgPath { get; set; }

        /// <summary>
        /// Gets or sets the addon paths.
        /// </summary>
        /// <value>
        /// The addon paths.
        /// </value>
        internal List<Addon> Addons { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is added addon.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is added addon; otherwise, <c>false</c>.
        /// </value>
        internal bool IsAdd { get; set; }
    }

    internal class Addon
    {
        internal string Name { get; set; }

        internal bool IsAdd { get; set; }

        internal string AddonPath { get; set; }

    }

    #endregion
}
