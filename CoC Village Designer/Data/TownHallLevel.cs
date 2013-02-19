namespace JustADadSoftware.VillageDesigner.Data
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType("townHallLevel")]
    public class TownHallLevel
    {
        public TownHallLevel()
        {
            this.Details = new TownHallLevelDetailDictionary();
        }

        [XmlAttribute("level")]
        public int Level { get; set; }

        [XmlElement("details")]
        public TownHallLevelDetailDictionary Details { get; set; }

        public TownHallLevel Clone()
        {
            TownHallLevel clone = new TownHallLevel();
            clone.Level = this.Level;

            foreach (string buildingID in this.Details.Keys)
            {
                clone.Details.AddTownHallLevelDetail(this.Details[buildingID].Clone());
            }

            return clone;
        }
    }
}
