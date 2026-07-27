using System;
using System.Collections.Generic;
using System.Xml.Linq;
using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using EAAPI = EA;
using EADM = MDD4All.EAFacade.DataAccess.Cached.Internal;

namespace MDD4All.EAFacade.DataAccess.Cached
{
    public class CachedRepository : EADM.AbstractDataCache, Repository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public CachedRepository()
        {
        }

        public CachedRepository(EAAPI.Repository repository)
        {
            _apiRepository = repository;
        }

        public void CacheAll()
        {
            logger.Debug("Caching model...");

            DateTime startTime = DateTime.Now;

            if(_apiRepository != null)
            {
                _connectionString = _apiRepository.ConnectionString;
                _projectGUID = _apiRepository.ProjectGUID;

                InitializePackageCache();
                logger.Debug(_packageCache.Count + " Packages cached.");

                InitializeElementCache();
                logger.Debug(_elementCache.Count + " Elements cached.");

                InitializeConnectorCache();
                logger.Debug(_connectorCache.Count + " Connectors cached.");

                InitializeDiagramCache();
                logger.Debug(_diagramCache.Count + " Diagrams cached.");
            }

            DateTime endTime = DateTime.Now;

            TimeSpan duration = endTime.Subtract(startTime);

            logger.Debug("Caching finished. Duration: " + duration);
        }

        public void InitializePackageCache()
        {
            _packageCache = new List<Package>();

            string xml = _apiRepository.SQLQuery("select * from t_package");

            XElement rootElement = XElement.Parse(xml);

            XElement datasetElement = rootElement.Element("Dataset_0");

            XElement dataElement = datasetElement.Element("Data");

            IEnumerable<XElement> rows = dataElement.Elements("Row");

            foreach (XElement row in rows)
            {
                EADM.PackageDataModel package = new EADM.PackageDataModel(row, this, this);

                _packageCache.Add(package);
            }

            InitializePackageElements();
        }

        private void InitializePackageElements()
        {
            _elementCache = new List<Element>();

            string xml = _apiRepository.SQLQuery("select * from t_object where Object_Type = 'Package'");

            XElement rootElement = XElement.Parse(xml);

            XElement datasetElement = rootElement.Element("Dataset_0");

            IEnumerable<XElement> rows = new List<XElement>();

            if (datasetElement != null)
            {
                XElement dataElement = datasetElement.Element("Data");

                if (dataElement != null)
                {
                    rows = dataElement.Elements("Row");
                }
            }

            foreach (XElement row in rows)
            {
                EADM.ElementDataModel element = new EADM.ElementDataModel(row, this, this);

                // tagged values
                string taggedValueXml = _apiRepository.SQLQuery("select * from t_objectproperties where Object_ID = " + element.ElementID);

                XElement taggedValueRootElement = XElement.Parse(taggedValueXml);

                XElement taggedValueDatasetElement = taggedValueRootElement.Element("Dataset_0");

                if (taggedValueDatasetElement != null)
                {
                    XElement taggedValueDataElement = taggedValueDatasetElement.Element("Data");

                    IEnumerable<XElement> taggedValueRows = taggedValueDataElement.Elements("Row");

                    foreach (XElement taggedValueRow in taggedValueRows)
                    {
                        TaggedValue taggedValue = new EADM.TaggedValueDataModel(taggedValueRow, this);

                        element.TaggedValues.Add(taggedValue);
                    }
                }

                _elementCache.Add(element);

            }
        }

