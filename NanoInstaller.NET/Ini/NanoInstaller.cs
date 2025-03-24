using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.NanoInstallerFromIni.Enum;
using drz.NanoInstallerFromIni.Instances;

namespace drz.NanoInstallerFromIni.Ini
{
    internal class NanoInstaller
    {
        internal NanoInstaller(SetupSet _setset)
        {
            setset = _setset;
        }

        IniApp _iniAllModules;
        public IniApp IniAllModules
        {
            get
            {
                ClsIni clsIni = new ClsIni(pathCfg);

                _iniAllModules = new IniApp();//? module spds def mech

                IniModule iniAppsSpds = new IniModule() { ModuleName = "SPDS" };

                IniModule iniAppsMex = new IniModule() { ModuleName = "Mech" };

                IniModule iniAppsDef = new IniModule() { ModuleName = "<<Default>>" };

                IniModule iniAppsUnknown = new IniModule() { ModuleName = "Unknown" };


                _iniAllModules.Modules.Add(iniAppsUnknown);
                _iniAllModules.Modules.Add(iniAppsSpds);
                _iniAllModules.Modules.Add(iniAppsMex);
                _iniAllModules.Modules.Add(iniAppsDef);

                List<string> sections = clsIni.ReadSections();//get All sections

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

                    switch (iniSections.ModuleName)
                    {
                        case "Unknown":
                            iniAppsUnknown.Sections.Add(iniSections);
                            break;
                        case "SPDS":
                            iniAppsSpds.Sections.Add(iniSections);
                            break;
                        case "Mech":
                            iniAppsMex.Sections.Add(iniSections);
                            break;
                        case "<<Default>>":
                            iniAppsDef.Sections.Add(iniSections);
                            break;
                        default:
                            iniAppsUnknown.Sections.Add(iniSections);
                            break;
                    }
                }
                return _iniAllModules;
            }
            set
            {
                _iniAllModules = value;

#if DEBUG
                ClsIni clsIni = new ClsIni(pathCfg + ".bak");
#else
                ClsIni clsIni = new ClsIni(pathCfg);
#endif

                int iniSections = 0;

                foreach (IniModule mod in _iniAllModules.Modules)
                {
                    foreach (IniSection sec in mod.Sections)
                    {
                        if (sec.IsSectionApp)
                        {
                            Console.WriteLine($"{sec.Name}");
                            if (sec.Keys.Count > 0)
                            {
                                foreach (IniKey key in sec.Keys)
                                {
                                    Console.WriteLine($"{key.KeyName}={key.KeyValue}");
                                }
                            }
                            else
                            {

                            }
                        }

                    }

                }

            }
        }

        static readonly string sectionPatternStart = @"\Configuration\";

        static readonly string sectionPatternEnd = @"\Appload\Startup\app";
        /// <summary>
        /// наномодули
        /// </summary>
        static string[] modules = { "SPDS", "<<Default>>", "Mech" };//todo Enum

        /// <summary>
        /// путь к наноконфигу
        /// </summary>
        /// <value>
        /// The path CFG ini.
        /// </value>
        internal string pathCfg { get; set; }

        /// <summary>
        ////путь к файлу аддона
        /// </summary>
        internal string AddonPath { get; set; }

        /// <summary>
        /// полный путь к аддону
        /// </summary>
        /// <value>
        /// The addon paths.
        /// </value>
        List<AddonCfg> addonCfgs { get; set; }

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
            {
                string extension = Path.GetExtension(AddonPath);
                return ConvertType.AddonType(extension).ToString();
            }
        }
        //"APP_PACKAGE";//todo real val

        bool isAdd { get; set; }



        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <returns></returns>
        internal bool Install()
        {
            bool isOk = true;

            foreach (var nano in setset.AppSets)
            {
                pathCfg = nano.AppCfg.CfgPath;

                addonCfgs = nano.AddonCfgs;

                isAdd = nano.IsAdd;

                //? if (!GetIni()) isOk = false;//если хоть один сбой то не ОК
            }

            return isOk;
        }

        internal void AddAddonByName()
        {
            throw new NotImplementedException("Worked");
            /********************************************************************************************************

             if ADD
             проверить все ли модули есть

             [\Configuration\SPDS\Appload]	
             [\Configuration\SPDS\Appload\Startup]	

 ****************************/

            /********************************************************************************************************
            //if remove
            var iniAppsSpds = IniAllModules;

            foreach (IniSection section in iniAppsSpds.Sections)//? заглушка
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

            compare String.Equals(loader, valuePattern, StringComparison.InvariantCultureIgnoreCase)
            if (key == "Loader" && iniKeys.KeyValue == pathAddRemoveAddon)// exist addon
            {
                if (isAdd)// add 
                {

                }
                else//rem
                {

                }

            }
            else
            {

            }



            IniApp iniNanos = new IniApp();
            //выбираем только секции запуска аддонов
            foreach (string app0 in modules)
            {
                string patern = $@"{sectionPatternStart}{app0}{sectionPatternEnd}";
                IEnumerable<IniSection> selectedSections = from p in iniAppsSpds.Sections//? заглушка
                                                           where p.RealName.Contains(patern, StringComparison.InvariantCultureIgnoreCase)
                                                           select p;
                //iniNanos.Modules.Add(selectedSections);
                //IniSection iniSortSections = selectedSections;// new IniSection(selectedSections);
            }


            ********************************************************************************************************/



        }

        internal void AddAddonByFullName()
        {
            throw new NotImplementedException("Worked");
        }

        /// <summary>
        /// Удаление аддона по имени
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal void RemoveAddonByName()
        {
            throw new NotImplementedException("Worked");
        }

        /// <summary>
        /// удаление имени по полному пути 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal void RemoveAddonByFullName()
        {
            throw new NotImplementedException("Worked");
        }


    }







}
