using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lek18_Miniopgave
{
    internal class RecipientList
    {
        public XmlDocument XmlDocument { get; set; }

        public RecipientList(XmlDocument xmlDocument)
        {
            XmlDocument = xmlDocument;
        }

        public void sendMessages()
        {
            ATCCContentFilter filter = new ATCCContentFilter(XmlDocument);
            filter.filterAndSend();

            AICContentFilter aICContentFilter = new AICContentFilter(XmlDocument);
            aICContentFilter.filterAndSend();

            ACContentFilter aCContentFilter = new ACContentFilter(XmlDocument);
            aCContentFilter.filterAndSend();
        }
    }
}
