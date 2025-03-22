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
}
