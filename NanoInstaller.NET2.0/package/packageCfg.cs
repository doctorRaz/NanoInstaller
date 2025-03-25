using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drz.NanoInstallerFromIni.package
{    
    /****************************************************
    класс заготовка для чтения содержимого пакетов
    ****************************************************/
    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "hostApplicationPackage/v01")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "hostApplicationPackage/v01", IsNullable = false)]
    public partial class ApplicationPackage
    {

        private ApplicationPackageComponents componentsField;

        private string nameField;

        /// <remarks/>
        public ApplicationPackageComponents Components
        {
            get
            {
                return this.componentsField;
            }
            set
            {
                this.componentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "hostApplicationPackage/v01")]
    public partial class ApplicationPackageComponents
    {

        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ComponentEntry", typeof(ApplicationPackageComponentsComponentEntry))]
        [System.Xml.Serialization.XmlElementAttribute("ConfigEntry", typeof(ApplicationPackageComponentsConfigEntry))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "hostApplicationPackage/v01")]
    public partial class ApplicationPackageComponentsComponentEntry
    {

        private string appNameField;

        private string moduleNameField;

        private string moduleTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AppName
        {
            get
            {
                return this.appNameField;
            }
            set
            {
                this.appNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ModuleName
        {
            get
            {
                return this.moduleNameField;
            }
            set
            {
                this.moduleNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ModuleType
        {
            get
            {
                return this.moduleTypeField;
            }
            set
            {
                this.moduleTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "hostApplicationPackage/v01")]
    public partial class ApplicationPackageComponentsConfigEntry
    {

        private string fileNameField;

        private string fileTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FileName
        {
            get
            {
                return this.fileNameField;
            }
            set
            {
                this.fileNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FileType
        {
            get
            {
                return this.fileTypeField;
            }
            set
            {
                this.fileTypeField = value;
            }
        }
    }


}
