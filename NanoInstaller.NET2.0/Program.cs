using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using drz.NanoInstallerFromIni.Enum;
using drz.NanoInstallerFromIni.Ini;
using drz.NanoInstallerFromIni.Instances;
using drz.NanoInstallerFromIni.Servise;

using Microsoft.Win32;

using static drz.NanoInstallerFromIni.Instances.InstanceAppCfg;

namespace drz.NanoInstallerFromIni
{
    enum test
    {

        None,
        test,
        ddd
    }
    internal class Program
    {

        /// <summary>
        /// отладка
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)

        {
#if NET
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif


            ClsIni clsIni = new ClsIni(@"d:\@Developers\В работе\!Текущее\Programmers\!NET\NanoInstaller\@res\test.ini");
            var rr = clsIni.ReadSections();

            clsIni.RemoveSections();

            clsIni.WriteSection("sect10");
            clsIni.WriteValue("sect100", "key", "val0");

            rr = clsIni.ReadSections();


            //***
            bool isAdd = true;//добавлять
            bool isDotNet = true;//только для NET

            //получим пути к нано, пофих стоит он или нет, конфиг существует значит ставим или сносим аддон в конфиге, хуже не будет точно
            InstanceAppCfg instanseAppCfg = new InstanceAppCfg(isDotNet);

            //все аддоны из каталога
            string addonDir = @"d:\@Developers\В работе\!Текущее\Programmers\!NET\!bundle\";
            InstanceAddonCfg instanseAddonCfg = new InstanceAddonCfg(addonDir, AppTypeEnum.APP_PACKAGE);

            //собираем в кучу список аддонов под конфиги, тут потом должен быть GUI
            PrepSettings prepSettings = new PrepSettings(instanseAppCfg.AppCfgs, instanseAddonCfg.AddonCfgs, isAdd);
            SetupSet set = prepSettings.SetupSets;


            NanoInstaller nanoInstaller = new NanoInstaller(prepSettings.SetupSets);

            //test get INI
            nanoInstaller.pathCfg = @"d:\@Developers\В работе\!Текущее\Programmers\!NET\NanoInstaller\@res\cfg.ini";// instanseAppCfg.AppCfgs[0].CfgPath;
            IniApp getini = nanoInstaller.IniAllModules;

#if DEBUG


            foreach (var mod in getini.Modules)
            {
                int iniSec = 0;
                string secName = string.Empty;
                foreach (var sec in mod.Sections)
                {
                    if (sec.IsSectionApp)
                    {
                        secName = sec.Name + iniSec;
                        iniSec++;
                    }
                    else
                    {
                        secName = sec.RealName;
                    }

                    Console.WriteLine($"{secName}");
                    if (sec.Keys.Count > 0)
                    {
                        foreach (var key in sec.Keys)
                        {
                            Console.WriteLine($"{key.KeyName}={key.KeyValue}");
                        }
                    }

                }

            }

            nanoInstaller.IniAllModules = getini;
#endif
            Console.WriteLine("*************************");



            /********************************************************************************************************
            получить  наноконфиги
            получить пакеты или аддоны
            расставить чекбоксы, что ставить что удалить
            запустить исталлер
            ********************************************************************************************************/



            nanoInstaller.Install();//пошли работать


        }
    }
}