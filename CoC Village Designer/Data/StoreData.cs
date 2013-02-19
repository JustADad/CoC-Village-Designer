namespace JustADadSoftware.VillageDesigner.Data
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType("storeData")]
    public class StoreData
    {
        public static StoreData Initialize(string fileName)
        {
            StoreData storeData = null;

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(StoreData));

                using (System.IO.Stream stream = typeof(StoreData).Assembly.GetManifestResourceStream(fileName))
                {
                    using (XmlReader xmlReader = XmlReader.Create(stream))
                    {
                        storeData = xmlSerializer.Deserialize(xmlReader) as StoreData;
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

            return storeData;
        }

        [XmlArrayItem("screenShot", typeof(string))]
        [XmlArray("screenshots")]
        public string[] Screenshots { get; set; }

        [XmlArrayItem("sectionData", typeof(StoreSection))]
        [XmlArray("sections")]
        public StoreSection[] Sections { get; set; }
    }
}
