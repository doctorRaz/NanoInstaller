namespace drz.NanoInstallerFromIni.Enum
{
    /// <summary> Типы возможных приложений </summary>
    public enum AppTypeEnum
    {
        /// <summary> Неизвестный тип </summary>
        Unknown,
        /// <summary> lisp-приложения </summary>
        LISP,
        /// <summary> package </summary>
        APP_PACKAGE,
        /// <summary> Managed (NET) </summary>
        MGD,
        /// <summary>Файлы NSF (.nsf, .nsc)</summary>
        NSF,

        /// <summary> Модули NRX (.nrx)</summary>
        NRX,

        /// <summary>Модули Teigha (.tx, .dll)</summary>
        TX
    }

    /// <summary>
    /// конверт енум аддона в расширение и наоборот
    /// </summary>
    internal class ConvertType
    {
        /// <summary>
        /// Расширение аддона по енум.
        /// </summary>
        /// <value>
        /// The addon ext.
        /// </value>

        public static string AddonExt(AppTypeEnum appTypeEnum)
        {
            switch (appTypeEnum)
            {
                case AppTypeEnum.LISP: return "lsp";
                case AppTypeEnum.APP_PACKAGE: return "package";
                case AppTypeEnum.MGD: return "dll";
                case AppTypeEnum.NSF: return "nsf";
                case AppTypeEnum.NRX: return "nrx";
                case AppTypeEnum.TX: return "tx";
                default: return "";
            }

        }

        /// <summary>
        /// енум аддона по расширению
        /// </summary>
        /// <value>
        /// The type of the addon.
        /// </value>
        public static AppTypeEnum AddonType(string ext)
        {
            switch (ext)
            {
                case "lsp": return AppTypeEnum.LISP;
                case "package": return AppTypeEnum.APP_PACKAGE;
                case "dll": return AppTypeEnum.MGD;
                case "nsf": return AppTypeEnum.NSF;
                case "nrx": return AppTypeEnum.NRX;
                case "tx": return AppTypeEnum.TX;
                default: return AppTypeEnum.Unknown;
            }
        }
    }
}