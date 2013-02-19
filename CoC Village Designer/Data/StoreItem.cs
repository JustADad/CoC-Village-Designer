namespace JustADadSoftware.VillageDesigner.Data
{
    using System;
    using System.Windows;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType("storeItem")]
    public class StoreItem
    {
        [XmlAttribute("itemName")]
        public string ItemName { get; set; }

        [XmlAttribute("sectionScreenIndex")]
        public int SectionScreenIndex { get; set; }

        [XmlElement("screenshotViewbox")]
        public Rect ScreenshotViewbox { get; set; }

        [XmlElement("buildingID")]
        public string BuildingID { get; set; }
    }
}
