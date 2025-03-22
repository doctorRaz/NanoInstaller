using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drz.NanoInstallerFromIni.Ini
{
    internal class NanoInstaller
    {
        IniApp iniAllModules { get; set; }

        static string sectionPatternStart = @"\Configuration\";

        static string sectionPatternEnd = @"\Appload\Startup\app";
        /// <summary>
        /// наномодули
        /// </summary>
        static string[] modules = { "SPDS", "<<Default>>", "Mech" };

        /// <summary>
        /// путь к наноконфигу
        /// </summary>
        /// <value>
        /// The path CFG ini.
        /// </value>
        string pathCfgIni { get; set; }

        /// <summary>
        /// полный путь к аддону
        /// </summary>
        /// <value>
        /// The addon paths.
        /// </value>
        List<Addon> addonPaths { get; set; }

        SetupSet setset { get; set; }

        /// <summary>
        /// тип приложения для записи в наноини
        /// </summary>
        /// <value>
        /// The type of the application.
        /// </value>
        internal string appType
        {
            get
            {//? заглушка
                //string extension = Path.GetExtension(setset.AppSets[0].AddonPaths[0]);
                string extension = Path.GetExtension(pathCfgIni);
                return "APP_PACKAGE";
            }
        }
        //"APP_PACKAGE";//todo real val

        bool isAddAdon { get; set; }

        internal NanoInstaller(SetupSet _setset)
        {
            setset = _setset;
        }

        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <returns></returns>
        internal bool Install()
        {
            bool isOk = true;

            foreach (var nano in setset.AppSets)
            {
                pathCfgIni = nano.NanoCfgPath;

                addonPaths = nano.Addons;

                isAddAdon = nano.IsAdd;

                if (!GetIni()) isOk = false;//если хоть один сбой то не ОК
            }

            return isOk;
        }

        internal bool GetIni()
        {

            //string pathCfgIni = "";// setset.CfgIniPaths[0];//todo add for

            ClsIni clsIni = new ClsIni(pathCfgIni);
            //clsIni.WriteSection("ааа");

            IniModule iniAppsSpds = new IniModule() { ModuleName = "SPDS" };

            IniModule iniAppsMex = new IniModule() { ModuleName = "Mech" };

            IniModule iniAppsDef = new IniModule() { ModuleName = "<<Default>>" };

            IniModule iniAppsUnknown = new IniModule() { ModuleName = "Unknown" };

            iniAllModules = new IniApp();//?module spds def mech

            List<string> sections = clsIni.ReadSections();//read All sections


            //get INi
            foreach (string section in sections)
            {
                //where p.Contains(sectionPatternEnd, StringComparison.InvariantCultureIgnoreCase)

                IniSection iniSections = new IniSection();

                iniSections.RealName = section;

                List<string> keys = clsIni.ReadKeys(section);

                foreach (string key in keys)
                {
                    IniKey iniKeys = new IniKey();

                    iniKeys.KeyName = key;
                    iniKeys.Section = section;
                    iniKeys.ModuleName = iniSections.ModuleName;
                    iniKeys.KeyValue = clsIni.ReadValue(section, key, "");

                    iniSections.Keys.Add(iniKeys);
                }

                iniAppsSpds.Sections.Add(iniSections);//?заглушка модули по условию
            }

            iniAllModules.Modules.Add(iniAppsUnknown);
            iniAllModules.Modules.Add(iniAppsSpds);
            iniAllModules.Modules.Add(iniAppsMex);
            iniAllModules.Modules.Add(iniAppsDef);

            Console.WriteLine("*************");
            /*****************************
            if ADD
            проверить все ли модули есть

            [\Configuration\SPDS\Appload]	
            [\Configuration\SPDS\Appload\Startup]	

            ****************************/

            //if remove


            foreach (IniSection section in iniAppsSpds.Sections)//?заглушка
            {
                if (section.IsSectionApp)
                {
                    Console.WriteLine(section.RealName + " " + section.IsSectionApp.ToString());
                }
                else
                {

                }

                if (section.RealName.Contains(sectionPatternEnd, StringComparison.InvariantCultureIgnoreCase))//only load addon
                {
                }
            }
            /*
            //todo compare String.Equals(loader, valuePattern,StringComparison.InvariantCultureIgnoreCase)
            if (key == "Loader" && iniKeys.KeyValue == pathAddRemoveAddon)// exist addon
            {
                if (isAddAdon)// add 
                {

                }
                else//rem
                {

                }

            }
            else
            {

            }
            */


            IniApp iniNanos = new IniApp();
            //выбираем только секции запуска аддонов
            foreach (string app0 in modules)
            {
                string patern = $@"{sectionPatternStart}{app0}{sectionPatternEnd}";
                IEnumerable<IniSection> selectedSections = from p in iniAppsSpds.Sections//?заглушка
                                                           where p.RealName.Contains(patern, StringComparison.InvariantCultureIgnoreCase)
                                                           select p;
                //iniNanos.Modules.Add(selectedSections);
                //IniSection iniSortSections = selectedSections;// new IniSection(selectedSections);
            }



            return true;




        }










    }



    #region SetupSettings

    internal class SetupSet
    {
        internal SetupSet()
        {
            AppSets = new List<AppSet>();
        }

        internal List<AppSet> AppSets { get; set; }
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
