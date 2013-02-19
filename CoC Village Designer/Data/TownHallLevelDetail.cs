namespace JustADadSoftware.VillageDesigner.Data
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType("townHallLevelDetail")]
    public class TownHallLevelDetail
    {
        [XmlAttribute("buildingID")]
        public string BuildingID { get; set; }

        [XmlAttribute("minAllowed")]
        public int MinAllowed { get; set; }

        [XmlAttribute("maxAllowed")]
        public int MaxAllowed { get; set; }

        [XmlAttribute("maxLevel")]
        public int MaxLevel { get; set; }

        public TownHallLevelDetail Clone()
        {
            TownHallLevelDetail clone = new TownHallLevelDetail();

            clone.BuildingID = this.BuildingID;
            clone.MinAllowed = this.MinAllowed;
            clone.MaxAllowed = this.MaxAllowed;
            clone.MaxLevel = this.MaxLevel;

            return clone;
        }
    }
}
