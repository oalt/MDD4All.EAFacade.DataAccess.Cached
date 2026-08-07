using MDD4All.EAFacade.DataModels.Contracts;
using MDD4All.EAFacade.DataAccess.Cached.Internal.Collections;
using System;
using System.Xml.Linq;
using EAAPI = EA;
using FACADE = MDD4All.EAFacade.DataModels.Contracts;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ElementDataModel : RepositoryElementDataModel, Element
    {
        public ElementDataModel()
        {
            _taggedValues = new TaggedValueCollection(this);
            _attributes = new AttributeCollection(this);
        }

        public ElementDataModel(XElement tObjectQueryRow,
                                AbstractDataCache abstractDataCache,
                                Repository repository)
        {
            _taggedValues = new TaggedValueCollection(this);
            _attributes = new AttributeCollection(this);

            Repository = repository;

            AbstractDataCache = abstractDataCache;

            _elementID = int.Parse(tObjectQueryRow.Element("Object_ID").Value);
            _type = tObjectQueryRow.Element("Object_Type").Value;
            _name = tObjectQueryRow.Element("Name").Value;
            _notes = tObjectQueryRow.Element("Note").Value;
            _packageID = int.Parse(tObjectQueryRow.Element("Package_ID").Value);
            _stereotype = tObjectQueryRow.Element("Stereotype").Value;
            _elementGUID = tObjectQueryRow.Element("ea_guid").Value;
            _parentID = int.Parse(tObjectQueryRow.Element("ParentID").Value);
            _created = DateTime.Parse(tObjectQueryRow.Element("CreatedDate").Value);
            _modified = DateTime.Parse(tObjectQueryRow.Element("ModifiedDate").Value);
            _classifierID = int.Parse(tObjectQueryRow.Element("Classifier").Value);

            _pdata1 = tObjectQueryRow.Element("PDATA1").Value;
            _pdata2 = tObjectQueryRow.Element("PDATA2").Value;
            _pdata3 = tObjectQueryRow.Element("PDATA3").Value;
            _pdata4 = tObjectQueryRow.Element("PDATA4").Value;
            _pdata5 = tObjectQueryRow.Element("PDATA5").Value;

            PropertyType = 0;

            int treePos = 0;

            if (int.TryParse(tObjectQueryRow.Element("TPos").Value, out treePos))
            {
                _treePos = treePos;
            }

            _alias = tObjectQueryRow.Element("Alias").Value;
            _runState = tObjectQueryRow.Element("RunState").Value;

            _version = tObjectQueryRow.Element("Version").Value;
            _abstract = tObjectQueryRow.Element("Abstract").Value;
            _complexity = tObjectQueryRow.Element("Complexity").Value;
            _status = tObjectQueryRow.Element("Status").Value;
            _visibility = tObjectQueryRow.Element("Visibility").Value;
            _persistence = tObjectQueryRow.Element("Persistence").Value;
            _gentype = tObjectQueryRow.Element("GenType").Value;
            _genfile = tObjectQueryRow.Element("GenFile").Value;
            _header1 = tObjectQueryRow.Element("Header1").Value;
            _header2 = tObjectQueryRow.Element("Header2").Value;
            _phase = tObjectQueryRow.Element("Phase").Value;
            _genlinks = tObjectQueryRow.Element("GenLinks").Value;
            _multiplicity = tObjectQueryRow.Element("Multiplicity").Value;
            _actionFlags = tObjectQueryRow.Element("ActionFlags").Value;

            // IsRoot is a write-only forwarding property with no backing field, so there is nothing to hydrate here.
            _isLeaf = tObjectQueryRow.Element("IsLeaf").Value == "1";
            _isSpec = tObjectQueryRow.Element("IsSpec").Value == "1";
            _isActive = tObjectQueryRow.Element("IsActive").Value == "1";
        }

        public ElementDataModel(EAAPI.Element apiElement)
        {
            _taggedValues = new TaggedValueCollection(this);
            _attributes = new AttributeCollection(this);

            _apiElement = apiElement;

            _elementID = apiElement.ElementID;
            _type = apiElement.Type;
            _name = apiElement.Name;
            _notes = apiElement.Notes;
            _packageID = apiElement.PackageID;
            _stereotype = apiElement.Stereotype;
            _elementGUID = apiElement.ElementGUID;
            _parentID = apiElement.ParentID;
            _created = apiElement.Created;
            _modified = apiElement.Modified;
            _classifierID = apiElement.ClassifierID;
            _treePos = apiElement.TreePos;
            _abstract = apiElement.Abstract;
            _alias = apiElement.Alias;
            _runState = apiElement.RunState;

            _version = apiElement.Version;
            _complexity = apiElement.Complexity;
            _status = apiElement.Status;
            _visibility = apiElement.Visibility;
            _persistence = apiElement.Persistence;
            _gentype = apiElement.Gentype;
            _genfile = apiElement.Genfile;
            _header1 = apiElement.Header1;
            _header2 = apiElement.Header2;
            _phase = apiElement.Phase;
            _genlinks = apiElement.Genlinks;
            _multiplicity = apiElement.Multiplicity;
            _actionFlags = apiElement.ActionFlags;

            _isLeaf = apiElement.IsLeaf;
            _isSpec = apiElement.IsSpec;
            _isActive = apiElement.IsActive;
        }

        private EAAPI.Element? _apiElement;

        private EAAPI.Element? ApiElement
        {
            get
            {
                if (_apiElement == null)
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        _apiElement = apiRepository.GetElementByID(ElementID);
                    }
                }

                return _apiElement;
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

                if (ApiElement != null)
                {
                    ApiElement.Name = value;
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

                if (ApiElement != null)
                {
                    ApiElement.Notes = value;
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

                if (ApiElement != null)
                {
                    ApiElement.Type = value;
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

                if (ApiElement != null)
                {
                    ApiElement.Stereotype = value;
                }
            }
        }

        private int _elementID;

        public int ElementID
        {
            get
            {
                return _elementID;
            }

            set
            {
                _elementID = value;
            }
        }

        private string _elementGUID = "";

        public string ElementGUID
        {
            get
            {
                return _elementGUID;
            }

            set
            {
                _elementGUID = value;
            }
        }

        private int _packageID;

        public int PackageID
        {
            get
            {
                return _packageID;
            }

            set
            {
                _packageID = value;

                if (ApiElement != null)
                {
                    ApiElement.PackageID = value;
                }
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

                if (ApiElement != null)
                {
                    ApiElement.ParentID = value;
                }
            }
        }

        private int _treePos;

        public int TreePos
        {
            get
            {
                return _treePos;
            }

            set
            {
                _treePos = value;

                if (ApiElement != null)
                {
                    ApiElement.TreePos = value;
                }
            }
        }

        public GenericCollection<Element> Elements
        {
            get
            {
                ElementChildCollection result = new ElementChildCollection(this, AbstractDataCache);

                result.AddRange(AbstractDataCache._elementCache.FindAll(element => element.ParentID == ElementID && (element.Type != "Port" && element.Type != "ActionPin")));

                return result;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private GenericCollection<TaggedValue> _taggedValues = null!;

        public GenericCollection<TaggedValue> TaggedValues
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

        private string _abstract = "";

        public string Abstract
        {
            get
            {
                return _abstract;
            }

            set
            {
                _abstract = value;

                if (ApiElement != null)
                {
                    ApiElement.Abstract = value;
                }
            }
        }

        private string _actionFlags = "";

        public string ActionFlags
        {
            get
            {
                return _actionFlags;
            }

            set
            {
                _actionFlags = value;

                if (ApiElement != null)
                {
                    ApiElement.ActionFlags = value;
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

                if (ApiElement != null)
                {
                    ApiElement.Alias = value;
                }
            }
        }

        public int AssociationClassConnectorID => throw new NotImplementedException();

        private Collection _attributes = null!;

        public Collection Attributes
        {
            get
            {
                return _attributes;
            }

            set
            {
                _attributes = value;
            }
        }

        private Collection _attributesEx = new GenericCollection<DataModels.Contracts.Attribute>();

        public Collection AttributesEx
        {
            get
            {
                return _attributesEx;
            }

            set
            {
                _attributesEx = value;
            }
        }

        private string _author = "";

        public string Author
        {
            get
            {
                return _author;
            }

            set
            {
                _author = value;

                if (ApiElement != null)
                {
                    ApiElement.Author = value;
                }
            }
        }

        public Collection BaseClasses => throw new NotImplementedException();

        public int ClassfierID
        {
            get { return ClassifierID; }
            set { ClassifierID = value; }
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

                if (ApiElement != null)
                {
                    ApiElement.ClassifierID = value;
                }
            }
        }

        private string _classifierName = "";

        public string ClassifierName
        {
            get
            {
                return _classifierName;
            }

            set
            {
                _classifierName = value;

                if (ApiElement != null)
                {
                    ApiElement.ClassifierName = value;
                }
            }
        }

        public string ClassifierType => throw new NotImplementedException();

        private string _complexity = "";

        public string Complexity
        {
            get
            {
                return _complexity;
            }

            set
            {
                _complexity = value;

                if (ApiElement != null)
                {
                    ApiElement.Complexity = value;
                }
            }
        }

        public object CompositeDiagram => throw new NotImplementedException();

        public Collection Connectors
        {
            get
            {
                ElementConnectorCollection result = new ElementConnectorCollection(this, AbstractDataCache);

                result.AddRange(AbstractDataCache._connectorCache.FindAll(connector => (connector.ClientID == ElementID && connector.SupplierID != ElementID) ||
                                                                                       (connector.SupplierID == ElementID && connector.ClientID != ElementID) ||
                                                                                       (connector.ClientID == ElementID && connector.SupplierID == ElementID)));

                return result;
            }
        }

        public Collection Constraints => throw new NotImplementedException();

        public Collection ConstraintsEx => throw new NotImplementedException();

        private DateTime _created;

        public DateTime Created
        {
            get
            {
                return _created;
            }

            set
            {
                _created = value;

                if (ApiElement != null)
                {
                    ApiElement.Created = value;
                }
            }
        }

        public Collection CustomProperties => throw new NotImplementedException();

        public Collection Diagrams
        {
            get
            {
                ElementDiagramCollection result = new ElementDiagramCollection(this, AbstractDataCache);

                result.AddRange(AbstractDataCache._diagramCache.FindAll(diagram => diagram.ParentID == ElementID));

                return result;
            }
        }

        private string _difficulty = "";

        public string Difficulty
        {
            get
            {
                return _difficulty;
            }

            set
            {
                _difficulty = value;

                if (ApiElement != null)
                {
                    ApiElement.Difficulty = value;
                }
            }
        }

        public Collection Efforts => throw new NotImplementedException();

        public GenericCollection<Element> EmbeddedElements
        {
            get
            {
                ElementEmbeddedElementCollection result = new ElementEmbeddedElementCollection(this, AbstractDataCache);

                result.AddRange(AbstractDataCache._elementCache.FindAll(element => element.ParentID == ElementID && (element.Type == "Port" || element.Type == "ActionPin")));

                return result;
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

                if (ApiElement != null)
                {
                    ApiElement.EventFlags = value;
                }
            }
        }

        private string _extensionPoints = "";

        public string ExtensionPoints
        {
            get
            {
                return _extensionPoints;
            }

            set
            {
                _extensionPoints = value;

                if (ApiElement != null)
                {
                    ApiElement.ExtensionPoints = value;
                }
            }
        }

        public Collection Files => throw new NotImplementedException();

        public string FQName => throw new NotImplementedException();

        public string FQStereotype => throw new NotImplementedException();

        private string _genfile = "";

        public string Genfile
        {
            get
            {
                return _genfile;
            }

            set
            {
                _genfile = value;

                if (ApiElement != null)
                {
                    ApiElement.Genfile = value;
                }
            }
        }

        private string _genlinks = "";

        public string Genlinks
        {
            get
            {
                return _genlinks;
            }

            set
            {
                _genlinks = value;

                if (ApiElement != null)
                {
                    ApiElement.Genlinks = value;
                }
            }
        }

        private string _gentype = "";

        public string Gentype
        {
            get
            {
                return _gentype;
            }

            set
            {
                _gentype = value;

                if (ApiElement != null)
                {
                    ApiElement.Gentype = value;
                }
            }
        }

        private object _header1 = null!;

        public object Header1
        {
            get
            {
                return _header1;
            }

            set
            {
                _header1 = value;

                if (ApiElement != null)
                {
                    ApiElement.Header1 = value;
                }
            }
        }

        private object _header2 = null!;

        public object Header2
        {
            get
            {
                return _header2;
            }

            set
            {
                _header2 = value;

                if (ApiElement != null)
                {
                    ApiElement.Header2 = value;
                }
            }
        }

        private bool _isActive;

        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            set
            {
                _isActive = value;

                if (ApiElement != null)
                {
                    ApiElement.IsActive = value;
                }
            }
        }

        private bool _isComposite;

        public bool IsComposite
        {
            get
            {
                return _isComposite;
            }

            set
            {
                _isComposite = value;

                if (ApiElement != null)
                {
                    ApiElement.IsComposite = value;
                }
            }
        }

        public bool IsInternalDocArtifact => throw new NotImplementedException();

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

                if (ApiElement != null)
                {
                    ApiElement.IsLeaf = value;
                }
            }
        }

        private bool _isNew;

        public bool IsNew
        {
            get
            {
                return _isNew;
            }

            set
            {
                _isNew = value;

                if (ApiElement != null)
                {
                    ApiElement.IsNew = value;
                }
            }
        }

        public bool IsRoot
        {
            set
            {
                if (ApiElement != null)
                {
                    ApiElement.IsRoot = value;
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

                if (ApiElement != null)
                {
                    ApiElement.IsSpec = value;
                }
            }
        }

        public Collection Issues => throw new NotImplementedException();

        private bool _locked;

        public bool Locked
        {
            get
            {
                return _locked;
            }

            set
            {
                _locked = value;

                if (ApiElement != null)
                {
                    ApiElement.Locked = value;
                }
            }
        }

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

                if (ApiElement != null)
                {
                    ApiElement.MetaType = value;
                }
            }
        }

        public Collection Methods
        {
            get
            {
                MethodCollection result = new MethodCollection(this, AbstractDataCache);

                result.AddRange(AbstractDataCache._methodCache.FindAll(method => method.ParentID == ElementID));

                return result;
            }
        }

        private Collection _methodsEx = new GenericCollection<Method>();

        public Collection MethodsEx
        {
            get
            {
                return _methodsEx;
            }

            set
            {
                _methodsEx = value;
            }
        }

        public Collection Metrics => throw new NotImplementedException();

        public string MiscData => throw new NotImplementedException();

        private DateTime _modified;

        public DateTime Modified
        {
            get
            {
                return _modified;
            }

            set
            {
                _modified = value;

                if (ApiElement != null)
                {
                    ApiElement.Modified = value;
                }
            }
        }

        private string _multiplicity = "";

        public string Multiplicity
        {
            get
            {
                return _multiplicity;
            }

            set
            {
                _multiplicity = value;

                if (ApiElement != null)
                {
                    ApiElement.Multiplicity = value;
                }
            }
        }

        public ObjectType ObjectType { get; } = ObjectType.otElement;

        public Collection Partitions => throw new NotImplementedException();

        private string _pdata1 = string.Empty;

        private string Pdata1
        {
            get
            {
                return _pdata1;
            }

            set
            {
                _pdata1 = value;
            }
        }

        private string _pdata2 = string.Empty;

        private string Pdata2
        {
            get
            {
                return _pdata2;
            }

            set
            {
                _pdata2 = value;
            }
        }

        private string _pdata3 = string.Empty;

        private string Pdata3
        {
            get
            {
                return _pdata3;
            }

            set
            {
                _pdata3 = value;
            }
        }

        private string _pdata4 = string.Empty;

        private string Pdata4
        {
            get
            {
                return _pdata4;
            }

            set
            {
                _pdata4 = value;
            }
        }

        private string _pdata5 = string.Empty;

        private string Pdata5
        {
            get
            {
                return _pdata5;
            }

            set
            {
                _pdata5 = value;
            }
        }

        private string _persistence = "";

        public string Persistence
        {
            get
            {
                return _persistence;
            }

            set
            {
                _persistence = value;

                if (ApiElement != null)
                {
                    ApiElement.Persistence = value;
                }
            }
        }

        private string _phase = "";

        public string Phase
        {
            get
            {
                return _phase;
            }

            set
            {
                _phase = value;

                if (ApiElement != null)
                {
                    ApiElement.Phase = value;
                }
            }
        }

        private string _priority = "";

        public string Priority
        {
            get
            {
                return _priority;
            }

            set
            {
                _priority = value;

                if (ApiElement != null)
                {
                    ApiElement.Priority = value;
                }
            }
        }

        public Properties Properties => throw new NotImplementedException();

        public int PropertyType
        {
            get
            {
                int result = 0;

                if(Type == "Port" || Type == "Part" || Type == "ActionPin")
                {
                    Element classifierElement = AbstractDataCache._elementCache.Find(el => el.ElementGUID == Pdata1);
                    if (classifierElement != null)
                    {
                        result = classifierElement.ElementID;
                    }
                }

                return result;
            }

            set
            {
            }
        }

        public object PropertyTypeName => throw new NotImplementedException();

        public Collection Realizes => throw new NotImplementedException();

        public Collection Requirements => throw new NotImplementedException();

        public Collection RequirementsEx => throw new NotImplementedException();

        public Collection Resources => throw new NotImplementedException();

        public Collection Risks => throw new NotImplementedException();

        private string _runState = "";

        public string RunState
        {
            get
            {
                return _runState;
            }

            set
            {
                _runState = value;

                if (ApiElement != null)
                {
                    ApiElement.RunState = value;
                }
            }
        }

        public Collection Scenarios => throw new NotImplementedException();

        public Collection StateTransitions => throw new NotImplementedException();

        private string _status = "";

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;

                if (ApiElement != null)
                {
                    ApiElement.Status = value;
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

                if (ApiElement != null)
                {
                    ApiElement.StereotypeEx = value;
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

                if (ApiElement != null)
                {
                    ApiElement.StyleEx = value;
                }
            }
        }

        private int _subtype;

        public int Subtype
        {
            get
            {
                return _subtype;
            }

            set
            {
                _subtype = value;

                if (ApiElement != null)
                {
                    ApiElement.Subtype = value;
                }
            }
        }

        private string _tablespace = "";

        public string Tablespace
        {
            get
            {
                return _tablespace;
            }

            set
            {
                _tablespace = value;

                if (ApiElement != null)
                {
                    ApiElement.Tablespace = value;
                }
            }
        }

        private string _tag = "";

        public string Tag
        {
            get
            {
                return _tag;
            }

            set
            {
                _tag = value;

                if (ApiElement != null)
                {
                    ApiElement.Tag = value;
                }
            }
        }

        public Collection TaggedValuesEx => throw new NotImplementedException();

        public Collection TemplateParameters => throw new NotImplementedException();

        public Collection Tests => throw new NotImplementedException();

        private string _version = "";

        public string Version
        {
            get
            {
                return _version;
            }

            set
            {
                _version = value;

                if (ApiElement != null)
                {
                    ApiElement.Version = value;
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

                if (ApiElement != null)
                {
                    ApiElement.Visibility = value;
                }
            }
        }

        public bool ApplyGroupLock(string aGroupName)
        {
            throw new NotImplementedException();
        }

        public bool ApplyUserLock()
        {
            throw new NotImplementedException();
        }

        public Element Clone()
        {
            throw new NotImplementedException();
        }

        public bool CreateAssociationClass(int ConnectorID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLinkedDocument()
        {
            throw new NotImplementedException();
        }

        public bool ExportInternalDocumentArtifact(string filenamne)
        {
            throw new NotImplementedException();
        }

        public string GetBusinessRules()
        {
            throw new NotImplementedException();
        }

        public string GetDecisionTable()
        {
            throw new NotImplementedException();
        }

        public string GetLastError()
        {
            string result = "";

            if (ApiElement != null)
            {
                result = ApiElement.GetLastError();
            }

            return result;
        }

        public string GetLinkedDocument()
        {
            throw new NotImplementedException();
        }

        public string GetRelationSet(EnumRelationSetType Type)
        {
            throw new NotImplementedException();
        }

        public string GetStereotypeList()
        {
            throw new NotImplementedException();
        }

        public bool HasStereotype(string stereo)
        {
            throw new NotImplementedException();
        }

        public bool ImportInternalDocumentArtifact(string filenamne)
        {
            throw new NotImplementedException();
        }

        public bool IsAssociationClass()
        {
            throw new NotImplementedException();
        }

        public bool LoadLinkedDocument(string FileName)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            _apiElement = null;
        }

        public bool ReleaseUserLock()
        {
            throw new NotImplementedException();
        }

        public bool SaveLinkedDocument(string FileName)
        {
            throw new NotImplementedException();
        }

        public void SetAppearance(int Scope, int Item, int Value)
        {
            if (ApiElement != null)
            {
                ApiElement.SetAppearance(Scope, Item, Value);
            }
        }

        public bool SetCompositeDiagram(string sGUID)
        {
            throw new NotImplementedException();
        }

        public bool SynchConstraints(string sProfile, string sStereotype)
        {
            throw new NotImplementedException();
        }

        public bool SynchTaggedValues(string sProfile, string sStereotype)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string result = "";

            result += "[" + Type + "] ";

            if (!string.IsNullOrEmpty(Stereotype))
            {
                result += "«" + Stereotype + "» ";
            }

            result += Name;

            result += " (id=" + ElementID + ")";

            return result;
        }

        public bool UnlinkFromAssociation()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            bool result = true;

            if (ApiElement != null)
            {
                result = ApiElement.Update();
            }

            return result;
        }
    }
}
