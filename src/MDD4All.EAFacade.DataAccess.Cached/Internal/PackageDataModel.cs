using MDD4All.EAFacade.DataModels.Contracts;
using MDD4All.EAFacade.DataAccess.Cached.Internal.Collections;
using NLog;
using System;
using System.Linq;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class PackageDataModel : RepositoryElementDataModel, Package
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        internal AbstractDataCache _abstractDataCache { get; set; }

        public PackageDataModel()
        {

        }

        public PackageDataModel(XElement tObjectQueryRow,
                                AbstractDataCache abstractDataCache,
                                Repository repository)
        {
            _abstractDataCache = abstractDataCache;
            Repository = repository;

            try
            {
                PackageID = int.Parse(tObjectQueryRow.Element("Package_ID").Value);
                Name = tObjectQueryRow.Element("Name").Value;
                ParentID = int.Parse(tObjectQueryRow.Element("Parent_ID").Value);
                Created = DateTime.Parse(tObjectQueryRow.Element("CreatedDate").Value);
                Modified = DateTime.Parse(tObjectQueryRow.Element("ModifiedDate").Value);
                Notes = tObjectQueryRow.Element("Notes").Value;
                PackageGUID = tObjectQueryRow.Element("ea_guid").Value;
                Flags = tObjectQueryRow.Element("PackageFlags").Value;

                int treePos = 0;

                if (int.TryParse(tObjectQueryRow.Element("TPos").Value, out treePos))
                {
                    TreePos = treePos;
                }

            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public PackageDataModel(EAAPI.Package apiPackage,
                                AbstractDataCache abstractDataCache,
                                Repository repository)
        {
            _abstractDataCache = abstractDataCache;
            Repository = repository;

            _apiPackage = apiPackage;

            PackageID = apiPackage.PackageID;
            Name = apiPackage.Name;
            ParentID = apiPackage.ParentID;
            Created = apiPackage.Created;
            Modified = apiPackage.Modified;
            Notes = apiPackage.Notes;
            PackageGUID = apiPackage.PackageGUID;
            TreePos = apiPackage.TreePos;
            Alias = apiPackage.Alias;
            Flags = apiPackage.Flags;
        }

        private EAAPI.Package? _apiPackage;

        private EAAPI.Package? ApiPackage
        {
            get
            {
                if (_apiPackage == null)
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        _apiPackage = apiRepository.GetPackageByID(PackageID);
                    }
                }

                return _apiPackage;
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

                if (ApiPackage != null)
                {
                    ApiPackage.Alias = value;
                }
            }
        }

        private int _batchLoad;

        public int BatchLoad
        {
            get
            {
                return _batchLoad;
            }

            set
            {
                _batchLoad = value;

                if (ApiPackage != null)
                {
                    ApiPackage.BatchLoad = value;
                }
            }
        }

        private int _batchSave;

        public int BatchSave
        {
            get
            {
                return _batchSave;
            }

            set
            {
                _batchSave = value;

                if (ApiPackage != null)
                {
                    ApiPackage.BatchSave = value;
                }
            }
        }

        private string _codePath = "";

        public string CodePath
        {
            get
            {
                return _codePath;
            }

            set
            {
                _codePath = value;

                if (ApiPackage != null)
                {
                    ApiPackage.CodePath = value;
                }
            }
        }

        public GenericCollection<Connector> Connectors => throw new NotImplementedException();

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

                if (ApiPackage != null)
                {
                    ApiPackage.Created = value;
                }
            }
        }

        public GenericCollection<Diagram> Diagrams
        {
            get
            {
                PackageDiagramCollection result = new PackageDiagramCollection(this, _abstractDataCache);

                result.AddRange(_abstractDataCache._diagramCache.FindAll(diagram => diagram.PackageID == PackageID && diagram.ParentID == 0));

                return result;
            }
        }

        public Element Element
        {
            get
            {
                Element result = null;

                result = _abstractDataCache._elementCache.Find(element => element.ElementGUID == PackageGUID);

                return result;
            }

            set
            {
                ;
            }
        }

        public GenericCollection<Element> Elements
        {
            get
            {
                PackageElementCollection result = new PackageElementCollection(this, _abstractDataCache);

                result.AddRange(_abstractDataCache._elementCache.FindAll(element => element.PackageID == PackageID && element.ParentID == 0).OrderBy(element => element.TreePos));

                return result;
            }
        }

        private string _flags = string.Empty;

        public string Flags
        {
            get
            {
                return _flags;
            }

            set
            {
                _flags = value;

                if (ApiPackage != null)
                {
                    ApiPackage.Flags = value;
                }
            }
        }

        private bool _isControlled;

        public bool IsControlled
        {
            get
            {
                return _isControlled;
            }

            set
            {
                _isControlled = value;

                if (ApiPackage != null)
                {
                    ApiPackage.IsControlled = value;
                }
            }
        }

        public bool IsModel
        {
            get
            {
                bool result = (ParentID == 0);

                return result;
            }
        }

        private bool _isNamespace;

        public bool IsNamespace
        {
            get
            {
                return _isNamespace;
            }

            set
            {
                _isNamespace = value;

                if (ApiPackage != null)
                {
                    ApiPackage.IsNamespace = value;
                }
            }
        }

        private bool _isProtected;

        public bool IsProtected
        {
            get
            {
                return _isProtected;
            }

            set
            {
                _isProtected = value;

                if (ApiPackage != null)
                {
                    ApiPackage.IsProtected = value;
                }
            }
        }

        public bool IsVersionControlled => throw new NotImplementedException();

        public DateTime LastLoadDate => throw new NotImplementedException();

        public DateTime LastSaveDate => throw new NotImplementedException();

        private bool _logXML;

        public bool LogXML
        {
            get
            {
                return _logXML;
            }

            set
            {
                _logXML = value;

                if (ApiPackage != null)
                {
                    ApiPackage.LogXML = value;
                }
            }
        }

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

                if (ApiPackage != null)
                {
                    ApiPackage.Modified = value;
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

                if (ApiPackage != null)
                {
                    ApiPackage.Name = value;
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

                if (ApiPackage != null)
                {
                    ApiPackage.Notes = value;
                }
            }
        }

        public ObjectType ObjectType { get; } = ObjectType.otPackage;

        private string _owner = "";

        public string Owner
        {
            get
            {
                return _owner;
            }

            set
            {
                _owner = value;

                if (ApiPackage != null)
                {
                    ApiPackage.Owner = value;
                }
            }
        }

        private string _packageGUID = "";

        public string PackageGUID
        {
            get
            {
                return _packageGUID;
            }

            set
            {
                _packageGUID = value;

                if (ApiPackage != null)
                {
                    ApiPackage.PackageGUID = value;
                }
            }
        }

        public int PackageID { get; private set; }

        public Collection Packages
        {
            get
            {
                PackageChildCollection result = new PackageChildCollection(this, _abstractDataCache);

                try
                {
                    result.AddRange(_abstractDataCache._packageCache.FindAll(package => package.ParentID == PackageID).OrderBy(p => p.TreePos));
                }
                catch
                {

                }

                return result;
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

                if (ApiPackage != null)
                {
                    ApiPackage.ParentID = value;
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

                if (ApiPackage != null)
                {
                    ApiPackage.StereotypeEx = value;
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

                if (ApiPackage != null)
                {
                    ApiPackage.TreePos = value;
                }
            }
        }

        private string _umlVersion = "";

        public string UMLVersion
        {
            get
            {
                return _umlVersion;
            }

            set
            {
                _umlVersion = value;

                if (ApiPackage != null)
                {
                    ApiPackage.UMLVersion = value;
                }
            }
        }

        private bool _useDTD;

        public bool UseDTD
        {
            get
            {
                return _useDTD;
            }

            set
            {
                _useDTD = value;

                if (ApiPackage != null)
                {
                    ApiPackage.UseDTD = value;
                }
            }
        }

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

                if (ApiPackage != null)
                {
                    ApiPackage.Version = value;
                }
            }
        }

        private string _xmlPath = "";

        public string XMLPath
        {
            get
            {
                return _xmlPath;
            }

            set
            {
                _xmlPath = value;

                if (ApiPackage != null)
                {
                    ApiPackage.XMLPath = value;
                }
            }
        }

        public bool ApplyGroupLock(string aGroupName)
        {
            throw new NotImplementedException();
        }

        public bool ApplyGroupLockRecursive(string aGroupName, bool IncludeElements, bool IncludeDiagrams, bool IncludeSubPackages)
        {
            throw new NotImplementedException();
        }

        public bool ApplyUserLock()
        {
            throw new NotImplementedException();
        }

        public bool ApplyUserLockRecursive(bool IncludeElements, bool IncludeDiagrams, bool IncludeSubPackages)
        {
            throw new NotImplementedException();
        }

        public Package Clone()
        {
            throw new NotImplementedException();
        }

        public object FindObject(string DottedID)
        {
            throw new NotImplementedException();
        }

        public void GenerateSourceCode()
        {
            throw new NotImplementedException();
        }

        public void GetCodeProject(ref string GUID, ref string ProjectType)
        {
            throw new NotImplementedException();
        }

        public string GetLastError()
        {
            string result = "";

            if (ApiPackage != null)
            {
                result = ApiPackage.GetLastError();
            }

            return result;
        }

        public bool ReleaseUserLock()
        {
            throw new NotImplementedException();
        }

        public bool ReleaseUserLockRecursive(bool IncludeElements, bool IncludeDiagrams, bool IncludeSubPackages)
        {
            throw new NotImplementedException();
        }

        public void SetCodeProject(string GUID, string ProjectType)
        {
            throw new NotImplementedException();
        }

        public void SetReadOnly(bool ReadOnly, bool IncludeSubPkgs)
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            bool result = true;

            if (ApiPackage != null)
            {
                result = ApiPackage.Update();
            }

            return result;
        }

        public void VersionControlAdd(string ConfigGuid, string XMLFile, string Comment, bool KeepCheckedOut)
        {
            throw new NotImplementedException();
        }

        public void VersionControlCheckin(string Comment)
        {
            throw new NotImplementedException();
        }

        public void VersionControlCheckinEx(string Comment, bool PreserveCrossPkgRefs)
        {
            throw new NotImplementedException();
        }

        public void VersionControlCheckout(string Comment)
        {
            throw new NotImplementedException();
        }

        public void VersionControlGetLatest(bool ForceImport)
        {
            throw new NotImplementedException();
        }

        public int VersionControlGetStatus()
        {
            throw new NotImplementedException();
        }

        public void VersionControlPutLatest(string Comment)
        {
            throw new NotImplementedException();
        }

        public void VersionControlRemove()
        {
            throw new NotImplementedException();
        }

        public void VersionControlResynchPkgStatus(bool ClearSettings)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string result = "[Package]";

            if (Name != null)
            {
                result += " " + Name;
            }

            return result;
        }
    }
}
