using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class TaggedValueDataModel : RepositoryElementDataModel, TaggedValue
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TaggedValueDataModel()
        {

        }

        public TaggedValueDataModel(XElement tObjectPropertiesRow, Repository repository)
        {
            Repository = repository;

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
            _apiTaggedValue = apiTaggedValue;

            Name = apiTaggedValue.Name;
            Notes = apiTaggedValue.Notes;
            Value = apiTaggedValue.Value;
            ElementID = apiTaggedValue.ElementID;
            PropertyID = apiTaggedValue.PropertyID;
            PropertyGUID = apiTaggedValue.PropertyGUID;
        }

        private EAAPI.TaggedValue? _apiTaggedValue;

        private EAAPI.TaggedValue? ApiTaggedValue
        {
            get
            {
                if (_apiTaggedValue == null && !string.IsNullOrEmpty(PropertyGUID))
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        EAAPI.Element apiElement = apiRepository.GetElementByID(ElementID);

                        if (apiElement != null)
                        {
                            for (short index = 0; index < apiElement.TaggedValues.Count; index++)
                            {
                                EAAPI.TaggedValue currentTaggedValue = (EAAPI.TaggedValue)apiElement.TaggedValues.GetAt(index);

                                if (currentTaggedValue.PropertyGUID == PropertyGUID)
                                {
                                    _apiTaggedValue = currentTaggedValue;
                                    break;
                                }
                            }
                        }
                    }
                }

                return _apiTaggedValue;
            }
        }

        private string _name = "";

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;

                if (ApiTaggedValue != null)
                {
                    ApiTaggedValue.Name = value;
                }
            }
        }

        private string _value = "";

        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;

                if (ApiTaggedValue != null)
                {
                    ApiTaggedValue.Value = value;
                }
            }
        }

        private string _notes = "";

        public string Notes
        {
            get
            {
                return _notes;
            }

            set
            {
                _notes = value;

                if (ApiTaggedValue != null)
                {
                    ApiTaggedValue.Notes = value;
                }
            }
        }

        private int _elementID;

        public int ElementID
        {
            get
            {
                return _elementID;
            }

            set
            {
                _elementID = value;

                if (ApiTaggedValue != null)
                {
                    ApiTaggedValue.ElementID = value;
                }
            }
        }

        public string FQName { get; set; } = "";

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otTaggedValue;
            }
        }

        public int ParentID => throw new NotImplementedException();

        private string _propertyGUID = "";

        public string PropertyGUID
        {
            get
            {
                return _propertyGUID;
            }

            set
            {
                _propertyGUID = value;

                if (ApiTaggedValue != null)
                {
                    ApiTaggedValue.PropertyGUID = value;
                }
            }
        }

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
            bool result = true;

            if (ApiTaggedValue != null)
            {
                result = ApiTaggedValue.Update();
            }

            return result;
        }
    }
}
