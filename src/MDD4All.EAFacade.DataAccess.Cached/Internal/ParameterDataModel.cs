using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ParameterDataModel : RepositoryElementDataModel, Parameter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ParameterDataModel()
        {
        }

        public ParameterDataModel(XElement parameterRow, Repository repository)
        {
            Repository = repository;

            try
            {
                OperationID = int.Parse(parameterRow.Element("OperationID").Value);
                Name = parameterRow.Element("Name").Value;
                Type = parameterRow.Element("Type").Value;
                Default = parameterRow.Element("Default").Value;
                Notes = parameterRow.Element("Notes").Value;
                Position = int.Parse(parameterRow.Element("Pos").Value);
                IsConst = parameterRow.Element("Const").Value == "1";
                Style = parameterRow.Element("Style").Value;
                Kind = parameterRow.Element("Kind").Value;
                ClassifierID = parameterRow.Element("Classifier").Value;
                ParameterGUID = parameterRow.Element("ea_guid").Value;
                StyleEx = parameterRow.Element("StyleEx").Value;

                Alias = GetStyleExValue(StyleEx, "alias");
            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public ParameterDataModel(EAAPI.Parameter apiParameter)
        {
            _apiParameter = apiParameter;

            OperationID = apiParameter.OperationID;
            Name = apiParameter.Name;
            Type = apiParameter.Type;
            Default = apiParameter.Default;
            Notes = apiParameter.Notes;
            Position = apiParameter.Position;
            IsConst = apiParameter.IsConst;
            Style = apiParameter.Style;
            Kind = apiParameter.Kind;
            ClassifierID = apiParameter.ClassifierID;
            ParameterGUID = apiParameter.ParameterGUID;
            StyleEx = apiParameter.StyleEx;
            Alias = apiParameter.Alias;
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

        internal int ParentElementID { get; set; }

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

        public int OperationID { get; set; }

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
