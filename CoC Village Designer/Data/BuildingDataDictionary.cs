namespace JustADadSoftware.VillageDesigner.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("buildingDataDictionary")]
    public class BuildingDataDictionary : Dictionary<string, BuildingData>, IXmlSerializable
    {
        public static BuildingDataDictionary Initialize(string fileName)
        {
            BuildingDataDictionary buildingDataDictionary = null;

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(BuildingDataDictionary));

                using (System.IO.Stream stream = typeof(StoreData).Assembly.GetManifestResourceStream(fileName))
                {
                    using (XmlReader xmlReader = XmlReader.Create(stream))
                    {
                        buildingDataDictionary = xmlSerializer.Deserialize(xmlReader) as BuildingDataDictionary;
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

            return buildingDataDictionary;
        }

        public void AddBuildingData(BuildingData buildingData)
        {
            this.Add(buildingData.ID, buildingData);
        }

        public bool RemoveBuildingData(BuildingData buildingData)
        {
            return this.Remove(buildingData.ID);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();

            XmlSerializer buildingDataSerializer = new XmlSerializer(typeof(BuildingData));

            while (XmlNodeType.EndElement != reader.NodeType)
            {
                if (XmlNodeType.Element == reader.NodeType)
                {
                    BuildingData buildingData = buildingDataSerializer.Deserialize(reader) as BuildingData;

                    this.Add(buildingData.ID, buildingData);
                }
                else
                {
                    reader.MoveToContent();
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer buildingDataSerializer = new XmlSerializer(typeof(BuildingData));

            foreach (string buildingID in this.Keys)
            {
                buildingDataSerializer.Serialize(writer, this[buildingID]);
            }
        }
    }
}
