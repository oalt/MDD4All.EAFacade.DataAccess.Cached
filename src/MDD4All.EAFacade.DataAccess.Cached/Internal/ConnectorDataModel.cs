using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ConnectorDataModel : RepositoryElementDataModel, Connector
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ConnectorDataModel()
        {
            TaggedValues = new ConnectorTagCollection(this);
        }

        public ConnectorDataModel(XElement tConnectorQueryRow, Repository repository)
        {
            TaggedValues = new ConnectorTagCollection(this);

            Repository = repository;

            try
            {
                ConnectorID = int.Parse(tConnectorQueryRow.Element("Connector_ID").Value);
                Name = tConnectorQueryRow.Element("Name").Value;
                Direction = tConnectorQueryRow.Element("Direction").Value;
                Notes = tConnectorQueryRow.Element("Notes").Value;
                Type = tConnectorQueryRow.Element("Connector_Type").Value;
                Subtype = tConnectorQueryRow.Element("SubType").Value;
                ClientID = int.Parse(tConnectorQueryRow.Element("Start_Object_ID").Value);
                SupplierID = int.Parse(tConnectorQueryRow.Element("End_Object_ID").Value);

                Stereotype = tConnectorQueryRow.Element("Stereotype").Value;

                ConnectorGUID = tConnectorQueryRow.Element("ea_guid").Value;

                StartPointX = int.Parse(tConnectorQueryRow.Element("PtStartX").Value);
                StartPointY = int.Parse(tConnectorQueryRow.Element("PtStartY").Value);
                EndPointX = int.Parse(tConnectorQueryRow.Element("PtEndX").Value);
                EndPointY = int.Parse(tConnectorQueryRow.Element("PtEndY").Value);
                SequenceNo = int.Parse(tConnectorQueryRow.Element("SeqNo").Value);
                RouteStyle = int.Parse(tConnectorQueryRow.Element("RouteStyle").Value);
                Color = int.Parse(tConnectorQueryRow.Element("LineColor").Value);
                DiagramID = int.Parse(tConnectorQueryRow.Element("DiagramID").Value);

                VirtualInheritance = tConnectorQueryRow.Element("VirtualInheritance").Value;
                StateFlags = tConnectorQueryRow.Element("StateFlags").Value;
                StyleEx = tConnectorQueryRow.Element("StyleEx").Value;
                EventFlags = tConnectorQueryRow.Element("EventFlags").Value;

                IsRoot = tConnectorQueryRow.Element("IsRoot").Value == "1";
                IsLeaf = tConnectorQueryRow.Element("IsLeaf").Value == "1";
                IsSpec = tConnectorQueryRow.Element("IsSpec").Value == "1";

                ClientEnd = new ConnectorEndDataModel(tConnectorQueryRow, "Source", repository, ConnectorID);
                SupplierEnd = new ConnectorEndDataModel(tConnectorQueryRow, "Destination", repository, ConnectorID);

            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public ConnectorDataModel(EAAPI.Connector apiConnector)
        {
            TaggedValues = new ConnectorTagCollection(this);

            _apiConnector = apiConnector;

            ConnectorID = apiConnector.ConnectorID;
            Name = apiConnector.Name;
            Direction = apiConnector.Direction;
            Notes = apiConnector.Notes;
            Type = apiConnector.Type;
            Subtype = apiConnector.Subtype;
            ClientID = apiConnector.ClientID;
            SupplierID = apiConnector.SupplierID;

            Stereotype = apiConnector.Stereotype;

            ConnectorGUID = apiConnector.ConnectorGUID;

            StartPointX = apiConnector.StartPointX;
            StartPointY = apiConnector.StartPointY;
            EndPointX = apiConnector.EndPointX;
            EndPointY = apiConnector.EndPointY;
            SequenceNo = apiConnector.SequenceNo;
            RouteStyle = apiConnector.RouteStyle;
            Color = apiConnector.Color;
            DiagramID = apiConnector.DiagramID;

            VirtualInheritance = apiConnector.VirtualInheritance;
            StateFlags = apiConnector.StateFlags;
            StyleEx = apiConnector.StyleEx;
            EventFlags = apiConnector.EventFlags;

            IsRoot = apiConnector.IsRoot;
            IsLeaf = apiConnector.IsLeaf;
            IsSpec = apiConnector.IsSpec;

            ClientEnd = new ConnectorEndDataModel(apiConnector.ClientEnd);
            SupplierEnd = new ConnectorEndDataModel(apiConnector.SupplierEnd);
        }

        private EAAPI.Connector? _apiConnector;

        private EAAPI.Connector? ApiConnector
        {
            get
            {
                if (_apiConnector == null)
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        _apiConnector = apiRepository.GetConnectorByID(ConnectorID);
                    }
                }

                return _apiConnector;
            }
        }

        private string _alias = "";

        public string Alias
        {
            get
            {
                return _alias;
            }

            set
            {
                _alias = value;

                if (ApiConnector != null)
                {
                    ApiConnector.Alias = value;
                }
            }
        }

        public Element AssociationClass => throw new NotImplementedException();

        public ConnectorEnd ClientEnd { get; set; } = new ConnectorEndDataModel();

        private int _clientID;

        public int ClientID
        {
            get
            {
                return _clientID;
            }

            set
            {
                _clientID = value;

                if (ApiConnector != null)
                {
                    ApiConnector.ClientID = value;
                }
            }
        }

        private int _color;

        public int Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;

                if (ApiConnector != null)
                {
                    ApiConnector.Color = value;
                }
            }
        }

        public string ConnectorGUID { get; private set; }

        public int ConnectorID { get; private set; }

        public Collection Constraints => throw new NotImplementedException();

        public Collection ConveyedItems => throw new NotImplementedException();

        public Collection CustomProperties => throw new NotImplementedException();

        private int _diagramID;

        public int DiagramID
        {
            get
            {
                return _diagramID;
            }

            set
            {
                _diagramID = value;

                if (ApiConnector != null)
                {
                    ApiConnector.DiagramID = value;
                }
            }
        }

        private string _direction = "";

        public string Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                _direction = value;

                if (ApiConnector != null)
                {
                    ApiConnector.Direction = value;
                }
            }
        }

        private int _endPointX;

        public int EndPointX
        {
            get
            {
                return _endPointX;
            }

            set
            {
                _endPointX = value;

                if (ApiConnector != null)
                {
                    ApiConnector.EndPointX = value;
                }
            }
        }

        private int _endPointY;

        public int EndPointY
        {
            get
            {
                return _endPointY;
            }

            set
            {
                _endPointY = value;

                if (ApiConnector != null)
                {
                    ApiConnector.EndPointY = value;
                }
            }
        }

        private string _eventFlags = "";

        public string EventFlags
        {
            get
            {
                return _eventFlags;
            }

            set
            {
                _eventFlags = value;

                if (ApiConnector != null)
                {
                    ApiConnector.EventFlags = value;
                }
            }
        }

        public string ForeignKeyInformation => throw new NotImplementedException();

        public string FQStereotype => throw new NotImplementedException();

        private bool _isLeaf;

        public bool IsLeaf
        {
            get
            {
                return _isLeaf;
            }

            set
            {
                _isLeaf = value;

                if (ApiConnector != null)
                {
                    ApiConnector.IsLeaf = value;
                }
            }
        }

        private bool _isRoot;

        public bool IsRoot
        {
            get
            {
                return _isRoot;
            }

            set
            {
                _isRoot = value;

                if (ApiConnector != null)
                {
                    ApiConnector.IsRoot = value;
                }
            }
        }

        private bool _isSpec;

        public bool IsSpec
        {
            get
            {
                return _isSpec;
            }

            set
            {
                _isSpec = value;

                if (ApiConnector != null)
                {
                    ApiConnector.IsSpec = value;
                }
            }
        }

        public string MessageArguments => throw new NotImplementedException();

        private string _metaType = "";

        public string MetaType
        {
            get
            {
                return _metaType;
            }

            set
            {
                _metaType = value;

                if (ApiConnector != null)
                {
                    ApiConnector.MetaType = value;
                }
            }
        }

        public string MiscData => throw new NotImplementedException();

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

                if (ApiConnector != null)
                {
                    ApiConnector.Name = value;
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

                if (ApiConnector != null)
                {
                    ApiConnector.Notes = value;
                }
            }
        }

        public ObjectType ObjectType { get; } = ObjectType.otConnector;

        public Properties Properties => throw new NotImplementedException();

        public string ReturnValueAlias => throw new NotImplementedException();

        private int _routeStyle;

        public int RouteStyle
        {
            get
            {
                return _routeStyle;
            }

            set
            {
                _routeStyle = value;

                if (ApiConnector != null)
                {
                    ApiConnector.RouteStyle = value;
                }
            }
        }

        private int _sequenceNo;

        public int SequenceNo
        {
            get
            {
                return _sequenceNo;
            }

            set
            {
                _sequenceNo = value;

                if (ApiConnector != null)
                {
                    ApiConnector.SequenceNo = value;
                }
            }
        }

        private int _startPointX;

        public int StartPointX
        {
            get
            {
                return _startPointX;
            }

            set
            {
                _startPointX = value;

                if (ApiConnector != null)
                {
                    ApiConnector.StartPointX = value;
                }
            }
        }

        private int _startPointY;

        public int StartPointY
        {
            get
            {
                return _startPointY;
            }

            set
            {
                _startPointY = value;

                if (ApiConnector != null)
                {
                    ApiConnector.StartPointY = value;
                }
            }
        }

        private string _stateFlags = "";

        public string StateFlags
        {
            get
            {
                return _stateFlags;
            }

            set
            {
                _stateFlags = value;

                if (ApiConnector != null)
                {
                    ApiConnector.StateFlags = value;
                }
            }
        }

        private string _stereotype = "";

        public string Stereotype
        {
            get
            {
                return _stereotype;
            }

            set
            {
                _stereotype = value;

                if (ApiConnector != null)
                {
                    ApiConnector.Stereotype = value;
                }
            }
        }

        private string _stereotypeEx = "";

        public string StereotypeEx
        {
            get
            {
                return _stereotypeEx;
            }

            set
            {
                _stereotypeEx = value;

                if (ApiConnector != null)
                {
                    ApiConnector.StereotypeEx = value;
                }
            }
        }

        private string _styleEx = "";

        public string StyleEx
        {
            get
            {
                return _styleEx;
            }

            set
            {
                _styleEx = value;

                if (ApiConnector != null)
                {
                    ApiConnector.StyleEx = value;
                }
            }
        }

        private string _subtype = "";

        public string Subtype
        {
            get
            {
                return _subtype;
            }

            set
            {
                _subtype = value;

                if (ApiConnector != null)
                {
                    ApiConnector.Subtype = value;
                }
            }
        }

        public ConnectorEnd SupplierEnd { get; set; } = new ConnectorEndDataModel();

        private int _supplierID;

        public int SupplierID
        {
            get
            {
                return _supplierID;
            }

            set
            {
                _supplierID = value;

                if (ApiConnector != null)
                {
                    ApiConnector.SupplierID = value;
                }
            }
        }

        public GenericCollection<ConnectorTag> TaggedValues { get; set; }

        public Collection TemplateBindings => throw new NotImplementedException();

        private string _transitionAction = "";

        public string TransitionAction
        {
            get
            {
                return _transitionAction;
            }

            set
            {
                _transitionAction = value;

                if (ApiConnector != null)
                {
                    ApiConnector.TransitionAction = value;
                }
            }
        }

        private string _transitionEvent = "";

        public string TransitionEvent
        {
            get
            {
                return _transitionEvent;
            }

            set
            {
                _transitionEvent = value;

                if (ApiConnector != null)
                {
                    ApiConnector.TransitionEvent = value;
                }
            }
        }

        private string _transitionGuard = "";

        public string TransitionGuard
        {
            get
            {
                return _transitionGuard;
            }

            set
            {
                _transitionGuard = value;

                if (ApiConnector != null)
                {
                    ApiConnector.TransitionGuard = value;
                }
            }
        }

        private string _type = "";

        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;

                if (ApiConnector != null)
                {
                    ApiConnector.Type = value;
                }
            }
        }

        private string _virtualInheritance = "";

        public string VirtualInheritance
        {
            get
            {
                return _virtualInheritance;
            }

            set
            {
                _virtualInheritance = value;

                if (ApiConnector != null)
                {
                    ApiConnector.VirtualInheritance = value;
                }
            }
        }

        private int _width;

        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;

                if (ApiConnector != null)
                {
                    ApiConnector.Width = value;
                }
            }
        }

        public string GetLastError()
        {
            throw new NotImplementedException();
        }

        public bool IsConnectorValid()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            bool result = true;

            if (ApiConnector != null)
            {
                result = ApiConnector.Update();
            }

            return result;
        }
    }
}
