namespace JustADadSoftware.VillageDesigner.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("townHallLevels")]
    public class TownHallLevels : Dictionary<int, TownHallLevel>, IXmlSerializable
    {
        public static TownHallLevels Initialize(string fileName)
        {
            TownHallLevels townHallLevels = null;

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TownHallLevels));
                
                using (System.IO.Stream stream = typeof(StoreData).Assembly.GetManifestResourceStream(fileName))
                {
                    using (XmlReader xmlReader = XmlReader.Create(stream))
                    {
                        townHallLevels = xmlSerializer.Deserialize(xmlReader) as TownHallLevels;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);

#if DEBUG
                throw;
#endif
            }

            return townHallLevels;
        }

        public void AddTownHallLevel(TownHallLevel townHallLevel)
        {
            this.Add(townHallLevel.Level, townHallLevel);
        }

        public bool RemoveTownHallLevel(TownHallLevel townHallLevel)
        {
            return this.Remove(townHallLevel.Level);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();

            XmlSerializer townHallLevelSerializer = new XmlSerializer(typeof(TownHallLevel));

            while (XmlNodeType.EndElement != reader.NodeType)
            {
                if (XmlNodeType.Element == reader.NodeType)
                {
                    TownHallLevel townHallLevel = townHallLevelSerializer.Deserialize(reader) as TownHallLevel;

                    this.AddTownHallLevel(townHallLevel);
                }
                else
                {
                    reader.MoveToContent();
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer townHallLevelSerializer = new XmlSerializer(typeof(TownHallLevel));

            foreach (int townHallLevel in this.Keys)
            {
                townHallLevelSerializer.Serialize(writer, this[townHallLevel]);
            }
        }
    }
}
