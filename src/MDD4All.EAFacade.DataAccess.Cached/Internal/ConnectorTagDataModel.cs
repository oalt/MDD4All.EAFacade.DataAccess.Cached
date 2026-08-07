using MDD4All.EAFacade.DataModels.Contracts;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ConnectorTagDataModel : RepositoryElementDataModel, ConnectorTag
    {
        public ConnectorTagDataModel()
        {
        }

        public ConnectorTagDataModel(XElement tConnectorTagRow, Repository repository)
        {
            Repository = repository;

            _name = tConnectorTagRow.Element("Property").Value;
            _notes = tConnectorTagRow.Element("NOTES").Value;
            _value = tConnectorTagRow.Element("VALUE").Value;
            _tagID = int.Parse(tConnectorTagRow.Element("PropertyID").Value);
            _tagGUID = tConnectorTagRow.Element("ea_guid").Value;
            _connectorID = int.Parse(tConnectorTagRow.Element("ElementID").Value);
        }

        public ConnectorTagDataModel(EAAPI.ConnectorTag apiConnectorTag)
        {
            _apiConnectorTag = apiConnectorTag;

            _name = apiConnectorTag.Name;
            _notes = apiConnectorTag.Notes;
            _value = apiConnectorTag.Value;
            _connectorID = apiConnectorTag.ConnectorID;
            _tagID = apiConnectorTag.TagID;
            _tagGUID = apiConnectorTag.TagGUID;
        }

        private EAAPI.ConnectorTag? _apiConnectorTag;

        private EAAPI.ConnectorTag? ApiConnectorTag
        {
            get
            {
                if (_apiConnectorTag == null && !string.IsNullOrEmpty(TagGUID))
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        EAAPI.Connector apiConnector = apiRepository.GetConnectorByID(ConnectorID);

                        if (apiConnector != null)
                        {
                            for (short index = 0; index < apiConnector.TaggedValues.Count; index++)
                            {
                                EAAPI.ConnectorTag currentConnectorTag = (EAAPI.ConnectorTag)apiConnector.TaggedValues.GetAt(index);

                                if (currentConnectorTag.TagGUID == TagGUID)
                                {
                                    _apiConnectorTag = currentConnectorTag;
                                    break;
                                }
                            }
                        }
                    }
                }

                return _apiConnectorTag;
            }
        }

        private int _connectorID;

        public int ConnectorID
        {
            get
            {
                return _connectorID;
            }

            set
            {
                _connectorID = value;

                if (ApiConnectorTag != null)
                {
                    ApiConnectorTag.ConnectorID = value;
                }
            }
        }

        public string FQName => throw new NotImplementedException();

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

                if (ApiConnectorTag != null)
                {
                    ApiConnectorTag.Name = value;
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

                if (ApiConnectorTag != null)
                {
                    ApiConnectorTag.Notes = value;
                }
            }
        }

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otConnectorTag;
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

                if (ApiConnectorTag != null)
                {
                    ApiConnectorTag.TagGUID = value;
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

                if (ApiConnectorTag != null)
                {
                    ApiConnectorTag.Value = value;
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

            if (ApiConnectorTag != null)
            {
                result = ApiConnectorTag.Update();
            }

            return result;
        }
    }
}
