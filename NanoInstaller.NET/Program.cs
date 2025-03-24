using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using drz.NanoInstallerFromIni.Enum;
using drz.NanoInstallerFromIni.Ini;
using drz.NanoInstallerFromIni.Instances;
using drz.NanoInstallerFromIni.Servise;

using Microsoft.Win32;

using static drz.NanoInstallerFromIni.Instances.InstanceNano;

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

            List<string> list = new List<string>()
            {
                "Tom",
                "Bob",
                "uk"
            };

            list.Remove("Tom");



            //***
            bool isAdd = true;
            bool isDotNet = true;

            //получим пути к нано, пофих стоит он или нет, конфиг существует значит ставим или сносим аддон в конфиге
            InstanceNano instanseNano = new InstanceNano(isDotNet);

            List<nanoCfg> nanoCfgs = instanseNano.nanoCfgs;

            foreach (nanoCfg n in nanoCfgs)
            {
                Console.WriteLine($"{n.CfgName}\t {n.CfgPath}");

            }
            Console.WriteLine("*************************");
            string addonDir = @"d:\@Developers\В работе\!Текущее\Programmers\!NET\!bundle\";
            InstanceAddon instanseAddon = new InstanceAddon(addonDir, AppTypeEnum.APP_PACKAGE);



            PrepSettings prepSettings = new PrepSettings(instanseNano.nanoCfgs, instanseAddon.AddonPaths, isAdd);
            /********************************************************************************************************
            получить  наноконфиги
            получить пакеты или аддоны
            расставить чекбоксы, что ставить что удалить
            запустить исталлер
            ********************************************************************************************************/



            NanoInstaller nanoInstaller = new NanoInstaller(prepSettings.SetupSets);
            nanoInstaller.Install();//пошли работать


        }
    }
}
