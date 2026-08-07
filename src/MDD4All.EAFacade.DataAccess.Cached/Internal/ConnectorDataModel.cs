using MDD4All.EAFacade.DataModels.Contracts;
using MDD4All.EAFacade.DataAccess.Cached.Internal.Collections;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ConnectorDataModel : RepositoryElementDataModel, Connector
    {
        public ConnectorDataModel()
        {
            _taggedValues = new ConnectorTagCollection(this);
        }

        public ConnectorDataModel(XElement tConnectorQueryRow, Repository repository)
        {
            _taggedValues = new ConnectorTagCollection(this);

            Repository = repository;

            _connectorID = int.Parse(tConnectorQueryRow.Element("Connector_ID").Value);
            _name = tConnectorQueryRow.Element("Name").Value;
            _direction = tConnectorQueryRow.Element("Direction").Value;
            _notes = tConnectorQueryRow.Element("Notes").Value;
            _type = tConnectorQueryRow.Element("Connector_Type").Value;
            _subtype = tConnectorQueryRow.Element("SubType").Value;
            _clientID = int.Parse(tConnectorQueryRow.Element("Start_Object_ID").Value);
            _supplierID = int.Parse(tConnectorQueryRow.Element("End_Object_ID").Value);

            _stereotype = tConnectorQueryRow.Element("Stereotype").Value;

            _connectorGUID = tConnectorQueryRow.Element("ea_guid").Value;

            _startPointX = int.Parse(tConnectorQueryRow.Element("PtStartX").Value);
            _startPointY = int.Parse(tConnectorQueryRow.Element("PtStartY").Value);
            _endPointX = int.Parse(tConnectorQueryRow.Element("PtEndX").Value);
            _endPointY = int.Parse(tConnectorQueryRow.Element("PtEndY").Value);
            _sequenceNo = int.Parse(tConnectorQueryRow.Element("SeqNo").Value);
            _routeStyle = int.Parse(tConnectorQueryRow.Element("RouteStyle").Value);
            _color = int.Parse(tConnectorQueryRow.Element("LineColor").Value);
            _diagramID = int.Parse(tConnectorQueryRow.Element("DiagramID").Value);

            _virtualInheritance = tConnectorQueryRow.Element("VirtualInheritance").Value;
            _stateFlags = tConnectorQueryRow.Element("StateFlags").Value;
            _styleEx = tConnectorQueryRow.Element("StyleEx").Value;
            _eventFlags = tConnectorQueryRow.Element("EventFlags").Value;

            _isRoot = tConnectorQueryRow.Element("IsRoot").Value == "1";
            _isLeaf = tConnectorQueryRow.Element("IsLeaf").Value == "1";
            _isSpec = tConnectorQueryRow.Element("IsSpec").Value == "1";

            _clientEnd = new ConnectorEndDataModel(tConnectorQueryRow, "Source", repository, ConnectorID);
            _supplierEnd = new ConnectorEndDataModel(tConnectorQueryRow, "Destination", repository, ConnectorID);
        }

        public ConnectorDataModel(EAAPI.Connector apiConnector)
        {
            _taggedValues = new ConnectorTagCollection(this);

            _apiConnector = apiConnector;

            _connectorID = apiConnector.ConnectorID;
            _name = apiConnector.Name;
            _direction = apiConnector.Direction;
            _notes = apiConnector.Notes;
            _type = apiConnector.Type;
            _subtype = apiConnector.Subtype;
            _clientID = apiConnector.ClientID;
            _supplierID = apiConnector.SupplierID;

            _stereotype = apiConnector.Stereotype;

            _connectorGUID = apiConnector.ConnectorGUID;

            _startPointX = apiConnector.StartPointX;
            _startPointY = apiConnector.StartPointY;
            _endPointX = apiConnector.EndPointX;
            _endPointY = apiConnector.EndPointY;
            _sequenceNo = apiConnector.SequenceNo;
            _routeStyle = apiConnector.RouteStyle;
            _color = apiConnector.Color;
            _diagramID = apiConnector.DiagramID;

            _virtualInheritance = apiConnector.VirtualInheritance;
            _stateFlags = apiConnector.StateFlags;
            _styleEx = apiConnector.StyleEx;
            _eventFlags = apiConnector.EventFlags;

            _isRoot = apiConnector.IsRoot;
            _isLeaf = apiConnector.IsLeaf;
            _isSpec = apiConnector.IsSpec;

            _clientEnd = new ConnectorEndDataModel(apiConnector.ClientEnd);
            _supplierEnd = new ConnectorEndDataModel(apiConnector.SupplierEnd);
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

        private ConnectorEnd _clientEnd = new ConnectorEndDataModel();

        public ConnectorEnd ClientEnd
        {
            get
            {
                return _clientEnd;
            }

            set
            {
                _clientEnd = value;
            }
        }

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

        private string _connectorGUID = "";

        public string ConnectorGUID
        {
            get
            {
                return _connectorGUID;
            }

            private set
            {
                _connectorGUID = value;
            }
        }

        private int _connectorID;

        public int ConnectorID
        {
            get
            {
                return _connectorID;
            }

            private set
            {
                _connectorID = value;
            }
        }

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

        private ConnectorEnd _supplierEnd = new ConnectorEndDataModel();

        public ConnectorEnd SupplierEnd
        {
            get
            {
                return _supplierEnd;
            }

            set
            {
                _supplierEnd = value;
            }
        }

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

        private GenericCollection<ConnectorTag> _taggedValues = null!;

        public GenericCollection<ConnectorTag> TaggedValues
        {
            get
            {
                return _taggedValues;
            }

            set
            {
                _taggedValues = value;
            }
        }

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

                // The connector may not have been saved yet when this instance was
                // constructed (see ElementConnectorCollection.AddNew, which returns a
                // connector before its supplier end is set), so ConnectorID/ConnectorGUID
                // can still be their unsaved defaults. Re-sync them once EA has actually
                // assigned real values.
                if (result)
                {
                    ConnectorID = ApiConnector.ConnectorID;
                    ConnectorGUID = ApiConnector.ConnectorGUID;
                }
            }

            return result;
        }
    }
}
