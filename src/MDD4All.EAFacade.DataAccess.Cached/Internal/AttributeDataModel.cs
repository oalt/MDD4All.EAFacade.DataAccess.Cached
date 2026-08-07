using MDD4All.EAFacade.DataModels.Contracts;
using System;
using System.Xml.Linq;
using Attribute = MDD4All.EAFacade.DataModels.Contracts.Attribute;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class AttributeDataModel : RepositoryElementDataModel, Attribute
    {
        public AttributeDataModel()
        {
        }

        public AttributeDataModel(XElement attributeRow, Repository repository)
        {
            Repository = repository;

            _name = attributeRow.Element("Name").Value;
            _notes = attributeRow.Element("Notes").Value;
            _type = attributeRow.Element("Type").Value;
            _attributeID = int.Parse(attributeRow.Element("ID").Value);
            _pos = int.Parse(attributeRow.Element("Pos").Value);
            _visibility = attributeRow.Element("Scope").Value;
            _attributeGUID = attributeRow.Element("ea_guid").Value;
            _default = attributeRow.Element("Default").Value;
            _classifierID = int.Parse(attributeRow.Element("Classifier").Value);
            _parentID = int.Parse(attributeRow.Element("Object_ID").Value);

            _stereotype = attributeRow.Element("Stereotype").Value;
            _containment = attributeRow.Element("Containment").Value;
            _isStatic = attributeRow.Element("IsStatic").Value == "1";
            _isCollection = attributeRow.Element("IsCollection").Value == "1";
            _isOrdered = attributeRow.Element("IsOrdered").Value == "1";
            _allowDuplicates = attributeRow.Element("AllowDuplicates").Value == "1";
            _lowerBound = attributeRow.Element("LowerBound").Value;
            _upperBound = attributeRow.Element("UpperBound").Value;
            _container = attributeRow.Element("Container").Value;
            _isDerived = attributeRow.Element("Derived").Value == "1";
            _length = attributeRow.Element("Length").Value;
            _precision = attributeRow.Element("Precision").Value;
            _scale = attributeRow.Element("Scale").Value;
            _isConst = attributeRow.Element("Const").Value == "1";
            _style = attributeRow.Element("Style").Value;
            _styleEx = attributeRow.Element("StyleEx").Value;
        }

        public AttributeDataModel(EAAPI.Attribute apiAttribute)
        {
            _apiAttribute = apiAttribute;

            _name = apiAttribute.Name;
            _notes = apiAttribute.Notes;
            _type = apiAttribute.Type;
            _attributeID = apiAttribute.AttributeID;
            _pos = apiAttribute.Pos;
            _visibility = apiAttribute.Visibility;
            _attributeGUID = apiAttribute.AttributeGUID;
            _default = apiAttribute.Default;
            _classifierID = apiAttribute.ClassifierID;
            _parentID = apiAttribute.ParentID;

            _stereotype = apiAttribute.Stereotype;
            _containment = apiAttribute.Containment;
            _isStatic = apiAttribute.IsStatic;
            _isCollection = apiAttribute.IsCollection;
            _isOrdered = apiAttribute.IsOrdered;
            _allowDuplicates = apiAttribute.AllowDuplicates;
            _lowerBound = apiAttribute.LowerBound;
            _upperBound = apiAttribute.UpperBound;
            _container = apiAttribute.Container;
            _isDerived = apiAttribute.IsDerived;
            _length = apiAttribute.Length;
            _precision = apiAttribute.Precision;
            _scale = apiAttribute.Scale;
            _isConst = apiAttribute.IsConst;
            _style = apiAttribute.Style;
            _styleEx = apiAttribute.StyleEx;
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

        private int _attributeID;

        public int AttributeID
        {
            get
            {
                return _attributeID;
            }

            private set
            {
                _attributeID = value;
            }
        }

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

        private int _parentID;

        public int ParentID
        {
            get
            {
                return _parentID;
            }

            private set
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
