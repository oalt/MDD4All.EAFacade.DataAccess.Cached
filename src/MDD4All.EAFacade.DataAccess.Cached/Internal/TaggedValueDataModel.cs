using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class TaggedValueDataModel : TaggedValue
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TaggedValueDataModel()
        {

        }

        public TaggedValueDataModel(XElement tObjectPropertiesRow)
        {
            try
            {
                Name = tObjectPropertiesRow.Element("Property").Value;
                Notes = tObjectPropertiesRow.Element("Notes").Value;
                Value = tObjectPropertiesRow.Element("Value").Value;
                PropertyID = int.Parse(tObjectPropertiesRow.Element("PropertyID").Value);
                PropertyGUID = tObjectPropertiesRow.Element("ea_guid").Value;
                ElementID = int.Parse(tObjectPropertiesRow.Element("Object_ID").Value);
            }
            catch(Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public TaggedValueDataModel(EAAPI.TaggedValue apiTaggedValue)
        {
            Name = apiTaggedValue.Name;
            Notes = apiTaggedValue.Notes;
            Value = apiTaggedValue.Value;
            ElementID = apiTaggedValue.ElementID;
            PropertyID = apiTaggedValue.PropertyID;
            PropertyGUID = apiTaggedValue.PropertyGUID;
        }

        public string Name { get; set; } = "";

        public string Value { get; set; } = "";

        public string Notes { get; set; } = "";

        public int ElementID { get; set; }

        public string FQName { get; set; } = "";

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otTaggedValue;
            }
        }

        public int ParentID => throw new NotImplementedException();

        public string PropertyGUID { get; set; } = "";

        public int PropertyID { get; set; }

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