        public void InitializeElementCache()
        {


            string xml = _apiRepository.SQLQuery("select * from t_object where Object_Type <> 'Package'");

            XElement rootElement = XElement.Parse(xml);

            XElement datasetElement = rootElement.Element("Dataset_0");

            if (datasetElement != null)
            {
                XElement dataElement = datasetElement.Element("Data");

                IEnumerable<XElement> rows = dataElement.Elements("Row");

                foreach (XElement row in rows)
                {
                    EADM.ElementDataModel element = new EADM.ElementDataModel(row, this, this);

                    // tagged values
                    string taggedValueXml = _apiRepository.SQLQuery("select * from t_objectproperties where Object_ID = " + element.ElementID);

                    XElement taggedValueRootElement = XElement.Parse(taggedValueXml);

                    XElement taggedValueDatasetElement = taggedValueRootElement.Element("Dataset_0");

                    if (taggedValueDatasetElement != null)
                    {
                        XElement taggedValueDataElement = taggedValueDatasetElement.Element("Data");

                        IEnumerable<XElement> taggedValueRows = taggedValueDataElement.Elements("Row");

                        foreach (XElement taggedValueRow in taggedValueRows)
                        {
                            TaggedValue taggedValue = new EADM.TaggedValueDataModel(taggedValueRow, this);

                            element.TaggedValues.Add(taggedValue);
                        }
                    }

                    // attributes
                    string attributeValueXml = _apiRepository.SQLQuery("select * from t_attribute where Object_ID = " + element.ElementID);

                    XElement attributeValueRootElement = XElement.Parse(attributeValueXml);

                    XElement attributeValueDatasetElement = attributeValueRootElement.Element("Dataset_0");

                    if (attributeValueDatasetElement != null)
                    {
                        XElement attributeDataElement = attributeValueDatasetElement.Element("Data");

                        IEnumerable<XElement> attributeRows = attributeDataElement.Elements("Row");

                        foreach (XElement attributeRow in attributeRows)
                        {
                            DataModels.Contracts.Attribute attribute = new EADM.AttributeDataModel(attributeRow, this);

                            ((GenericCollection<DataModels.Contracts.Attribute>)element.Attributes).Add(attribute);
                        }
                    }

                    _elementCache.Add(element);

                }
            }
        }

        public void InitializeConnectorCache()
        {
            _connectorCache = new List<Connector>();

            string xml = _apiRepository.SQLQuery("select * from t_connector");

            XElement rootElement = XElement.Parse(xml);

            XElement datasetElement = rootElement.Element("Dataset_0");

            if (datasetElement != null)
            {
                XElement dataElement = datasetElement.Element("Data");

                IEnumerable<XElement> rows = dataElement.Elements("Row");

                foreach (XElement row in rows)
                {
                    EADM.ConnectorDataModel connector = new EADM.ConnectorDataModel(row, this);

                    // tagged values
                    string taggedValueXml = _apiRepository.SQLQuery("select * from t_connectortag where ElementID = " + connector.ConnectorID);

                    XElement taggedValueRootElement = XElement.Parse(taggedValueXml);

                    XElement taggedValueDatasetElement = taggedValueRootElement.Element("Dataset_0");

                    if (taggedValueDatasetElement != null)
                    {
                        XElement taggedValueDataElement = taggedValueDatasetElement.Element("Data");

                        IEnumerable<XElement> taggedValueRows = taggedValueDataElement.Elements("Row");

                        foreach (XElement taggedValueRow in taggedValueRows)
                        {
                            ConnectorTag taggedValue = new EADM.ConnectorTagDataModel(taggedValueRow, this);

                            connector.TaggedValues.Add(taggedValue);
                        }
                    }

                    _connectorCache.Add(connector);

                }
            }
        }

