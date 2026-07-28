using MDD4All.EAFacade.DataAccess.Cached.Internal.Collections;
using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class MethodDataModel : RepositoryElementDataModel, Method
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public MethodDataModel()
        {
            Parameters = new ParameterCollection(this);
            TaggedValues = new MethodTagCollection(this);
        }

        public MethodDataModel(XElement operationRow, Repository repository)
        {
            Parameters = new ParameterCollection(this);
            TaggedValues = new MethodTagCollection(this);

            Repository = repository;

            try
            {
                MethodID = int.Parse(operationRow.Element("OperationID").Value);
                ParentID = int.Parse(operationRow.Element("Object_ID").Value);
                Name = operationRow.Element("Name").Value;
                Visibility = operationRow.Element("Scope").Value;
                ReturnType = operationRow.Element("Type").Value;
                ReturnIsArray = operationRow.Element("ReturnArray").Value == "1";
                Stereotype = operationRow.Element("Stereotype").Value;
                IsStatic = operationRow.Element("IsStatic").Value == "1";
                Concurrency = operationRow.Element("Concurrency").Value;
                Notes = operationRow.Element("Notes").Value;
                Behavior = operationRow.Element("Behaviour").Value;
                Abstract = operationRow.Element("Abstract").Value == "1";
                IsSynchronized = operationRow.Element("Synchronized").Value == "1";
                Pos = int.Parse(operationRow.Element("Pos").Value);
                IsConst = operationRow.Element("Const").Value == "1";
                Style = operationRow.Element("Style").Value;
                IsPure = operationRow.Element("Pure").Value == "1";
                ClassifierID = operationRow.Element("Classifier").Value;
                Code = operationRow.Element("Code").Value;
                IsRoot = operationRow.Element("IsRoot").Value == "1";
                IsLeaf = operationRow.Element("IsLeaf").Value == "1";
                IsQuery = operationRow.Element("IsQuery").Value == "1";
                StateFlags = operationRow.Element("StateFlags").Value;
                MethodGUID = operationRow.Element("ea_guid").Value;
                StyleEx = operationRow.Element("StyleEx").Value;
            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public MethodDataModel(EAAPI.Method apiMethod)
        {
            Parameters = new ParameterCollection(this);
            TaggedValues = new MethodTagCollection(this);

            _apiMethod = apiMethod;

            MethodID = apiMethod.MethodID;
            ParentID = apiMethod.ParentID;
            Name = apiMethod.Name;
            Visibility = apiMethod.Visibility;
            ReturnType = apiMethod.ReturnType;
            ReturnIsArray = apiMethod.ReturnIsArray;
            Stereotype = apiMethod.Stereotype;
            IsStatic = apiMethod.IsStatic;
            Concurrency = apiMethod.Concurrency;
            Notes = apiMethod.Notes;
            Behavior = apiMethod.Behavior;
            Abstract = apiMethod.Abstract;
            IsSynchronized = apiMethod.IsSynchronized;
            Pos = apiMethod.Pos;
            IsConst = apiMethod.IsConst;
            Style = apiMethod.Style;
            IsPure = apiMethod.IsPure;
            ClassifierID = apiMethod.ClassifierID;
            Code = apiMethod.Code;
            IsRoot = apiMethod.IsRoot;
            IsLeaf = apiMethod.IsLeaf;
            IsQuery = apiMethod.IsQuery;
            StateFlags = apiMethod.StateFlags;
            MethodGUID = apiMethod.MethodGUID;
            StyleEx = apiMethod.StyleEx;
        }

        private EAAPI.Method? _apiMethod;

        internal EAAPI.Method? ApiMethod
        {
            get
            {
                if (_apiMethod == null)
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        EAAPI.Element? apiElement = apiRepository.GetElementByID(ParentID);

                        if (apiElement != null)
                        {
                            for (short index = 0; index < apiElement.Methods.Count; index++)
                            {
                                EAAPI.Method currentMethod = (EAAPI.Method)apiElement.Methods.GetAt(index);

                                if (currentMethod.MethodID == MethodID)
                                {
                                    _apiMethod = currentMethod;
                                    break;
                                }
                            }
                        }
                    }
                }

                return _apiMethod;
            }
        }

        private bool _abstract;

        public bool Abstract
        {
            get
            {
                return _abstract;
            }

            set
            {
                _abstract = value;

                if (ApiMethod != null)
                {
                    ApiMethod.Abstract = value;
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

                if (ApiMethod != null)
                {
                    ApiMethod.Alias = value;
                }
            }
        }

        private string _behavior = "";

        public string Behavior
        {
            get
            {
                return _behavior;
            }

            set
            {
                _behavior = value;

                if (ApiMethod != null)
                {
                    ApiMethod.Behavior = value;
                }
            }
        }

        private string _behaviour = "";

        public string Behaviour
        {
            get
            {
                return _behaviour;
            }

            set
            {
                _behaviour = value;

                if (ApiMethod != null)
                {
                    ApiMethod.Behaviour = value;
                }
            }
        }

        private string _classifierID = "";

        public string ClassifierID
        {
            get
            {
                return _classifierID;
            }

            set
            {
                _classifierID = value;

                if (ApiMethod != null)
                {
                    ApiMethod.ClassifierID = value;
                }
            }
        }

        private string _code = "";

        public string Code
        {
            get
            {
                return _code;
            }

            set
            {
                _code = value;

                if (ApiMethod != null)
                {
                    ApiMethod.Code = value;
                }
            }
        }

        private string _concurrency = "";

        public string Concurrency
        {
            get
            {
                return _concurrency;
            }

            set
            {
                _concurrency = value;

                if (ApiMethod != null)
                {
                    ApiMethod.Concurrency = value;
                }
            }
        }

        public string FQStereotype => throw new NotImplementedException();

        private bool _isConst;

        public bool IsConst
        {
            get
            {
                return _isConst;
            }

            set
            {
                _isConst = value;

                if (ApiMethod != null)
                {
                    ApiMethod.IsConst = value;
                }
            }
        }

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

                if (ApiMethod != null)
                {
                    ApiMethod.IsLeaf = value;
                }
            }
        }

        private bool _isPure;

        public bool IsPure
        {
            get
            {
                return _isPure;
            }

            set
            {
                _isPure = value;

                if (ApiMethod != null)
                {
                    ApiMethod.IsPure = value;
                }
            }
        }

        private bool _isQuery;

        public bool IsQuery
        {
            get
            {
                return _isQuery;
            }

            set
            {
                _isQuery = value;

                if (ApiMethod != null)
                {
                    ApiMethod.IsQuery = value;
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

                if (ApiMethod != null)
                {
                    ApiMethod.IsRoot = value;
                }
            }
        }

        private bool _isStatic;

        public bool IsStatic
        {
            get
            {
                return _isStatic;
            }

            set
            {
                _isStatic = value;

                if (ApiMethod != null)
                {
                    ApiMethod.IsStatic = value;
                }
            }
        }

        private bool _isSynchronized;

        public bool IsSynchronized
        {
            get
            {
                return _isSynchronized;
            }

            set
            {
                _isSynchronized = value;

                if (ApiMethod != null)
                {
                    ApiMethod.IsSynchronized = value;
                }
            }
        }

        private string _methodGUID = "";

        public string MethodGUID
        {
            get
            {
                return _methodGUID;
            }

            set
            {
                _methodGUID = value;

                if (ApiMethod != null)
                {
                    ApiMethod.MethodGUID = value;
                }
            }
        }

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

                if (ApiMethod != null)
                {
                    ApiMethod.Name = value;
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

                if (ApiMethod != null)
                {
                    ApiMethod.Notes = value;
                }
            }
        }

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otMethod;
            }
        }

        public Collection Parameters { get; set; }

        public int ParentID { get; set; }

        private int _pos;

        public int Pos
        {
            get
            {
                return _pos;
            }

            set
            {
                _pos = value;

                if (ApiMethod != null)
                {
                    ApiMethod.Pos = value;
                }
            }
        }

        public Collection PostConditions => throw new NotImplementedException();

        public Collection PreConditions => throw new NotImplementedException();

        private bool _returnIsArray;

        public bool ReturnIsArray
        {
            get
            {
                return _returnIsArray;
            }

            set
            {
                _returnIsArray = value;

                if (ApiMethod != null)
                {
                    ApiMethod.ReturnIsArray = value;
                }
            }
        }

        private string _returnType = "";

        public string ReturnType
        {
            get
            {
                return _returnType;
            }

            set
            {
                _returnType = value;

                if (ApiMethod != null)
                {
                    ApiMethod.ReturnType = value;
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

                if (ApiMethod != null)
                {
                    ApiMethod.StateFlags = value;
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

                if (ApiMethod != null)
                {
                    ApiMethod.Stereotype = value;
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

                if (ApiMethod != null)
                {
                    ApiMethod.StereotypeEx = value;
                }
            }
        }

        private string _style = "";

        public string Style
        {
            get
            {
                return _style;
            }

            set
            {
                _style = value;

                if (ApiMethod != null)
                {
                    ApiMethod.Style = value;
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

                if (ApiMethod != null)
                {
                    ApiMethod.StyleEx = value;
                }
            }
        }

        public Collection TaggedValues { get; set; }

        private string _throws = "";

        public string Throws
        {
            get
            {
                return _throws;
            }

            set
            {
                _throws = value;

                if (ApiMethod != null)
                {
                    ApiMethod.Throws = value;
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

                if (ApiMethod != null)
                {
                    ApiMethod.Visibility = value;
                }
            }
        }

        public string GetLastError()
        {
            string result = "";

            if (ApiMethod != null)
            {
                result = ApiMethod.GetLastError();
            }

            return result;
        }

        public bool Update()
        {
            bool result = true;

            if (ApiMethod != null)
            {
                result = ApiMethod.Update();
            }

            return result;
        }
    }
}
