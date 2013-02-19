namespace JustADadSoftware.VillageDesigner.Data
{
    using System;
    using System.Windows.Media;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Windows;

    [Serializable]
    [XmlType("storeSection")]
    public class StoreSection
    {
        [XmlAttribute("sectionName")]
        public string SectionName { get; set; }

        [XmlAttribute("storeScreenIndex")]
        public int StoreScreenIndex { get; set; }

        [XmlElement("screenshotViewbox")]
        public Rect ScreenshotViewbox { get; set; }

        [XmlArrayItem("screenShot", typeof(string))]
        [XmlArray("screenshots")]
        public string[] Screenshots { get; set; }

        [XmlArrayItem("itemData", typeof(StoreItem))]
        [XmlArray("items")]
        public StoreItem[] Items { get; set; }
    }
}
