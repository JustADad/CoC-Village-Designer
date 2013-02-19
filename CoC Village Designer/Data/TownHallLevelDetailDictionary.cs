namespace JustADadSoftware.VillageDesigner.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable]
    public class TownHallLevelDetailDictionary : Dictionary<string, TownHallLevelDetail>, IXmlSerializable
    {
        public void AddTownHallLevelDetail(TownHallLevelDetail levelDetail)
        {
            this.Add(levelDetail.BuildingID, levelDetail);
        }

        public bool RemoveTownHallLevelDetail(TownHallLevelDetail levelDetail)
        {
            return this.Remove(levelDetail.BuildingID);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();

            XmlSerializer townHallLevelDetailSerializer = new XmlSerializer(typeof(TownHallLevelDetail));

            while (XmlNodeType.EndElement != reader.NodeType)
            {
                if (XmlNodeType.Element == reader.NodeType)
                {
                    TownHallLevelDetail townHallLevelDetail = townHallLevelDetailSerializer.Deserialize(reader) as TownHallLevelDetail;

                    this.AddTownHallLevelDetail(townHallLevelDetail);
                }
                else
                {
                    reader.MoveToContent();
                }
            }

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer townHallLevelDetailSerializer = new XmlSerializer(typeof(TownHallLevelDetail));

            foreach (string buildingID in this.Keys)
            {
                townHallLevelDetailSerializer.Serialize(writer, this[buildingID]);
            }
        }
    }
}
