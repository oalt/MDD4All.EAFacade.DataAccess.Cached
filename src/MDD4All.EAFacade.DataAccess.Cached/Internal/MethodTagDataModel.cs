using MDD4All.EAFacade.DataModels.Contracts;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class MethodTagDataModel : RepositoryElementDataModel, MethodTag
    {
        public MethodTagDataModel()
        {
        }

        public MethodTagDataModel(XElement operationTagRow, Repository repository)
        {
            Repository = repository;

            _name = operationTagRow.Element("Property").Value;
            _notes = operationTagRow.Element("NOTES").Value;
            _value = operationTagRow.Element("VALUE").Value;
            _tagID = int.Parse(operationTagRow.Element("PropertyID").Value);
            _tagGUID = operationTagRow.Element("ea_guid").Value;
            _methodID = int.Parse(operationTagRow.Element("ElementID").Value);
        }

        public MethodTagDataModel(EAAPI.MethodTag apiMethodTag)
        {
            _apiMethodTag = apiMethodTag;

            _name = apiMethodTag.Name;
            _notes = apiMethodTag.Notes;
            _value = apiMethodTag.Value;
            _methodID = apiMethodTag.MethodID;
            _tagID = apiMethodTag.TagID;
            _tagGUID = apiMethodTag.TagGUID;
        }

        private int _parentElementID;

        internal int ParentElementID
        {
            get
            {
                return _parentElementID;
            }

            set
            {
                _parentElementID = value;
            }
        }

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

        private int _methodID;

        public int MethodID
        {
            get
            {
                return _methodID;
            }

            set
            {
                _methodID = value;
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

        private int _tagID;

        public int TagID
        {
            get
            {
                return _tagID;
            }

            private set
            {
                _tagID = value;
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
