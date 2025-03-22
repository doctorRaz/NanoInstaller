using System.Text;

using drz.NanoInstallerFromIni;

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
        static List<string> addonPaths => new List<string>
            {
                @"d:\@Developers\В работе\!Текущее\Programmers\!NET\!bundle\CorrectBlocksNC.package",
                @"d:\@Developers\В работе\!Текущее\Programmers\!NET\!bundle\docProp.package",
                @"d:\@Developers\В работе\!Текущее\Programmers\!NET\!bundle\drz.package",
            };

        static List<string> nanoCfgPaths = new List<string> {
            @"d:\@Developers\В работе\!Текущее\Programmers\!NET\NanoInstaller\@res\cfgAllModules.ini",
            @"d:\@Developers\В работе\!Текущее\Programmers\!NET\NanoInstaller\@res\cfg.ini",
            @"d:\@Developers\В работе\!Текущее\Programmers\!NET\NanoInstaller\@res\cfg (2).ini"
            };

        /// <summary>
        /// отладка
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine(nameof(test.ddd));

#if NET
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif

            bool isAdd = true;

            Repository runer = new Repository(nanoCfgPaths, addonPaths, isAdd);





        }
    }
}