        public void InitializeDiagramCache()
        {
            _diagramCache = new List<Diagram>();

            string xml = _apiRepository.SQLQuery("select * from t_diagram");

            XElement rootElement = XElement.Parse(xml);

            XElement datasetElement = rootElement.Element("Dataset_0");

            if (datasetElement != null)
            {

                XElement dataElement = datasetElement.Element("Data");

                IEnumerable<XElement> rows = dataElement.Elements("Row");

                foreach (XElement row in rows)
                {
                    EADM.DiagramDataModel diagram = new EADM.DiagramDataModel(row, this);


                    _diagramCache.Add(diagram);

                    // diagram objects
                    string diagramObjectsXml = _apiRepository.SQLQuery("select * from t_diagramobjects where Diagram_ID = " + diagram.DiagramID);

                    XElement diagramObjectRootElement = XElement.Parse(diagramObjectsXml);

                    XElement diagramObjectDatasetElement = diagramObjectRootElement.Element("Dataset_0");

                    if (diagramObjectDatasetElement != null)
                    {
                        XElement diagramObjectDataElement = diagramObjectDatasetElement.Element("Data");

                        IEnumerable<XElement> diagramObjectRows = diagramObjectDataElement.Elements("Row");

                        foreach (XElement diagramObjectRow in diagramObjectRows)
                        {
                            EADM.DiagramObjectDataModel diagramObject = new EADM.DiagramObjectDataModel(diagramObjectRow, this);

                            diagram.DiagramObjects.Add(diagramObject);
                        }
                    }

                    // diagram links
                    string diagramLinksXml = _apiRepository.SQLQuery("select * from t_diagramlinks where DiagramID = " + diagram.DiagramID);

                    XElement diagramLinkRootElement = XElement.Parse(diagramLinksXml);

                    XElement diagramLinkDatasetElement = diagramLinkRootElement.Element("Dataset_0");

                    if (diagramLinkDatasetElement != null)
                    {
                        XElement diagramLinkDataElement = diagramLinkDatasetElement.Element("Data");

                        IEnumerable<XElement> diagramLinkRows = diagramLinkDataElement.Elements("Row");

                        foreach (XElement diagramLinkRow in diagramLinkRows)
                        {
                            EADM.DiagramLinkDataModel diagramLink = new EADM.DiagramLinkDataModel(diagramLinkRow, this);

                            diagram.DiagramLinks.Add(diagramLink);
                        }
                    }
                }
            }
        }

        private EAAPI.Repository? _apiRepository;

        public EAAPI.Repository? ApiRepository
        {
            get
            {
                return _apiRepository;
            }

            set
            {
                _apiRepository = value;
            }
        }

        public event EventHandler CachingFinished;

        public Collection Authors => throw new NotImplementedException();

        public bool BatchAppend
        {
            get
            {
                bool result = false;

                if (_apiRepository != null)
                {
                    result = _apiRepository.BatchAppend;
                }

                return result;
            }

            set
            {
                if (_apiRepository != null)
                {
                    _apiRepository.BatchAppend = value;
                }
            }
        }

        public Collection Clients => throw new NotImplementedException();

        private string _connectionString = string.Empty;

        public string ConnectionString => _connectionString;

        public Collection Datatypes => throw new NotImplementedException();

        public EAEditionTypes EAEdition
        {
            get
            {
                EAEditionTypes result = EAEditionTypes.piLite;

                if (_apiRepository != null)
                {
                    result = (EAEditionTypes)_apiRepository.EAEdition;
                }

                return result;
            }
        }

        public EAEditionTypes EAEditionEx
        {
            get
            {
                EAEditionTypes result = EAEditionTypes.piLite;

                if (_apiRepository != null)
                {
                    result = (EAEditionTypes)_apiRepository.EAEditionEx;
                }

                return result;
            }
        }

        public bool EnableCache
        {
            get
            {
                bool result = false;

                if (_apiRepository != null)
                {
                    result = _apiRepository.EnableCache;
                }

                return result;
            }

            set
            {
                if (_apiRepository != null)
                {
                    _apiRepository.EnableCache = value;
                }
            }
        }

        public int EnableEventFlags
        {
            get
            {
                int result = 0;

                if (_apiRepository != null)
                {
                    result = _apiRepository.EnableEventFlags;
                }

                return result;
            }

            set
            {
                if (_apiRepository != null)
                {
                    _apiRepository.EnableEventFlags = value;
                }
            }
        }

        public bool EnableUIUpdates
        {
            get
            {
                bool result = false;

                if (_apiRepository != null)
                {
                    result = _apiRepository.EnableUIUpdates;
                }

                return result;
            }

            set
            {
                if (_apiRepository != null)
                {
                    _apiRepository.EnableUIUpdates = value;
                }
            }
        }

        public bool FlagUpdate
        {
            get
            {
                bool result = false;

                if (_apiRepository != null)
                {
                    result = _apiRepository.FlagUpdate;
                }

                return result;
            }

            set
            {
                if (_apiRepository != null)
                {
                    _apiRepository.FlagUpdate = value;
                }
            }
        }

        public string InstanceGUID
        {
            get
            {
                string result = "";

                if (_apiRepository != null)
                {
                    result = _apiRepository.InstanceGUID;
                }

                return result;
            }
        }

