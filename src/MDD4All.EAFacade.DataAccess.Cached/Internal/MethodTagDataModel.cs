using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class MethodTagDataModel : RepositoryElementDataModel, MethodTag
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public MethodTagDataModel()
        {
        }

        public MethodTagDataModel(XElement operationTagRow, Repository repository)
        {
            Repository = repository;

            try
            {
                Name = operationTagRow.Element("Property").Value;
                Notes = operationTagRow.Element("NOTES").Value;
                Value = operationTagRow.Element("VALUE").Value;
                TagID = int.Parse(operationTagRow.Element("PropertyID").Value);
                TagGUID = operationTagRow.Element("ea_guid").Value;
                MethodID = int.Parse(operationTagRow.Element("ElementID").Value);
            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public MethodTagDataModel(EAAPI.MethodTag apiMethodTag)
        {
            _apiMethodTag = apiMethodTag;

            Name = apiMethodTag.Name;
            Notes = apiMethodTag.Notes;
            Value = apiMethodTag.Value;
            MethodID = apiMethodTag.MethodID;
            TagID = apiMethodTag.TagID;
            TagGUID = apiMethodTag.TagGUID;
        }

        internal int ParentElementID { get; set; }

        private EAAPI.MethodTag? _apiMethodTag;

        private EAAPI.MethodTag? ApiMethodTag
        {
            get
            {
                if (_apiMethodTag == null && !string.IsNullOrEmpty(TagGUID))
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        EAAPI.Element? apiElement = apiRepository.GetElementByID(ParentElementID);

                        if (apiElement != null)
                        {
                            for (short methodIndex = 0; methodIndex < apiElement.Methods.Count; methodIndex++)
                            {
                                EAAPI.Method currentMethod = (EAAPI.Method)apiElement.Methods.GetAt(methodIndex);

                                if (currentMethod.MethodID == MethodID)
                                {
                                    for (short tagIndex = 0; tagIndex < currentMethod.TaggedValues.Count; tagIndex++)
                                    {
                                        EAAPI.MethodTag currentMethodTag = (EAAPI.MethodTag)currentMethod.TaggedValues.GetAt(tagIndex);

                                        if (currentMethodTag.TagGUID == TagGUID)
                                        {
                                            _apiMethodTag = currentMethodTag;
                                            break;
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }

                return _apiMethodTag;
            }
        }

        public string FQName => throw new NotImplementedException();

        public int MethodID { get; set; }

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

                if (ApiMethodTag != null)
                {
                    ApiMethodTag.Name = value;
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

                if (ApiMethodTag != null)
                {
                    ApiMethodTag.Notes = value;
                }
            }
        }

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otMethodTag;
            }
        }

        private string _tagGUID = "";

        public string TagGUID
        {
            get
            {
                return _tagGUID;
            }

            set
            {
                _tagGUID = value;

                if (ApiMethodTag != null)
                {
                    ApiMethodTag.TagGUID = value;
                }
            }
        }

        public int TagID { get; private set; }

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

                if (ApiMethodTag != null)
                {
                    ApiMethodTag.Value = value;
                }
            }
        }

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

            if (ApiMethodTag != null)
            {
                result = ApiMethodTag.Update();
            }

            return result;
        }
    }
}
