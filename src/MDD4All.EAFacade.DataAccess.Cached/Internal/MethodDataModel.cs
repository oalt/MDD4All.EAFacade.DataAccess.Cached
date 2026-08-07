using MDD4All.EAFacade.DataAccess.Cached.Internal.Collections;
using MDD4All.EAFacade.DataModels.Contracts;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class MethodDataModel : RepositoryElementDataModel, Method
    {
        public MethodDataModel()
        {
            _parameters = new ParameterCollection(this);
            _taggedValues = new MethodTagCollection(this);
        }

        public MethodDataModel(XElement operationRow, Repository repository)
        {
            _parameters = new ParameterCollection(this);
            _taggedValues = new MethodTagCollection(this);

            Repository = repository;

            _methodID = int.Parse(operationRow.Element("OperationID").Value);
            _parentID = int.Parse(operationRow.Element("Object_ID").Value);
            _name = operationRow.Element("Name").Value;
            _visibility = operationRow.Element("Scope").Value;
            _returnType = operationRow.Element("Type").Value;
            _returnIsArray = operationRow.Element("ReturnArray").Value == "1";
            _stereotype = operationRow.Element("Stereotype").Value;
            _isStatic = operationRow.Element("IsStatic").Value == "1";
            _concurrency = operationRow.Element("Concurrency").Value;
            _notes = operationRow.Element("Notes").Value;
            _behavior = operationRow.Element("Behaviour").Value;
            _abstract = operationRow.Element("Abstract").Value == "1";
            _isSynchronized = operationRow.Element("Synchronized").Value == "1";
            _pos = int.Parse(operationRow.Element("Pos").Value);
            _isConst = operationRow.Element("Const").Value == "1";
            _style = operationRow.Element("Style").Value;
            _isPure = operationRow.Element("Pure").Value == "1";
            _classifierID = operationRow.Element("Classifier").Value;
            _code = operationRow.Element("Code").Value;
            _isRoot = operationRow.Element("IsRoot").Value == "1";
            _isLeaf = operationRow.Element("IsLeaf").Value == "1";
            _isQuery = operationRow.Element("IsQuery").Value == "1";
            _stateFlags = operationRow.Element("StateFlags").Value;
            _methodGUID = operationRow.Element("ea_guid").Value;
            _styleEx = operationRow.Element("StyleEx").Value;
        }

        public MethodDataModel(EAAPI.Method apiMethod)
        {
            _parameters = new ParameterCollection(this);
            _taggedValues = new MethodTagCollection(this);

            _apiMethod = apiMethod;

            _methodID = apiMethod.MethodID;
            _parentID = apiMethod.ParentID;
            _name = apiMethod.Name;
            _visibility = apiMethod.Visibility;
            _returnType = apiMethod.ReturnType;
            _returnIsArray = apiMethod.ReturnIsArray;
            _stereotype = apiMethod.Stereotype;
            _isStatic = apiMethod.IsStatic;
            _concurrency = apiMethod.Concurrency;
            _notes = apiMethod.Notes;
            _behavior = apiMethod.Behavior;
            _abstract = apiMethod.Abstract;
            _isSynchronized = apiMethod.IsSynchronized;
            _pos = apiMethod.Pos;
            _isConst = apiMethod.IsConst;
            _style = apiMethod.Style;
            _isPure = apiMethod.IsPure;
            _classifierID = apiMethod.ClassifierID;
            _code = apiMethod.Code;
            _isRoot = apiMethod.IsRoot;
            _isLeaf = apiMethod.IsLeaf;
            _isQuery = apiMethod.IsQuery;
            _stateFlags = apiMethod.StateFlags;
            _methodGUID = apiMethod.MethodGUID;
            _styleEx = apiMethod.StyleEx;
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

        private Collection _parameters = null!;

        public Collection Parameters
        {
            get
            {
                return _parameters;
            }

            set
            {
                _parameters = value;
            }
        }

        private int _parentID;

        public int ParentID
        {
            get
            {
                return _parentID;
            }

            set
            {
                _parentID = value;
            }
        }

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

        private Collection _taggedValues = null!;

        public Collection TaggedValues
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