        public bool IsSecurityEnabled
        {
            get
            {
                bool result = false;

                if (_apiRepository != null)
                {
                    result = _apiRepository.IsSecurityEnabled;
                }

                return result;
            }
        }

        public Collection Issues => throw new NotImplementedException();

        public string LastUpdate
        {
            get
            {
                string result = "";

                if (_apiRepository != null)
                {
                    result = _apiRepository.LastUpdate;
                }

                return result;
            }
        }

        public int LibraryVersion
        {
            get
            {
                int result = 0;

                if (_apiRepository != null)
                {
                    result = _apiRepository.LibraryVersion;
                }

                return result;
            }
        }

        public Collection Models
        {
            get
            {
                GenericCollection<Package> result = new GenericCollection<Package>();

                try
                {
                    result.AddRange(_packageCache.FindAll(package => package.ParentID == 0));
                }
                catch
                {

                }

                return result;
            }
        }

        public ObjectType ObjectType { get; } = ObjectType.otRepository;

        private string _projectGUID = string.Empty;

        public string ProjectGUID => _projectGUID;

        public Collection PropertyTypes => throw new NotImplementedException();

        public Collection Resources => throw new NotImplementedException();

        public Collection Stereotypes => throw new NotImplementedException();

        public bool SuppressEADialogs
        {
            get
            {
                bool result = false;

                if (_apiRepository != null)
                {
                    result = _apiRepository.SuppressEADialogs;
                }

                return result;
            }

            set
            {
                if (_apiRepository != null)
                {
                    _apiRepository.SuppressEADialogs = value;
                }
            }
        }

        public bool SuppressSecurityDialog
        {
            get
            {
                bool result = false;

                if (_apiRepository != null)
                {
                    result = _apiRepository.SuppressSecurityDialog;
                }

                return result;
            }

            set
            {
                if (_apiRepository != null)
                {
                    _apiRepository.SuppressSecurityDialog = value;
                }
            }
        }

        public Collection Tasks => throw new NotImplementedException();

        public Collection Terms => throw new NotImplementedException();

        public List<Package> AllPackages
        {
            get
            {
                return _packageCache;
            }
        }

        public List<Element> AllElements
        {
            get
            {
                return _elementCache;
            }
        }

        public List<Connector> AllConnectors
        {
            get
            {
                return _connectorCache;
            }
        }

