using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ConnectorTagDataModel : RepositoryElementDataModel, ConnectorTag
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ConnectorTagDataModel()
        {
        }

        public ConnectorTagDataModel(XElement tConnectorTagRow, Repository repository)
        {
            Repository = repository;

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

        public ConnectorTagDataModel(EAAPI.ConnectorTag apiConnectorTag)
        {
            _apiConnectorTag = apiConnectorTag;

            Name = apiConnectorTag.Name;
            Notes = apiConnectorTag.Notes;
            Value = apiConnectorTag.Value;
            ConnectorID = apiConnectorTag.ConnectorID;
            TagID = apiConnectorTag.TagID;
            TagGUID = apiConnectorTag.TagGUID;
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
