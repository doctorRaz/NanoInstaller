using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drz.NanoInstallerFromIni.Ini
{

    #region IniClass    

    /// <summary>
    ////Ini уровня CAD версии
    /// </summary>
    public class IniApp
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IniApp"/> class.
        /// </summary>
        public IniApp()
        {
            Modules = new List<IniModule>();
        }
        /// <summary>
        /// Gets or sets the nano version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the modules spds mech def.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        public List<IniModule> Modules { get; set; }
    }

    /// <summary>
    /// Ini уровня модуля spds mech def
    /// </summary>
    public class IniModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IniModule"/> class.
        /// </summary>
        public IniModule()
        {
            Sections = new List<IniSection>();
        }

        /// <summary>
        /// Gets or sets the name of the module spds mech def
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the sections.
        /// </summary>
        /// <value>
        /// The sections.
        /// </value>
        public List<IniSection> Sections { get; set; }
    }

    /// <summary>
    /// Секция
    /// </summary>
    public class IniSection
    {
        public IniSection()
        {
            Keys = new List<IniKey>();
        }

        string _name;

        /// <summary>
        /// Gets the name без номера секции.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }

        }
        string _realName;

        /// <summary>
        /// Gets or sets the name of the real прочитанное имя.
        /// </summary>
        /// <value>
        /// The name of the real.
        /// </value>
        public string RealName
        {
            get
            {
                return _realName;
            }
            set
            {
                string[] arName = value.Split('\\');
                if (arName.Length > 5 && arName[5].Contains("app", StringComparison.InvariantCultureIgnoreCase))
                {
                    arName[5] = "app";

                    _isSectionApp = true;//флаг, что из секции загрузка и ее надо нумеровать

                    _name = string.Join("\\", arName);
                }
                else
                {
                    _name = value;
                    _isSectionApp = false;//флаг, что из секции загрузка и ее надо нумеровать
                }

                _realName = value;
            }
        }

        /// <summary>
        /// Gets the name of the module spds mech def Unknown.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public string ModuleName
        {
            get
            {
                string[] arName = RealName.Split('\\');
                if (arName.Length > 2)
                {

                    return arName[2];
                }
                else
                {
                    return "Unknown";
                }
            }
        }

        bool _isSectionApp;
        public bool IsSectionApp
        {
            get

            {
                return _isSectionApp;
            }
        }


        /// <summary>
        /// Gets or sets the keys.
        /// </summary>
        /// <value>
        /// The keys.
        /// </value>
        public List<IniKey> Keys { get; set; }


    }
    public class IniKey
    {
        public string ModuleName { get; set; }
        public string Section { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        //public List<IniValues> Values { get; set; }
    }

    //public class IniValues
    //{
    //    public string ModuleName { get; set; }
    //    public string Section { get; set; }
    //    public string Loader { get; set; }
    //    public string appType { get; set; }
    //    public string Enabled { get; set; }

    //}
    #endregion
    
}
