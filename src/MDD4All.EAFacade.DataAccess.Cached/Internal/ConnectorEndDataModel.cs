using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ConnectorEndDataModel : RepositoryElementDataModel, ConnectorEnd
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private int _connectorID;

        private bool _isClientEnd;

        public ConnectorEndDataModel()
        {
        }

        public ConnectorEndDataModel(XElement tConnectorQueryRow, string endType, Repository repository, int connectorID)
        {
            Repository = repository;
            _connectorID = connectorID;
            _isClientEnd = (endType == "Source");

            try
            {
                if (endType == "Source")
                {
                    Role = tConnectorQueryRow.Element("SourceRole").Value;
                    Aggregation = int.Parse(tConnectorQueryRow.Element("SourceIsAggregate").Value);
                    Cardinality = tConnectorQueryRow.Element("SourceCard").Value;

                    string style = tConnectorQueryRow.Element("SourceStyle").Value;

                    ConnectorStyleDataModel styleData = new ConnectorStyleDataModel(style);

                    if(styleData.StyleData.ContainsKey("Derived"))
                    {
                        string derivedString = styleData.StyleData["Derived"];
                        if (derivedString != "0")
                        {
                            Derived = true;
                        }
                    }
                    if (styleData.StyleData.ContainsKey("AllowDuplicates"))
                    {
                        string derivedString = styleData.StyleData["AllowDuplicates"];
                        if (derivedString != "0")
                        {
                            AllowDuplicates = true;
                        }
                    }


                }
                else if(endType == "Destination")
                {
                    Role = tConnectorQueryRow.Element("DestRole").Value;
                    Aggregation = int.Parse(tConnectorQueryRow.Element("DestIsAggregate").Value);
                    Cardinality = tConnectorQueryRow.Element("DestCard").Value;

                    string style = tConnectorQueryRow.Element("DestStyle").Value;

                    ConnectorStyleDataModel styleData = new ConnectorStyleDataModel(style);

                    if (styleData.StyleData.ContainsKey("Derived"))
                    {
                        string derivedString = styleData.StyleData["Derived"];
                        if (derivedString != "0")
                        {
                            Derived = true;
                        }
                    }
                    if (styleData.StyleData.ContainsKey("AllowDuplicates"))
                    {
                        string derivedString = styleData.StyleData["AllowDuplicates"];
                        if (derivedString != "0")
                        {
                            AllowDuplicates = true;
                        }
                    }
                }


            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public ConnectorEndDataModel(EAAPI.ConnectorEnd connectorEnd)
        {
            _apiConnectorEnd = connectorEnd;

            Role = connectorEnd.Role;
            Aggregation = connectorEnd.Aggregation;
            Cardinality = connectorEnd.Cardinality;
            Derived = connectorEnd.Derived;
            AllowDuplicates = connectorEnd.AllowDuplicates;
        }

        private EAAPI.ConnectorEnd? _apiConnectorEnd;

        private EAAPI.ConnectorEnd? ApiConnectorEnd
        {
            get
            {
                if (_apiConnectorEnd == null)
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        EAAPI.Connector apiConnector = apiRepository.GetConnectorByID(_connectorID);

                        if (apiConnector != null)
                        {
                            _apiConnectorEnd = _isClientEnd ? apiConnector.ClientEnd : apiConnector.SupplierEnd;
                        }
                    }
                }

                return _apiConnectorEnd;
            }
        }

        public string End => throw new NotImplementedException();

        private string _cardinality = "";

        public string Cardinality
        {
            get
            {
                return _cardinality;
            }

            set
            {
                _cardinality = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Cardinality = value;
                }
            }
        }

        private string _visibility = "";

        public string Visibility
        {
            get
            {
                return _visibility;
            }

            set
            {
                _visibility = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Visibility = value;
                }
            }
        }

        private string _role = "";

        public string Role
        {
            get
            {
                return _role;
            }

            set
            {
                _role = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Role = value;
                }
            }
        }

        private string _roleType = "";

        public string RoleType
        {
            get
            {
                return _roleType;
            }

            set
            {
                _roleType = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.RoleType = value;
                }
            }
        }

        private string _roleNote = "";

        public string RoleNote
        {
            get
            {
                return _roleNote;
            }

            set
            {
                _roleNote = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.RoleNote = value;
                }
            }
        }

        private string _containment = "";

        public string Containment
        {
            get
            {
                return _containment;
            }

            set
            {
                _containment = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Containment = value;
                }
            }
        }

        private int _aggregation;

        public int Aggregation
        {
            get
            {
                return _aggregation;
            }

            set
            {
                _aggregation = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Aggregation = value;
                }
            }
        }

        private int _ordering;

        public int Ordering
        {
            get
            {
                return _ordering;
            }

            set
            {
                _ordering = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Ordering = value;
                }
            }
        }

        private string _qualifier = "";

        public string Qualifier
        {
            get
            {
                return _qualifier;
            }

            set
            {
                _qualifier = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Qualifier = value;
                }
            }
        }

        private string _constraint = "";

        public string Constraint
        {
            get
            {
                return _constraint;
            }

            set
            {
                _constraint = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Constraint = value;
                }
            }
        }

        private bool _isNavigable;

        public bool IsNavigable
        {
            get
            {
                return _isNavigable;
            }

            set
            {
                _isNavigable = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.IsNavigable = value;
                }
            }
        }

        private string _isChangeable = "";

        public string IsChangeable
        {
            get
            {
                return _isChangeable;
            }

            set
            {
                _isChangeable = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.IsChangeable = value;
                }
            }
        }

        public Collection TaggedValues => throw new NotImplementedException();

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

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Stereotype = value;
                }
            }
        }

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otConnectorEnd;
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

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.StereotypeEx = value;
                }
            }
        }

        private string _navigable = "";

        public string Navigable
        {
            get
            {
                return _navigable;
            }

            set
            {
                _navigable = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Navigable = value;
                }
            }
        }

        private bool _ownedByClassifier;

        public bool OwnedByClassifier
        {
            get
            {
                return _ownedByClassifier;
            }

            set
            {
                _ownedByClassifier = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.OwnedByClassifier = value;
                }
            }
        }

        private bool _derived;

        public bool Derived
        {
            get
            {
                return _derived;
            }

            set
            {
                _derived = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Derived = value;
                }
            }
        }

        private bool _derivedUnion;

        public bool DerivedUnion
        {
            get
            {
                return _derivedUnion;
            }

            set
            {
                _derivedUnion = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.DerivedUnion = value;
                }
            }
        }

        private bool _allowDuplicates;

        public bool AllowDuplicates
        {
            get
            {
                return _allowDuplicates;
            }

            set
            {
                _allowDuplicates = value;

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.AllowDuplicates = value;
                }
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

                if (ApiConnectorEnd != null)
                {
                    ApiConnectorEnd.Alias = value;
                }
            }
        }

        public bool Update()
        {
            bool result = true;

            if (ApiConnectorEnd != null)
            {
                result = ApiConnectorEnd.Update();
            }

            return result;
        }

        public string GetLastError()
        {
            throw new NotImplementedException();
        }
    }
}
