using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using Attribute = MDD4All.EAFacade.DataModels.Contracts.Attribute;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class AttributeDataModel : RepositoryElementDataModel, Attribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AttributeDataModel()
        {
        }

        public AttributeDataModel(XElement attributeRow, Repository repository)
        {
            Repository = repository;

            try
            {
                Name = attributeRow.Element("Name").Value;
                Notes = attributeRow.Element("Notes").Value;
                Type = attributeRow.Element("Type").Value;
                AttributeID = int.Parse(attributeRow.Element("ID").Value);
                Pos = int.Parse(attributeRow.Element("Pos").Value);
                Visibility = attributeRow.Element("Scope").Value;
                AttributeGUID = attributeRow.Element("ea_guid").Value;
                Default = attributeRow.Element("Default").Value;
                ClassifierID = int.Parse(attributeRow.Element("Classifier").Value);
                ParentID = int.Parse(attributeRow.Element("Object_ID").Value);

                Stereotype = attributeRow.Element("Stereotype").Value;
                Containment = attributeRow.Element("Containment").Value;
                IsStatic = attributeRow.Element("IsStatic").Value == "1";
                IsCollection = attributeRow.Element("IsCollection").Value == "1";
                IsOrdered = attributeRow.Element("IsOrdered").Value == "1";
                AllowDuplicates = attributeRow.Element("AllowDuplicates").Value == "1";
                LowerBound = attributeRow.Element("LowerBound").Value;
                UpperBound = attributeRow.Element("UpperBound").Value;
                Container = attributeRow.Element("Container").Value;
                IsDerived = attributeRow.Element("Derived").Value == "1";
                Length = attributeRow.Element("Length").Value;
                Precision = attributeRow.Element("Precision").Value;
                Scale = attributeRow.Element("Scale").Value;
                IsConst = attributeRow.Element("Const").Value == "1";
                Style = attributeRow.Element("Style").Value;
                StyleEx = attributeRow.Element("StyleEx").Value;
            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public AttributeDataModel(EAAPI.Attribute apiAttribute)
        {
            _apiAttribute = apiAttribute;

            Name = apiAttribute.Name;
            Notes = apiAttribute.Notes;
            Type = apiAttribute.Type;
            AttributeID = apiAttribute.AttributeID;
            Pos = apiAttribute.Pos;
            Visibility = apiAttribute.Visibility;
            AttributeGUID = apiAttribute.AttributeGUID;
            Default = apiAttribute.Default;
            ClassifierID = apiAttribute.ClassifierID;
            ParentID = apiAttribute.ParentID;

            Stereotype = apiAttribute.Stereotype;
            Containment = apiAttribute.Containment;
            IsStatic = apiAttribute.IsStatic;
            IsCollection = apiAttribute.IsCollection;
            IsOrdered = apiAttribute.IsOrdered;
            AllowDuplicates = apiAttribute.AllowDuplicates;
            LowerBound = apiAttribute.LowerBound;
            UpperBound = apiAttribute.UpperBound;
            Container = apiAttribute.Container;
            IsDerived = apiAttribute.IsDerived;
            Length = apiAttribute.Length;
            Precision = apiAttribute.Precision;
            Scale = apiAttribute.Scale;
            IsConst = apiAttribute.IsConst;
            Style = apiAttribute.Style;
            StyleEx = apiAttribute.StyleEx;
        }

        private EAAPI.Attribute? _apiAttribute;

        private EAAPI.Attribute? ApiAttribute
        {
            get
            {
                if (_apiAttribute == null)
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        _apiAttribute = apiRepository.GetAttributeByID(AttributeID);
                    }
                }

                return _apiAttribute;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.Alias = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.AllowDuplicates = value;
                }
            }
        }

        private string _attributeGUID = "";

        public string AttributeGUID
        {
            get
            {
                return _attributeGUID;
            }

            set
            {
                _attributeGUID = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.AttributeGUID = value;
                }
            }
        }

        public int AttributeID { get; private set; }

        private int _classifierID;

        public int ClassifierID
        {
            get
            {
                return _classifierID;
            }

            set
            {
                _classifierID = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.ClassifierID = value;
                }
            }
        }

        public Collection Constraints => throw new NotImplementedException();

        private string _container = "";

        public string Container
        {
            get
            {
                return _container;
            }

            set
            {
                _container = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.Container = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.Containment = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.Default = value;
                }
            }
        }

        public string FQStereotype => throw new NotImplementedException();

        private bool _isCollection;

        public bool IsCollection
        {
            get
            {
                return _isCollection;
            }

            set
            {
                _isCollection = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.IsCollection = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.IsConst = value;
                }
            }
        }

        private bool _isDerived;

        public bool IsDerived
        {
            get
            {
                return _isDerived;
            }

            set
            {
                _isDerived = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.IsDerived = value;
                }
            }
        }

        private bool _isID;

        public bool IsID
        {
            get
            {
                return _isID;
            }

            set
            {
                _isID = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.IsID = value;
                }
            }
        }

        private bool _isOrdered;

        public bool IsOrdered
        {
            get
            {
                return _isOrdered;
            }

            set
            {
                _isOrdered = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.IsOrdered = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.IsStatic = value;
                }
            }
        }

        private string _length = string.Empty;

        public string Length
        {
            get
            {
                return _length;
            }

            set
            {
                _length = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.Length = value;
                }
            }
        }

        private string _lowerBound = "";

        public string LowerBound
        {
            get
            {
                return _lowerBound;
            }

            set
            {
                _lowerBound = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.LowerBound = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.Name = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.Notes = value;
                }
            }
        }

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otAttribute;
            }
        }

        public int ParentID { get; private set; }

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

                if (ApiAttribute != null)
                {
                    ApiAttribute.Pos = value;
                }
            }
        }

        private string _precision = "";

        public string Precision
        {
            get
            {
                return _precision;
            }

            set
            {
                _precision = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.Precision = value;
                }
            }
        }

        private string _redefinedProperty = "";

        public string RedefinedProperty
        {
            get
            {
                return _redefinedProperty;
            }

            set
            {
                _redefinedProperty = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.RedefinedProperty = value;
                }
            }
        }

        private string _scale = "";

        public string Scale
        {
            get
            {
                return _scale;
            }

            set
            {
                _scale = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.Scale = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.Stereotype = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.StereotypeEx = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.Style = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.StyleEx = value;
                }
            }
        }

        private string _subsettedProperty = "";

        public string SubsettedProperty
        {
            get
            {
                return _subsettedProperty;
            }

            set
            {
                _subsettedProperty = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.SubsettedProperty = value;
                }
            }
        }

        public Collection TaggedValues => throw new NotImplementedException();

        public Collection TaggedValuesEx => throw new NotImplementedException();

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

                if (ApiAttribute != null)
                {
                    ApiAttribute.Type = value;
                }
            }
        }

        private string _upperBound = "";

        public string UpperBound
        {
            get
            {
                return _upperBound;
            }

            set
            {
                _upperBound = value;

                if (ApiAttribute != null)
                {
                    ApiAttribute.UpperBound = value;
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

                if (ApiAttribute != null)
                {
                    ApiAttribute.Visibility = value;
                }
            }
        }

        public string GetLastError()
        {
            string result = string.Empty;

            return result;
        }

        public bool Update()
        {
            bool result = true;

            if (ApiAttribute != null)
            {
                result = ApiAttribute.Update();
            }

            return result;
        }
    }
}
