namespace JustADadSoftware.VillageDesigner.Data
{
    using System;
    using System.Windows.Media;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType("buildingData")]
    public class BuildingData
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("maxRange")]
        public int MaxRange { get; set; }

        [XmlAttribute("minRange")]
        public int MinRange { get; set; }

        [XmlElement("buildingColor")]
        public Color BuildingColor { get; set; }
    }
}
