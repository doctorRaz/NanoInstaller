﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using drz.NanoInstallerFromIni.Enum;

namespace drz.NanoInstallerFromIni.Ini
{
    internal class NanoInstaller
    {
        internal NanoInstaller(SetupSet _setset)
        {
            setset = _setset;
        }

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
            {
                string extension = Path.GetExtension(pathCfgIni);
                return ConvertType.AddonType(extension).ToString();
            }
        }
        //"APP_PACKAGE";//todo real val

        bool isAddAdon { get; set; }

       

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

        internal bool GetIni()//? IniApp iniAllModules вынести в свойство
        {

            //string pathCfgIni = "";// setset.CfgIniPaths[0];//todo add for

            ClsIni clsIni = new ClsIni(pathCfgIni);
            //clsIni.WriteSection("ааа");

            IniModule iniAppsSpds = new IniModule() { ModuleName = "SPDS" };

            IniModule iniAppsMex = new IniModule() { ModuleName = "Mech" };

            IniModule iniAppsDef = new IniModule() { ModuleName = "<<Default>>" };

            IniModule iniAppsUnknown = new IniModule() { ModuleName = "Unknown" };

            iniAllModules = new IniApp();//? module spds def mech

            iniAllModules.Modules.Add(iniAppsUnknown);
            iniAllModules.Modules.Add(iniAppsSpds);
            iniAllModules.Modules.Add(iniAppsMex);
            iniAllModules.Modules.Add(iniAppsDef);

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

                iniAppsSpds.Sections.Add(iniSections);//? заглушка модули по условию
            }



            Console.WriteLine("*************");
            /*****************************
            if ADD
            проверить все ли модули есть

            [\Configuration\SPDS\Appload]	
            [\Configuration\SPDS\Appload\Startup]	

            ****************************/

            //if remove


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
                IEnumerable<IniSection> selectedSections = from p in iniAppsSpds.Sections//? заглушка
                                                           where p.RealName.Contains(patern, StringComparison.InvariantCultureIgnoreCase)
                                                           select p;
                //iniNanos.Modules.Add(selectedSections);
                //IniSection iniSortSections = selectedSections;// new IniSection(selectedSections);
            }



            return true;




        }




        internal bool AddAddon()
        {
            return false;
        }
        internal bool RemoveAddon()
        {
            return false;
        }





    }



  

  

}
