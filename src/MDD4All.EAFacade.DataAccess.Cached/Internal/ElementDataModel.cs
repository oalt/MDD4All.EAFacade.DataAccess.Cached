using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;
using FACADE = MDD4All.EAFacade.DataModels.Contracts;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ElementDataModel : RepositoryElementDataModel, Element
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ElementDataModel()
        {

        }

        public ElementDataModel(XElement tObjectQueryRow,
                                AbstractDataCache abstractDataCache,
                                Repository repository)
        {
            Repository = repository;
            
            AbstractDataCache = abstractDataCache;

            try
            {
                ElementID = int.Parse(tObjectQueryRow.Element("Object_ID").Value);
                Type = tObjectQueryRow.Element("Object_Type").Value;
                Name = tObjectQueryRow.Element("Name").Value;
                Notes = tObjectQueryRow.Element("Note").Value;
                PackageID = int.Parse(tObjectQueryRow.Element("Package_ID").Value);
                Stereotype = tObjectQueryRow.Element("Stereotype").Value;
                ElementGUID = tObjectQueryRow.Element("ea_guid").Value;
                ParentID = int.Parse(tObjectQueryRow.Element("ParentID").Value);
                Created = DateTime.Parse(tObjectQueryRow.Element("CreatedDate").Value);
                Modified = DateTime.Parse(tObjectQueryRow.Element("ModifiedDate").Value);
                ClassifierID = int.Parse(tObjectQueryRow.Element("Classifier").Value);

                Pdata1 = tObjectQueryRow.Element("PDATA1").Value;
                Pdata2 = tObjectQueryRow.Element("PDATA2").Value;
                Pdata3 = tObjectQueryRow.Element("PDATA3").Value;
                Pdata4 = tObjectQueryRow.Element("PDATA4").Value;
                Pdata5 = tObjectQueryRow.Element("PDATA5").Value;

                PropertyType = 0;

                int treePos = 0;

                if (int.TryParse(tObjectQueryRow.Element("TPos").Value, out treePos))
                {
                    TreePos = treePos;
                }

                Alias = tObjectQueryRow.Element("Alias").Value;
                RunState = tObjectQueryRow.Element("RunState").Value;

            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public ElementDataModel(EAAPI.Element apiElement)
        {
            ElementID = apiElement.ElementID;
            Type = apiElement.Type;
            Name = apiElement.Name;
            Notes = apiElement.Notes;
            PackageID = apiElement.PackageID;
            Stereotype = apiElement.Stereotype;
            ElementGUID = apiElement.ElementGUID;
            PackageID = apiElement.ParentID;
            Created = apiElement.Created;
            Modified = apiElement.Modified;
            ClassifierID = apiElement.ClassifierID;
            TreePos = apiElement.TreePos;
            Abstract = apiElement.Abstract;
            Alias = apiElement.Alias;
            RunState = apiElement.RunState;

        }

        public string Name { get; set; } = "";

        public string Notes { get; set; } = "";

        public string Type { get; set; } = "";

        public string Stereotype { get; set; } = "";

        public int ElementID { get; set; }

        public string ElementGUID { get; set; } = "";

        public int PackageID { get; set; }

        public int ParentID { get; set; }

        public int TreePos { get; set; }

        public GenericCollection<Element> Elements 
        { 
            get
            {
                GenericCollection<Element> result = new GenericCollection<Element>();

                result.AddRange(AbstractDataCache._elementCache.FindAll(element => element.ParentID == ElementID && (element.Type != "Port" && element.Type != "ActionPin")));

                return result;
            }
            
            set 
            { 
                throw new NotImplementedException();
            } 
        } 

        public GenericCollection<TaggedValue> TaggedValues { get; set; } = new GenericCollection<TaggedValue>();

        public string Abstract { get; set; } = "";

        public string ActionFlags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Alias { get; set; } = "";

        public int AssociationClassConnectorID => throw new NotImplementedException();

        public Collection Attributes { get; set; } = new GenericCollection<EAFacade.DataModels.Contracts.Attribute>();

        public Collection AttributesEx { get; set; } = new GenericCollection<DataModels.Contracts.Attribute>();

        public string Author { get; set; } = "";

        public Collection BaseClasses => throw new NotImplementedException();

        public int ClassfierID
        {
            get { return ClassifierID; }
            set { ClassifierID = value; }
        }

        public int ClassifierID { get; set; }

        public string ClassifierName { get; set; } = "";

        public string ClassifierType => throw new NotImplementedException();

        public string Complexity { get; set; } = "";

        public object CompositeDiagram => throw new NotImplementedException();

        public Collection Connectors => throw new NotImplementedException();

        public Collection Constraints => throw new NotImplementedException();

        public Collection ConstraintsEx => throw new NotImplementedException();

        public DateTime Created { get; set; }

        public Collection CustomProperties => throw new NotImplementedException();

        public Collection Diagrams
        {
            get
            {
                GenericCollection<Diagram> result = new GenericCollection<Diagram>();

                result.AddRange(AbstractDataCache._diagramCache.FindAll(diagram => diagram.ParentID == ElementID));

                return result;
            }
        }

        public string Difficulty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Collection Efforts => throw new NotImplementedException();

        public GenericCollection<Element> EmbeddedElements
        {
            get
            {
                GenericCollection<Element> result = new GenericCollection<Element>();

                result.AddRange(AbstractDataCache._elementCache.FindAll(element => element.ParentID == ElementID && (element.Type == "Port" || element.Type == "ActionPin")));

                return result;
            }
        }

        public string EventFlags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ExtensionPoints { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Collection Files => throw new NotImplementedException();

        public string FQName => throw new NotImplementedException();

        public string FQStereotype => throw new NotImplementedException();

        public string Genfile { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Genlinks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Gentype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object Header1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object Header2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsComposite { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsInternalDocArtifact => throw new NotImplementedException();

        public bool IsLeaf { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsNew { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsRoot { set => throw new NotImplementedException(); }

        public bool IsSpec { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Collection Issues => throw new NotImplementedException();

        public bool Locked { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string MetaType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Collection Methods { get; set; } = new GenericCollection<Method>();

        public Collection MethodsEx { get; set; } = new GenericCollection<Method>();

        public Collection Metrics => throw new NotImplementedException();

        public string MiscData => throw new NotImplementedException();

        public DateTime Modified { get; set; }

        public string Multiplicity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ObjectType ObjectType { get; } = ObjectType.otElement;

        public Collection Partitions => throw new NotImplementedException();

        private string Pdata1 { get; set; } = string.Empty;

        private string Pdata2 { get; set; } = string.Empty;

        private string Pdata3 { get; set; } = string.Empty;

        private string Pdata4 { get; set; } = string.Empty;

        private string Pdata5 { get; set; } = string.Empty;

        public string Persistence { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Phase { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Priority { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public string RunState { get; set; } = "";

        public Collection Scenarios => throw new NotImplementedException();

        public Collection StateTransitions => throw new NotImplementedException();

        public string Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string StereotypeEx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string StyleEx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Subtype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Tablespace { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Tag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Collection TaggedValuesEx => throw new NotImplementedException();

        public Collection TemplateParameters => throw new NotImplementedException();

        public Collection Tests => throw new NotImplementedException();

        public string Version { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Visibility { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
