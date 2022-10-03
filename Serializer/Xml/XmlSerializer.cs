
using Tracer;
using Serializer.Dto;
using System.Xml.Serialization;
using System.Xml.Linq;
using Serializer.Interfaces;
namespace Serializer.Xml
{
    public class XmlSerializer:ITracerResultSerializer<List<ThreadData>>
    {
        public string SerializeResult(List<ThreadData> threads)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer =
                new System.Xml.Serialization.XmlSerializer(typeof(List<ThreadDataView>));

            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, threads.Transform());
                return FormatXml(stringWriter.ToString());
            }
                       
        }
        private static string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                Console.WriteLine("incorrect XML format");
                return xml;
            }
        }
    }
}