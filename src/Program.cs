using System;
using System.Collections.Generic;
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
    //enum modules
    //{ 
    //    SPDS,
    //    <<Default>>,
    //    Mech 
    //}

    enum test
    {

        None,
        test10,
        ddd
    }
    internal class Program
    {
        [STAThread]
        /// <summary>
        /// отладка
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)

        {

            string[] appType = AppTypeEnum.GetNames(typeof(AppTypeEnum));

            var extension = "package";
            var s1 = ConvertType.AddonType(extension);
            var ss = appType[(int)s1];

            var s10 = ConvertType.AddonType(extension).ToString();
#if NET
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif

            //***
            bool isChangeAppCfg = true;//менять конфиг да нет 

            bool isAddonAdd = true;//добавлять аддон true/ удалять аддон false

            bool isDotNet = true;//только для NET

            //получим пути к нано, конфиг существует значит ставим или сносим аддон в конфиге
            InstanceAppCfg instanseAppCfg = new InstanceAppCfg(isDotNet);

            //GUI запрос к юзеру на какие App наны ставить
            List<AppCfg> appCfgs = instanseAppCfg.AppCfgs;

            foreach (AppCfg appCfg in appCfgs)
            {
                appCfg.IsChangeAppCfg = isChangeAppCfg;//пока так, ставим на все найденные App
            }

            //все аддоны из каталога
            string addonDir = @"d:\@Developers\В работе\!Текущее\Programmers\!NET\!bundle\";

            //по хорошему это подготовка, потом писать в класс XML при сборке аддона, при установке читать XML
            InstanceAddonCfg instanseAddonCfg = new InstanceAddonCfg(addonDir, AppTypeEnum.APP_PACKAGE);
            List<AddonCfg> addonCfgs = instanseAddonCfg.AddonCfgs;

            foreach (AddonCfg addonCfg in addonCfgs)
            {
                addonCfg.IsAddonAdd = isAddonAdd;//пока так, добавляем аддоны
            }

            //собираем в кучу список аддонов под конфиги
            PrepSettings prepSettings = new PrepSettings(appCfgs, addonCfgs);
            SetupSet setupSet = prepSettings.SetupSets;


            NanoInstaller nanoInstaller = new NanoInstaller(prepSettings.SetupSets);

            //test get INI
            nanoInstaller.pathCfg = @"d:\@Developers\В работе\!Текущее\Programmers\!NET\NanoInstaller\@res\cfg.ini";// instanseAppCfg.AppCfgs[0].CfgPath;
            IniApp iniApp = nanoInstaller.IniAllModules;

#if DEBUG
            var modules = iniApp.Modules;

            foreach (var mod in modules)
            {
                int iniSec = 0;
                foreach (var sec in mod.Sections)
                {
                    string secName = string.Empty;
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

            nanoInstaller.IniAllModules = iniApp;
#endif
            Console.WriteLine("*************************");

            Console.ReadKey();

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