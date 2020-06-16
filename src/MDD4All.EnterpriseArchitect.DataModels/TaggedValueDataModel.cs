using MDD4All.EnterpriseArchitect.DataModels.Contracts;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EnterpriseArchitect.DataModels
{
    public class TaggedValueDataModel : TaggedValue
    {
        public TaggedValueDataModel()
        {

        }

        public TaggedValueDataModel(XElement tObjectPropertiesRow)
        {
            Name = tObjectPropertiesRow.Element("Property").Value;
            Notes = tObjectPropertiesRow.Element("Notes").Value;
            Value = tObjectPropertiesRow.Element("Value").Value;
            PropertyID = int.Parse(tObjectPropertiesRow.Element("PropertyID").Value);
            PropertyGUID = tObjectPropertiesRow.Element("ea_guid").Value;
            ElementID = int.Parse(tObjectPropertiesRow.Element("Object_ID").Value);
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

            return false;
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
