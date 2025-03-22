using System.Text.RegularExpressions;

using drz.NanoInstallerFromIni.Ini;
using drz.NanoInstallerFromIni.package;

using KpblcNCadCfgIni;
using KpblcNCadCfgIni.Data;

using KENUM = KpblcNCadCfgIni.Enums;
namespace drz.NanoInstallerFromIni.Example
{
    internal class Examples
    {
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
        /// INI CLS INI
        /// </summary>
        static void ClsINI(string pathCfgIni, string[] modules, string sectionPatternStart, string sectionPatternEnd, string pathAddRemoveAddon)
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
        /// чтение iNI от Кулик
        /// </summary>
        static void KplINI(string pathCfgIni, string pathAddRemoveAddon)
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


   




    }
    internal class UtilitesExamples
    {
        /// <summary>
        /// Regex Test
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        static string regsect(string o)
        {
            //string o = @" [ \APPPACKAGES ]";

            o = Regex.Replace(o, @"[\\[\]]", "").Trim();

            return o;
        }


    }
}
