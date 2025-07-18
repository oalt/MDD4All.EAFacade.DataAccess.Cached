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

        private EAAPI.Repository _apiRepository;
      
        public CachedRepository()
        {

        }

        public CachedRepository(EAAPI.Repository repository)
        {
            _apiRepository = repository;

            //CacheAll();
        }

        public void CacheAll()
        {
            logger.Debug("Caching model...");

            DateTime startTime = DateTime.Now;

            if(_apiRepository != null)
            {
                _connectionString = _apiRepository.ConnectionString;

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
                        TaggedValue taggedValue = new EADM.TaggedValueDataModel(taggedValueRow);

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
                        TaggedValue taggedValue = new EADM.TaggedValueDataModel(taggedValueRow);

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
                        DataModels.Contracts.Attribute attribute = new EADM.AttributeDataModel(attributeRow);

                        ((GenericCollection<DataModels.Contracts.Attribute>)element.Attributes).Add(attribute);
                    }
                }

                _elementCache.Add(element);

            }
        }

        public void InitializeConnectorCache()
        {
            _connectorCache = new List<Connector>();

            string xml = _apiRepository.SQLQuery("select * from t_connector");

            XElement rootElement = XElement.Parse(xml);

            XElement datasetElement = rootElement.Element("Dataset_0");

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
                        ConnectorTag taggedValue = new EADM.ConnectorTagDataModel(taggedValueRow);

                        connector.TaggedValues.Add(taggedValue);
                    }
                }

                _connectorCache.Add(connector);

            }
        }

        public void InitializeDiagramCache()
        {
            _diagramCache = new List<Diagram>();

            string xml = _apiRepository.SQLQuery("select * from t_diagram");

            XElement rootElement = XElement.Parse(xml);

            XElement datasetElement = rootElement.Element("Dataset_0");

            XElement dataElement = datasetElement.Element("Data");

            IEnumerable<XElement> rows = dataElement.Elements("Row");

            foreach (XElement row in rows)
            {
                EADM.DiagramDataModel diagram = new EADM.DiagramDataModel(row);


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
                        EADM.DiagramObjectDataModel diagramObject = new EADM.DiagramObjectDataModel(diagramObjectRow);

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
                        EADM.DiagramLinkDataModel diagramLink = new EADM.DiagramLinkDataModel(diagramLinkRow);

                        diagram.DiagramLinks.Add(diagramLink);
                    }
                }
            }
        }

        public event EventHandler CachingFinished; 

        public Collection Authors => throw new NotImplementedException();

        public bool BatchAppend { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Collection Clients => throw new NotImplementedException();

        private string _connectionString = string.Empty;

        public string ConnectionString => _connectionString;

        public Collection Datatypes => throw new NotImplementedException();

        public EAEditionTypes EAEdition => throw new NotImplementedException();

        public EAEditionTypes EAEditionEx => throw new NotImplementedException();

        public bool EnableCache { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int EnableEventFlags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool EnableUIUpdates { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool FlagUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string InstanceGUID => throw new NotImplementedException();

        public bool IsSecurityEnabled => throw new NotImplementedException();

        public Collection Issues => throw new NotImplementedException();

        public string LastUpdate => throw new NotImplementedException();

        public int LibraryVersion => throw new NotImplementedException();

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

        public string ProjectGUID => throw new NotImplementedException();

        public Collection PropertyTypes => throw new NotImplementedException();

        public Collection Resources => throw new NotImplementedException();

        public Collection Stereotypes => throw new NotImplementedException();

        public bool SuppressEADialogs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool SuppressSecurityDialog { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            throw new NotImplementedException();
        }

        public bool ActivatePerspective(string Perspective, int Options)
        {
            throw new NotImplementedException();
        }

        public void ActivateTab(string Name)
        {
            throw new NotImplementedException();
        }

        public bool ActivateTechnology(string ID)
        {
            throw new NotImplementedException();
        }

        public bool ActivateToolbox(string Toolbox, int Options)
        {
            throw new NotImplementedException();
        }

        public bool AddDefinedSearches(string sXML)
        {
            throw new NotImplementedException();
        }

        public bool AddDocumentationPath(object Name, object Path, int Type)
        {
            throw new NotImplementedException();
        }

        public bool AddPerspective(string Perspective, int Options)
        {
            throw new NotImplementedException();
        }

        public object AddTab(string TabName, string ControlID)
        {
            throw new NotImplementedException();
        }

        public object AddWindow(string TabName, string ControlID)
        {
            throw new NotImplementedException();
        }

        public void AdviseConnectorChange(int ConnectorID)
        {
            throw new NotImplementedException();
        }

        public void AdviseElementChange(int ElementID)
        {
            throw new NotImplementedException();
        }

        public bool ChangeLoginUser(string Name, string Password)
        {
            throw new NotImplementedException();
        }

        public bool ClearAuditLogs(object StateDateTime, object EndDateTime)
        {
            throw new NotImplementedException();
        }

        public int ClearObjectFlags(string flagSet, int objectID)
        {
            throw new NotImplementedException();
        }

        public void ClearOutput(string Name)
        {
            throw new NotImplementedException();
        }

        public void CloseAddins()
        {
            throw new NotImplementedException();
        }

        public void CloseDiagram(int DiagramID)
        {
            throw new NotImplementedException();
        }

        public void CloseFile()
        {
            throw new NotImplementedException();
        }

        public void CreateOutputTab(string Name)
        {
            throw new NotImplementedException();
        }

        public string CustomCommand(string ClassName, string MethodName, string Parameters)
        {
            throw new NotImplementedException();
        }

        public bool DefineOverlay(string flagSet, string image)
        {
            throw new NotImplementedException();
        }

        public bool DeletePerspective(string Perspective, int Options)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTechnology(string ID)
        {
            throw new NotImplementedException();
        }

        public void EnsureOutputVisible(string Name)
        {
            throw new NotImplementedException();
        }

        public void Execute(string SQL)
        {
            throw new NotImplementedException();
        }

        public void ExecutePackageBuildScript(int ScriptOptions, string PackageGUID)
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            _apiRepository.Exit();
        }

        public string ExtractImagesFromNote(object Notes, object absPath, object imagePath, int applyMapOption)
        {
            throw new NotImplementedException();
        }

        public bool GenerateMDGTechnology(string mtsFilename)
        {
            throw new NotImplementedException();
        }

        public string GetActivePerspective()
        {
            throw new NotImplementedException();
        }

        public MDD4All.EAFacade.DataModels.Contracts.Attribute GetAttributeByGuid(string GUID)
        {
            throw new NotImplementedException();
        }

        public MDD4All.EAFacade.DataModels.Contracts.Attribute GetAttributeByID(int AttributeID)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public ObjectType GetContextItemType()
        {
            throw new NotImplementedException();
        }

        public object GetContextObject()
        {
            throw new NotImplementedException();
        }

        public string GetCounts()
        {
            throw new NotImplementedException();
        }

        public Diagram GetCurrentDiagram()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentLoginUser(bool GetGuid)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public string GetFormatFromField(string Format, string Text)
        {
            throw new NotImplementedException();
        }

        public string GetFormattedName(object GUID, int FlagInclude, object Separator, int FlagFormat)
        {
            throw new NotImplementedException();
        }

        public string GetGapAnalysisMatrix()
        {
            throw new NotImplementedException();
        }

        public string GetLastError()
        {
            throw new NotImplementedException();
        }

        public string GetLocalPath(string sType, string sPath)
        {
            throw new NotImplementedException();
        }

        public Package GetPackageByGuid(string guid)
        {
            Package result = null;

            _packageCache.Find(package => package.PackageGUID == guid);

            return result;
        }

        public Package GetPackageByID(int PackageID)
        {
            Package result = null;


            result = _packageCache.Find(package => package.PackageID == PackageID);

            return result;
        }

        public string GetRelationshipMatrix()
        {
            throw new NotImplementedException();
        }

        public string GetTechnologyVersion(string ID)
        {
            throw new NotImplementedException();
        }

        public Collection GetTreeSelectedElements()
        {
            throw new NotImplementedException();
        }

        public ObjectType GetTreeSelectedItem(ref object Item)
        {
            throw new NotImplementedException();
        }

        public ObjectType GetTreeSelectedItemType()
        {
            throw new NotImplementedException();
        }

        public object GetTreeSelectedObject()
        {
            throw new NotImplementedException();
        }

        public Package GetTreeSelectedPackage()
        {
            throw new NotImplementedException();
        }

        public string GetTreeXML(int RootPackageID)
        {
            throw new NotImplementedException();
        }

        public string GetTreeXMLByGUID(string GUID)
        {
            throw new NotImplementedException();
        }

        public string GetTreeXMLForElement(int ElementID)
        {
            throw new NotImplementedException();
        }

        public string HasPerspective(string Perspective)
        {
            throw new NotImplementedException();
        }

        public void HideAddinWindow()
        {
            throw new NotImplementedException();
        }

        public void ImportPackageBuildScripts(string PackageGUID, string BuildScriptXML)
        {
            throw new NotImplementedException();
        }

        public bool ImportRASAsset(string pkgGUID, string protocol, string servername, string model, string storage, string rasGUID, string Version, string Password)
        {
            throw new NotImplementedException();
        }

        public bool ImportTechnology(string Technology)
        {
            throw new NotImplementedException();
        }

        public int InvokeConstructPicker(object ConstructType)
        {
            throw new NotImplementedException();
        }

        public string InvokeFileDialog(object FilterString, int DefaultFilterIndex, int Flags)
        {
            throw new NotImplementedException();
        }

        public int IsTabOpen(string TabName)
        {
            throw new NotImplementedException();
        }

        public bool IsTechnologyEnabled(string ID)
        {
            throw new NotImplementedException();
        }

        public bool IsTechnologyLoaded(string ID)
        {
            throw new NotImplementedException();
        }

        public void LoadAddins()
        {
            throw new NotImplementedException();
        }

        public string MarkupText(object Text)
        {
            throw new NotImplementedException();
        }

        public void OpenDiagram(int DiagramID)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Collection ProjectRoles()
        {
            throw new NotImplementedException();
        }

        public void RefreshModelView(int PackageID)
        {
            throw new NotImplementedException();
        }

        public void RefreshOpenDiagrams(bool FullReload)
        {
            throw new NotImplementedException();
        }

        public void ReloadDiagram(int DiagramID)
        {
            throw new NotImplementedException();
        }

        public void ReloadPackage(int PackageID)
        {
            throw new NotImplementedException();
        }

        public void RemoveOutputTab(string Name)
        {
            throw new NotImplementedException();
        }

        public void RemoveTab(string Name)
        {
            throw new NotImplementedException();
        }

        public bool RemoveWindow(object TabName)
        {
            throw new NotImplementedException();
        }

        public string RepositoryType()
        {
            throw new NotImplementedException();
        }

        public void RunModelSearch(string QueryName, string SearchTerm, string SearchOptions, string SearchData)
        {
            throw new NotImplementedException();
        }

        public void SaveAllDiagrams()
        {
            throw new NotImplementedException();
        }

        public bool SaveAuditLogs(string FilePath, object StateDateTime, object EndDateTime)
        {
            throw new NotImplementedException();
        }

        public void SaveDiagram(int DiagramID)
        {
            throw new NotImplementedException();
        }

        public bool SaveDiagramAsUMLProfile(string dgmGUID, string FileName)
        {
            throw new NotImplementedException();
        }

        public bool SaveImageToPath(object imagename, object Path)
        {
            throw new NotImplementedException();
        }

        public bool SavePackageAsUMLProfile(string pkgGUID, string FileName)
        {
            throw new NotImplementedException();
        }

        public bool ScanXMIAndReconcile()
        {
            throw new NotImplementedException();
        }

        public void SetMarkupTerms(object Terms)
        {
            throw new NotImplementedException();
        }

        public int SetObjectFlags(string flagSet, int objectID, int Flags)
        {
            throw new NotImplementedException();
        }

        public void SetReplacementTerms(object Terms)
        {
            throw new NotImplementedException();
        }

        public void SetUIPerspective(string Perspective)
        {
            throw new NotImplementedException();
        }

        public bool ShowAddinWindow(object TabName)
        {
            throw new NotImplementedException();
        }

        public void ShowBrowser(string TabName, string URL)
        {
            throw new NotImplementedException();
        }

        public void ShowDynamicHelp(string Topic)
        {
            throw new NotImplementedException();
        }

        public void ShowInProjectView(object Object)
        {
            throw new NotImplementedException();
        }

        public void ShowProfileToolbox(string Technology, string Profile, bool Show)
        {
            throw new NotImplementedException();
        }

        public void ShowWindow(int Show)
        {
            throw new NotImplementedException();
        }

        public string SQLQuery(string SQL)
        {
            throw new NotImplementedException();
        }

        public bool SynchProfile(object Profile, object Stereotype)
        {
            throw new NotImplementedException();
        }

        public void VersionControlResynchPkgStatuses(bool ClearSettings)
        {
            throw new NotImplementedException();
        }

        public void WriteOutput(string Name, string String, int ID)
        {
            throw new NotImplementedException();
        }

        public int __TempDebug(int No, DateTime No2, ref int pNo3)
        {
            throw new NotImplementedException();
        }
    }
}
