using System.Text;
using System.Text.RegularExpressions;

using drz.NanoInstallerFromIni.Ini;
using drz.NanoInstallerFromIni.package;

using KpblcNCadCfgIni;
using KpblcNCadCfgIni.Data;

using KENUM = KpblcNCadCfgIni.Enums;

namespace drz.NanoInstallerFromIni
{

    internal class Program
    {
        //static string pathCfgIni = @"D:\@Developers\В работе\!Текущее\Programmers\!NET\Console_TEST\cfg.ini";
        static string pathCfgIni = @"d:\@Developers\В работе\!Текущее\Programmers\!NET\Console_TEST\@res\cfg.ini";//todo real val
        //static string pathCfgIni = @"C:\Users\razygraevaa\AppData\Roaming\Nanosoft\nanoCAD x64 23.1\Config\cfg.ini";

        static string pathAddRemoveAddon = @"d:\@Developers\В работе\@ss\TBS Plus\TBS nanoCAD 23 Addins\TBS Plus.package";//todo real val
        //static string pathAddRemoveAddonPahch = @"d:\@Developers\В работе\!Текущее\Programmers\!NET\!bundle\drzTools\drzTools.package";
        static bool isAddAdon = false;

        static string appType = "APP_PACKAGE";//todo real val

        static string sectionPatternStart = @"\Configuration\";

        static string sectionPatternEnd = @"\Appload\Startup\app";


        static string[] modules = { "SPDS", "<<Default>>", "Mech" };

        /// <summary>
        /// чтение пакета drzTools.package
        /// </summary>
        static void readPack()
        {

            ApplicationPackage appPack = new ApplicationPackage();
            ApplicationPackageComponents applComp = new ApplicationPackageComponents();
            var ddd = appPack.Components;
        }

        /// <summary>
        /// чтение iNI от Кулик
        /// </summary>
        static void KplINI()
        {
            NCadConfig start = new NCadConfig(pathCfgIni);

            NCadConfiguration config = start.ConfigurationList
                                        .FirstOrDefault(o => string.IsNullOrWhiteSpace(o.ConfigurationName));

            StartupApplication app = new StartupApplication()
            {
                LoadOrder = 0,
                LoaderName = pathAddRemoveAddon,
                AppType = KENUM.AppTypeEnum.MGD
            };

            config.StartupApplicationList.Remove(app);
            start.Save();


            config.StartupApplicationList.Add(app);
            start.Save();
        }

        static string regsect(string o)
        {
            //string o = @" [ \APPPACKAGES ]";

            o = Regex.Replace(o, @"[\\[\]]", "").Trim();

            return o;
        }
        /// <summary>
        /// INI CLS INI
        /// </summary>
        static void ClsINI()
        {
            List<List<AddonInfo>> addoninfoList = new List<List<AddonInfo>>();

            ClsIni clsIni = new ClsIni(pathCfgIni);

            foreach (string app0 in modules)
            {
                List<AddonInfo> sections = clsIni.ReadSections($@"\{app0}{sectionPatternEnd}", $"s{pathAddRemoveAddon}");
                if (sections.Count > 0)
                {
                    addoninfoList.Add(sections);
                }

            }
        }




        /// <summary>
        /// отладка
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
#if NET
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif

            ClsIni clsIni = new ClsIni(pathCfgIni);
            //clsIni.WriteSection("ааа");
            List<string> sections = clsIni.ReadSections();

            IniModule iniAllModules = new IniModule();


            //get INi
            foreach (string section in sections)//? move to module
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
                    iniKeys.KeyValue = clsIni.ReadValue(section, key, "");

                    iniSections.Keys.Add(iniKeys);
                }

                iniAllModules.Sections.Add(iniSections);
            }
            /*****************************
            if ADD
            проверить все ли модули есть
            
            [\Configuration\SPDS\Appload]	
            [\Configuration\SPDS\Appload\Startup]	

            ****************************/

            //if remove


            foreach (IniSection section in iniAllModules.Sections)
            {
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
                IEnumerable<IniSection> selectedSections = from p in iniAllModules.Sections
                                                           where p.RealName.Contains(patern, StringComparison.InvariantCultureIgnoreCase)
                                                           select p;
                //iniNanos.Modules.Add(selectedSections);
                //IniSection iniSortSections = selectedSections;// new IniSection(selectedSections);
            }



            IniModule iniAppsSpds = new IniModule();

            IniModule iniAppsMex = new IniModule();
            IniModule iniAppsOther = new IniModule();







        }



        /*
        static void test()
        {
            var testIniFile = @"#This section provides the general configuration of the application
[GeneralConfiguration] 

#Update rate in msecs
setUpdate = 100

#Maximun errors before quit
setMaxErrors = 2

#Users allowed to access the system
#format: user = pass
[Users]
ricky = rickypass
patty = pattypass ";

            //Create an instance of a ini file parser
            var parser = new IniDataParser();



            // This is a special ini file where we use the '#' character for comment lines
            // instead of ';' so we need to change the configuration of the parser:
            parser.Scheme.CommentString = "#";

            // Here we'll be storing the contents of the ini file we are about to read:
            IniData parsedData = parser.Parse(testIniFile);

            // Write down the contents of the ini file to the console
            Console.WriteLine("---- Printing contents of the INI file ----\n");
            Console.WriteLine(parsedData);
            Console.WriteLine();

            // Get concrete data from the ini file
            Console.WriteLine("---- Printing setMaxErrors value from GeneralConfiguration section ----");
            Console.WriteLine("setMaxErrors = " + parsedData["GeneralConfiguration"]["setMaxErrors"]);
            Console.WriteLine();

            // Modify the INI contents and save
            Console.WriteLine();

            // Modify the loaded ini file
            parsedData["GeneralConfiguration"]["setMaxErrors"] = "10";
            parsedData.Sections.Add("newSection");
            parsedData.Sections.FindByName("newSection").Comments
                .Add("This is a new comment for the section");
            parsedData.Sections.FindByName("newSection").Properties.Add("myNewKey", "value");
            parsedData.Sections.FindByName("newSection").Properties.FindByKey("myNewKey").Comments
            .Add("new key comment");

            // Write down the contents of the modified ini file to the console
            Console.WriteLine("---- Printing contents of the new INI file ----");
            Console.WriteLine(parsedData);
            Console.WriteLine();
        }
        */
    }

    public class IniApp
    {
        public IniApp()
        {
            Modules = new List<IniModule>();
        }
        public string Version { get; set; }
        public List<IniModule> Modules { get; set; }
    }
    public class IniModule
    {
        public IniModule()
        {
            Sections = new List<IniSection>();
        }
        public string AppName { get; set; }
        public List<IniSection> Sections { get; set; }
    }
    public class IniSection
    {
        public IniSection()
        {
            Keys = new List<IniKey>();
        }


        public string Name
        {
            get
            {
                string[] arName = RealName.Split('\\');
                if (arName.Length > 5 && arName[5].Contains("app", StringComparison.InvariantCultureIgnoreCase))
                {
                    arName[5] = "app";
                    return string.Join("\\", arName);
                }
                else
                {
                    return RealName;
                }
            }

        }
        public string RealName { get; set; }
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
                    return "RealName";
                }
            }
        }

        public int item { get; set; }

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
    //    public string AppName { get; set; }
    //    public string Section { get; set; }
    //    public string Loader { get; set; }
    //    public string appType { get; set; }
    //    public string Enabled { get; set; }

    //}
}
