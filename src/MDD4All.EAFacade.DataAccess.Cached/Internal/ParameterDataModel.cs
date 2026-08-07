using MDD4All.EAFacade.DataModels.Contracts;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ParameterDataModel : RepositoryElementDataModel, Parameter
    {
        public ParameterDataModel()
        {
        }

        public ParameterDataModel(XElement parameterRow, Repository repository)
        {
            Repository = repository;

            _operationID = int.Parse(parameterRow.Element("OperationID").Value);
            _name = parameterRow.Element("Name").Value;
            _type = parameterRow.Element("Type").Value;
            _default = parameterRow.Element("Default").Value;
            _notes = parameterRow.Element("Notes").Value;
            _position = int.Parse(parameterRow.Element("Pos").Value);
            _isConst = parameterRow.Element("Const").Value == "1";
            _style = parameterRow.Element("Style").Value;
            _kind = parameterRow.Element("Kind").Value;
            _classifierID = parameterRow.Element("Classifier").Value;
            _parameterGUID = parameterRow.Element("ea_guid").Value;
            _styleEx = parameterRow.Element("StyleEx").Value;

            _alias = GetStyleExValue(StyleEx, "alias");
        }

        public ParameterDataModel(EAAPI.Parameter apiParameter)
        {
            _apiParameter = apiParameter;

            _operationID = apiParameter.OperationID;
            _name = apiParameter.Name;
            _type = apiParameter.Type;
            _default = apiParameter.Default;
            _notes = apiParameter.Notes;
            _position = apiParameter.Position;
            _isConst = apiParameter.IsConst;
            _style = apiParameter.Style;
            _kind = apiParameter.Kind;
            _classifierID = apiParameter.ClassifierID;
            _parameterGUID = apiParameter.ParameterGUID;
            _styleEx = apiParameter.StyleEx;
            _alias = apiParameter.Alias;
        }

        private static string GetStyleExValue(string styleEx, string key)
        {
            string result = "";

            char[] elementSeparator = { ';' };

            char[] keyValueSeparator = { '=' };

            if (!string.IsNullOrEmpty(styleEx))
            {
                string[] styleElements = styleEx.Split(elementSeparator);

                foreach (string styleElement in styleElements)
                {
                    if (!string.IsNullOrWhiteSpace(styleElement))
                    {
                        string[] styleElementSplitted = styleElement.Split(keyValueSeparator);

                        if (styleElementSplitted[0] == key)
                        {
                            result = styleElementSplitted[1];
                            break;
                        }
                    }
                }
            }

            return result;
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

        private EAAPI.Parameter? _apiParameter;

        private EAAPI.Parameter? ApiParameter
        {
            get
            {
                if (_apiParameter == null && !string.IsNullOrEmpty(ParameterGUID))
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

                                if (currentMethod.MethodID == OperationID)
                                {
                                    for (short parameterIndex = 0; parameterIndex < currentMethod.Parameters.Count; parameterIndex++)
                                    {
                                        EAAPI.Parameter currentParameter = (EAAPI.Parameter)currentMethod.Parameters.GetAt(parameterIndex);

                                        if (currentParameter.ParameterGUID == ParameterGUID)
                                        {
                                            _apiParameter = currentParameter;
                                            break;
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }

                return _apiParameter;
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

                if (ApiParameter != null)
                {
                    ApiParameter.Alias = value;
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

                if (ApiParameter != null)
                {
                    ApiParameter.ClassifierID = value;
                }
            }
        }

        private string _default = "";

        public string Default
        {
            get
            {
                return _default;
            }

            set
            {
                _default = value;

                if (ApiParameter != null)
                {
                    ApiParameter.Default = value;
                }
            }
        }

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

                if (ApiParameter != null)
                {
                    ApiParameter.IsConst = value;
                }
            }
        }

        private string _kind = "";

        public string Kind
        {
            get
            {
                return _kind;
            }

            set
            {
                _kind = value;

                if (ApiParameter != null)
                {
                    ApiParameter.Kind = value;
                }
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

                if (ApiParameter != null)
                {
                    ApiParameter.Name = value;
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

                if (ApiParameter != null)
                {
                    ApiParameter.Notes = value;
                }
            }
        }

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otParameter;
            }
        }

        private int _operationID;

        public int OperationID
        {
            get
            {
                return _operationID;
            }

            set
            {
                _operationID = value;
            }
        }

        private string _parameterGUID = "";

        public string ParameterGUID
        {
            get
            {
                return _parameterGUID;
            }

            set
            {
                _parameterGUID = value;

                if (ApiParameter != null)
                {
                    ApiParameter.ParameterGUID = value;
                }
            }
        }

        private int _position;

        public int Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;

                if (ApiParameter != null)
                {
                    ApiParameter.Position = value;
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

                if (ApiParameter != null)
                {
                    ApiParameter.Stereotype = value;
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

                if (ApiParameter != null)
                {
                    ApiParameter.StereotypeEx = value;
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

                if (ApiParameter != null)
                {
                    ApiParameter.Style = value;
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

                if (ApiParameter != null)
                {
                    ApiParameter.StyleEx = value;
                }
            }
        }

        public Collection TaggedValues => throw new NotImplementedException();

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

                if (ApiParameter != null)
                {
                    ApiParameter.Type = value;
                }
            }
        }

        public string GetLastError()
        {
            string result = "";

            if (ApiParameter != null)
            {
                result = ApiParameter.GetLastError();
            }

            return result;
        }

        public bool Update()
        {
            bool result = true;

            if (ApiParameter != null)
            {
                result = ApiParameter.Update();
            }

            return result;
        }
    }
}