        public void ActivateDiagram(int DiagramID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ActivateDiagram(DiagramID);
            }
        }

        public bool ActivatePerspective(string Perspective, int Options)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.ActivatePerspective(Perspective, Options);
            }

            return result;
        }

        public void ActivateTab(string Name)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ActivateTab(Name);
            }
        }

        public bool ActivateTechnology(string ID)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.ActivateTechnology(ID);
            }

            return result;
        }

        public bool ActivateToolbox(string Toolbox, int Options)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.ActivateToolbox(Toolbox, Options);
            }

            return result;
        }

        public bool AddDefinedSearches(string sXML)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.AddDefinedSearches(sXML);
            }

            return result;
        }

        public bool AddDocumentationPath(object Name, object Path, int Type)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.AddDocumentationPath(Name, Path, Type);
            }

            return result;
        }

        public bool AddPerspective(string Perspective, int Options)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.AddPerspective(Perspective, Options);
            }

            return result;
        }

        public object AddTab(string TabName, string ControlID)
        {
            object result = null;

            if (_apiRepository != null)
            {
                result = _apiRepository.AddTab(TabName, ControlID);
            }

            return result;
        }

        public object AddWindow(string TabName, string ControlID)
        {
            object result = null;

            if (_apiRepository != null)
            {
                result = _apiRepository.AddWindow(TabName, ControlID);
            }

            return result;
        }

        public void AdviseConnectorChange(int ConnectorID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.AdviseConnectorChange(ConnectorID);
            }
        }

        public void AdviseElementChange(int ElementID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.AdviseElementChange(ElementID);
            }
        }

        public bool ChangeLoginUser(string Name, string Password)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.ChangeLoginUser(Name, Password);
            }

            return result;
        }

        public bool ClearAuditLogs(object StateDateTime, object EndDateTime)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.ClearAuditLogs(StateDateTime, EndDateTime);
            }

            return result;
        }

        public int ClearObjectFlags(string flagSet, int objectID)
        {
            throw new NotImplementedException();
        }

        public void ClearOutput(string Name)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ClearOutput(Name);
            }
        }

        public void CloseAddins()
        {
            if (_apiRepository != null)
            {
                _apiRepository.CloseAddins();
            }
        }

        public void CloseDiagram(int DiagramID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.CloseDiagram(DiagramID);
            }
        }

        public void CloseFile()
        {
            if (_apiRepository != null)
            {
                _apiRepository.CloseFile();
            }
        }

        public void CreateOutputTab(string Name)
        {
            if (_apiRepository != null)
            {
                _apiRepository.CreateOutputTab(Name);
            }
        }

        public string CustomCommand(string ClassName, string MethodName, string Parameters)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.CustomCommand(ClassName, MethodName, Parameters);
            }

            return result;
        }

        public bool DefineOverlay(string flagSet, string image)
        {
            throw new NotImplementedException();
        }

        public bool DeletePerspective(string Perspective, int Options)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.DeletePerspective(Perspective, Options);
            }

            return result;
        }

        public bool DeleteTechnology(string ID)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.DeleteTechnology(ID);
            }

            return result;
        }

        public void EnsureOutputVisible(string Name)
        {
            if (_apiRepository != null)
            {
                _apiRepository.EnsureOutputVisible(Name);
            }
        }

        public void Execute(string SQL)
        {
            if (_apiRepository != null)
            {
                _apiRepository.Execute(SQL);
            }
        }

        public void ExecutePackageBuildScript(int ScriptOptions, string PackageGUID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ExecutePackageBuildScript(ScriptOptions, PackageGUID);
            }
        }

        public void Exit()
        {
            if (_apiRepository != null)
            {
                _apiRepository.Exit();
            }
        }

        public string ExtractImagesFromNote(object Notes, object absPath, object imagePath, int applyMapOption)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.ExtractImagesFromNote(Notes, absPath, imagePath, applyMapOption);
            }

            return result;
        }

        public bool GenerateMDGTechnology(string mtsFilename)
        {
            throw new NotImplementedException();
        }

        public string GetActivePerspective()
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetActivePerspective();
            }

            return result;
        }

        public MDD4All.EAFacade.DataModels.Contracts.Attribute GetAttributeByGuid(string GUID)
        {
            MDD4All.EAFacade.DataModels.Contracts.Attribute result = null;

            if (_apiRepository != null)
            {
                EAAPI.Attribute apiAttribute = _apiRepository.GetAttributeByGuid(GUID);

                if (apiAttribute != null)
                {
                    EADM.AttributeDataModel attribute = new EADM.AttributeDataModel(apiAttribute);

                    attribute.Repository = this;

                    result = attribute;
                }
            }

            return result;
        }

        public MDD4All.EAFacade.DataModels.Contracts.Attribute GetAttributeByID(int AttributeID)
        {
            MDD4All.EAFacade.DataModels.Contracts.Attribute result = null;

            if (_apiRepository != null)
            {
                EAAPI.Attribute apiAttribute = _apiRepository.GetAttributeByID(AttributeID);

                if (apiAttribute != null)
                {
                    EADM.AttributeDataModel attribute = new EADM.AttributeDataModel(apiAttribute);

                    attribute.Repository = this;

                    result = attribute;
                }
            }

            return result;
        }

        public Connector GetConnectorByGuid(string guid)
        {
            Connector result = null;

            result = _connectorCache.Find(connector => connector.ConnectorGUID == guid);

            return result;
        }

        public Connector GetConnectorByID(int connectorID)
        {
            Connector result = null;

            result = _connectorCache.Find(connector => connector.ConnectorID == connectorID);

            return result;
        }

        public ObjectType GetContextItem(ref object Item)
        {
            ObjectType result = ObjectType.otNone;

            if (_apiRepository != null)
            {
                result = (ObjectType)_apiRepository.GetContextItem(out Item);
            }

            return result;
        }

        public ObjectType GetContextItemType()
        {
            ObjectType result = ObjectType.otNone;

            if (_apiRepository != null)
            {
                result = (ObjectType)_apiRepository.GetContextItemType();
            }

            return result;
        }

        public object GetContextObject()
        {
            object result = null;

            if (_apiRepository != null)
            {
                result = _apiRepository.GetContextObject();
            }

            return result;
        }

        public string GetCounts()
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetCounts();
            }

            return result;
        }

        public Diagram GetCurrentDiagram()
        {
            Diagram result = null;

            if (_apiRepository != null)
            {
                EAAPI.Diagram apiDiagram = _apiRepository.GetCurrentDiagram();

                if (apiDiagram != null)
                {
                    EADM.DiagramDataModel diagram = new EADM.DiagramDataModel(apiDiagram);

                    diagram.Repository = this;

                    result = diagram;
                }
            }

            return result;
        }

        public string GetCurrentLoginUser(bool GetGuid)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetCurrentLoginUser(GetGuid);
            }

            return result;
        }

        public object GetDiagramByGuid(string guid)
        {
            Diagram result = null;

            result = _diagramCache.Find(diagram => diagram.DiagramGUID == guid);

            return result;
        }

        public Diagram GetDiagramByID(int diagramID)
        {
            Diagram result = null;

            result = _diagramCache.Find(diagram => diagram.DiagramID == diagramID);

            return result;
        }

        public Element GetElementByGuid(string guid)
        {
            Element result = null;


            result = _elementCache.Find(element => element.ElementGUID == guid);

            return result;
        }

        public Element GetElementByID(int ElementID)
        {
            Element result = null;


            result = _elementCache.Find(element => element.ElementID == ElementID);

            return result;
        }

        public Collection GetElementsByQuery(string QueryName, string SearchTerm)
        {
            throw new NotImplementedException();
        }

        public Collection GetElementSet(string IDList, int Unused)
        {
            throw new NotImplementedException();
        }

        public string GetFieldFromFormat(string Format, string Text)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetFieldFromFormat(Format, Text);
            }

            return result;
        }

        public string GetFormatFromField(string Format, string Text)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetFormatFromField(Format, Text);
            }

            return result;
        }

        public string GetFormattedName(object GUID, int FlagInclude, object Separator, int FlagFormat)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetFormattedName(GUID, FlagInclude, Separator, FlagFormat);
            }

            return result;
        }

        public string GetGapAnalysisMatrix()
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetGapAnalysisMatrix();
            }

            return result;
        }

        public string GetLastError()
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetLastError();
            }

            return result;
        }

        public string GetLocalPath(string sType, string sPath)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetLocalPath(sType, sPath);
            }

            return result;
        }

        public Package GetPackageByGuid(string guid)
        {
            Package result = _packageCache.Find(package => package.PackageGUID == guid);

            return result;
        }

        public Package GetPackageByID(int PackageID)
        {
            Package result = null;


            result = _packageCache.Find(package => package.PackageID == PackageID);

            return result;
        }

        public Project GetProjectInterface()
        {
            Project result = null;

            if (_apiRepository != null)
            {
                EAAPI.Project apiProject = _apiRepository.GetProjectInterface();

                if (apiProject != null)
                {
                    result = new EADM.ProjectDataModel(apiProject);
                }
            }

            return result;
        }

        public string GetRelationshipMatrix()
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetRelationshipMatrix();
            }

            return result;
        }

        public string GetTechnologyVersion(string ID)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetTechnologyVersion(ID);
            }

            return result;
        }

        public Collection GetTreeSelectedElements()
        {
            throw new NotImplementedException();
        }

        public ObjectType GetTreeSelectedItem(ref object Item)
        {
            ObjectType result = ObjectType.otNone;

            if (_apiRepository != null)
            {
                result = (ObjectType)_apiRepository.GetTreeSelectedItem(out Item);
            }

            return result;
        }

        public ObjectType GetTreeSelectedItemType()
        {
            ObjectType result = ObjectType.otNone;

            if (_apiRepository != null)
            {
                result = (ObjectType)_apiRepository.GetTreeSelectedItemType();
            }

            return result;
        }

        public object GetTreeSelectedObject()
        {
            object result = null;

            if (_apiRepository != null)
            {
                result = _apiRepository.GetTreeSelectedObject();
            }

            return result;
        }

        public Package GetTreeSelectedPackage()
        {
            Package result = null;

            if (_apiRepository != null)
            {
                EAAPI.Package apiPackage = _apiRepository.GetTreeSelectedPackage();

                if (apiPackage != null)
                {
                    result = new EADM.PackageDataModel(apiPackage, this, this);
                }
            }

            return result;
        }

        public string GetTreeXML(int RootPackageID)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetTreeXML(RootPackageID);
            }

            return result;
        }

        public string GetTreeXMLByGUID(string GUID)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetTreeXMLByGUID(GUID);
            }

            return result;
        }

        public string GetTreeXMLForElement(int ElementID)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.GetTreeXMLForElement(ElementID);
            }

            return result;
        }

        public string HasPerspective(string Perspective)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.HasPerspective(Perspective);
            }

            return result;
        }

        public void HideAddinWindow()
        {
            if (_apiRepository != null)
            {
                _apiRepository.HideAddinWindow();
            }
        }

        public void ImportPackageBuildScripts(string PackageGUID, string BuildScriptXML)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ImportPackageBuildScripts(PackageGUID, BuildScriptXML);
            }
        }

        public bool ImportRASAsset(string pkgGUID, string protocol, string servername, string model, string storage, string rasGUID, string Version, string Password)
        {
            throw new NotImplementedException();
        }

        public bool ImportTechnology(string Technology)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.ImportTechnology(Technology);
            }

            return result;
        }

        public int InvokeConstructPicker(object ConstructType)
        {
            int result = 0;

            if (_apiRepository != null)
            {
                result = _apiRepository.InvokeConstructPicker(ConstructType);
            }

            return result;
        }

        public string InvokeFileDialog(object FilterString, int DefaultFilterIndex, int Flags)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.InvokeFileDialog(FilterString, DefaultFilterIndex, Flags);
            }

            return result;
        }

        public int IsTabOpen(string TabName)
        {
            int result = 0;

            if (_apiRepository != null)
            {
                result = _apiRepository.IsTabOpen(TabName);
            }

            return result;
        }

        public bool IsTechnologyEnabled(string ID)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.IsTechnologyEnabled(ID);
            }

            return result;
        }

        public bool IsTechnologyLoaded(string ID)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.IsTechnologyLoaded(ID);
            }

            return result;
        }

        public void LoadAddins()
        {
            if (_apiRepository != null)
            {
                _apiRepository.LoadAddins();
            }
        }

        public string MarkupText(object Text)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.MarkupText(Text);
            }

            return result;
        }

        public void OpenDiagram(int DiagramID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.OpenDiagram(DiagramID);
            }
        }

        public bool OpenFile(string filePath)
        {
            bool result = false;

            _apiRepository = new EAAPI.Repository();

            logger.Debug("Starting EA...");
            if (_apiRepository.OpenFile(filePath))
            {
                logger.Debug("EA is open.");
                CacheAll();
                result = true;
            }

            return result;
        }

        public bool OpenFile2(string filePath, string username, string password)
        {
            bool result = false;

            _apiRepository = new EAAPI.Repository();

            if (_apiRepository.OpenFile2(filePath, username, password))
            {
                CacheAll();
                result = true;
            }

            return result;
        }

        public bool OpenFileInEditor(object Name)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.OpenFileInEditor(Name);
            }

            return result;
        }

        public Collection ProjectRoles()
        {
            throw new NotImplementedException();
        }

        public void RefreshModelView(int PackageID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.RefreshModelView(PackageID);
            }
        }

        public void RefreshOpenDiagrams(bool FullReload)
        {
            if (_apiRepository != null)
            {
                _apiRepository.RefreshOpenDiagrams(FullReload);
            }
        }

        public void ReloadDiagram(int DiagramID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ReloadDiagram(DiagramID);
            }
        }

        public void ReloadPackage(int PackageID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ReloadPackage(PackageID);
            }
        }

        public void RemoveOutputTab(string Name)
        {
            if (_apiRepository != null)
            {
                _apiRepository.RemoveOutputTab(Name);
            }
        }

        public void RemoveTab(string Name)
        {
            if (_apiRepository != null)
            {
                _apiRepository.RemoveTab(Name);
            }
        }

        public bool RemoveWindow(object TabName)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.RemoveWindow(TabName);
            }

            return result;
        }

        public string RepositoryType()
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.RepositoryType();
            }

            return result;
        }

        public void RunModelSearch(string QueryName, string SearchTerm, string SearchOptions, string SearchData)
        {
            if (_apiRepository != null)
            {
                _apiRepository.RunModelSearch(QueryName, SearchTerm, SearchOptions, SearchData);
            }
        }

        public void SaveAllDiagrams()
        {
            if (_apiRepository != null)
            {
                _apiRepository.SaveAllDiagrams();
            }
        }

        public bool SaveAuditLogs(string FilePath, object StateDateTime, object EndDateTime)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.SaveAuditLogs(FilePath, StateDateTime, EndDateTime);
            }

            return result;
        }

        public void SaveDiagram(int DiagramID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.SaveDiagram(DiagramID);
            }
        }

        public bool SaveDiagramAsUMLProfile(string dgmGUID, string FileName)
        {
            throw new NotImplementedException();
        }

        public bool SaveImageToPath(object imagename, object Path)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.SaveImageToPath(imagename, Path);
            }

            return result;
        }

        public bool SavePackageAsUMLProfile(string pkgGUID, string FileName)
        {
            throw new NotImplementedException();
        }

        public bool ScanXMIAndReconcile()
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.ScanXMIAndReconcile();
            }

            return result;
        }

        public void SetMarkupTerms(object Terms)
        {
            if (_apiRepository != null)
            {
                _apiRepository.SetMarkupTerms(Terms);
            }
        }

        public int SetObjectFlags(string flagSet, int objectID, int Flags)
        {
            throw new NotImplementedException();
        }

        public void SetReplacementTerms(object Terms)
        {
            if (_apiRepository != null)
            {
                _apiRepository.SetReplacementTerms(Terms);
            }
        }

        public void SetUIPerspective(string Perspective)
        {
            if (_apiRepository != null)
            {
                _apiRepository.SetUIPerspective(Perspective);
            }
        }

        public bool ShowAddinWindow(object TabName)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.ShowAddinWindow(TabName);
            }

            return result;
        }

        public void ShowBrowser(string TabName, string URL)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ShowBrowser(TabName, URL);
            }
        }

        public void ShowDynamicHelp(string Topic)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ShowDynamicHelp(Topic);
            }
        }

        public void ShowInProjectView(object Object)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ShowInProjectView(Object);
            }
        }

        public void ShowProfileToolbox(string Technology, string Profile, bool Show)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ShowProfileToolbox(Technology, Profile, Show);
            }
        }

        public void ShowWindow(int Show)
        {
            if (_apiRepository != null)
            {
                _apiRepository.ShowWindow(Show);
            }
        }

        public string SQLQuery(string SQL)
        {
            string result = "";

            if (_apiRepository != null)
            {
                result = _apiRepository.SQLQuery(SQL);
            }

            return result;
        }

        public bool SynchProfile(object Profile, object Stereotype)
        {
            bool result = false;

            if (_apiRepository != null)
            {
                result = _apiRepository.SynchProfile(Profile, Stereotype);
            }

            return result;
        }

        public void VersionControlResynchPkgStatuses(bool ClearSettings)
        {
            if (_apiRepository != null)
            {
                _apiRepository.VersionControlResynchPkgStatuses(ClearSettings);
            }
        }

        public void WriteOutput(string Name, string String, int ID)
        {
            if (_apiRepository != null)
            {
                _apiRepository.WriteOutput(Name, String, ID);
            }
        }

        public int __TempDebug(int No, DateTime No2, ref int pNo3)
        {
            int result = 0;

            if (_apiRepository != null)
            {
                result = _apiRepository.__TempDebug(No, No2, out pNo3);
            }

            return result;
        }
    }
}
