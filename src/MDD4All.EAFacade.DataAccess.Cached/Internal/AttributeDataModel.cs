using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using Attribute = MDD4All.EAFacade.DataModels.Contracts.Attribute;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class AttributeDataModel : Attribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AttributeDataModel()
        {
        }

        public AttributeDataModel(XElement attributeRow)
        {
            try
            {
                Name = attributeRow.Element("Name").Value;
                Notes = attributeRow.Element("Notes").Value;
                Type = attributeRow.Element("Type").Value;
                AttributeID = int.Parse(attributeRow.Element("ID").Value);
                Pos = int.Parse(attributeRow.Element("Pos").Value);
                Visibility = attributeRow.Element("Scope").Value;
                AttributeGUID = attributeRow.Element("ea_guid").Value;
                Default = attributeRow.Element("Default").Value;
                ClassifierID = int.Parse(attributeRow.Element("Classifier").Value);
            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public string Alias { get; set; } = "";

        public bool AllowDuplicates { get; set; }

        public string AttributeGUID { get; set; }

        public int AttributeID { get; private set; }

        public int ClassifierID { get; set; }

        public Collection Constraints => throw new NotImplementedException();

        public string Container { get; set; } = "";

        public string Containment { get; set; } = "";

        public string Default { get; set; } = "";

        public string FQStereotype => throw new NotImplementedException();

        public bool IsCollection { get; set; }

        public bool IsConst { get; set; }

        public bool IsDerived { get; set; }

        public bool IsID { get; set; }

        public bool IsOrdered { get; set; }

        public bool IsStatic { get; set; }

        public string Length { get; set; } = string.Empty;

        public string LowerBound { get; set; } = "";

        public string Name { get; set; } = "";

        public string Notes { get; set; } = "";

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otAttribute;
            }
        }

        public int ParentID => throw new NotImplementedException();

        public int Pos { get; set; }

        public string Precision { get; set; } = "";

        public string RedefinedProperty { get; set; }

        public string Scale { get; set; } = "";

        public string Stereotype { get; set; } = "";

        public string StereotypeEx { get; set; } = "";

        public string Style { get; set; } = "";

        public string StyleEx { get; set; } = "";

        public string SubsettedProperty { get; set; } = "";

        public Collection TaggedValues => throw new NotImplementedException();

        public Collection TaggedValuesEx => throw new NotImplementedException();

        public string Type { get; set; } = "";

        public string UpperBound { get; set; } = "";

        public string Visibility { get; set; } = "";

        public string GetLastError()
        {
            string result = string.Empty;

            return result;
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
