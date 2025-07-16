using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ConnectorTagDataModel : ConnectorTag
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ConnectorTagDataModel() 
        {
        }

        public ConnectorTagDataModel(XElement tConnectorTagRow)
        {
            try
            {
                Name = tConnectorTagRow.Element("Property").Value;
                Notes = tConnectorTagRow.Element("NOTES").Value;
                Value = tConnectorTagRow.Element("VALUE").Value;
                TagID = int.Parse(tConnectorTagRow.Element("PropertyID").Value);
                TagGUID = tConnectorTagRow.Element("ea_guid").Value;
                ConnectorID = int.Parse(tConnectorTagRow.Element("ElementID").Value);
            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public int ConnectorID { get; set; }

        public string FQName => throw new NotImplementedException();

        public string Name { get; set; }

        public string Notes { get; set; }

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otConnectorTag;
            }
        }

        public string TagGUID { get; set; }

        public int TagID { get; private set; }

        public string Value { get; set; }

        public string GetAttribute(string PropName)
        {
            throw new NotImplementedException();
        }

        public string GetLastError()
        {
            string result = "";

            return result;
        }

        public bool HasAttributes()
        {
            bool result = false;

            return result;
        }

        public bool SetAttribute(string PropName, string PropValue)
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
