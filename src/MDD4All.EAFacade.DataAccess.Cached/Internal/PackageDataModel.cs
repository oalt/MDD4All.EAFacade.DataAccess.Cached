using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class PackageDataModel : Package
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private AbstractDataCache _abstractDataCache { get; set; }

        public PackageDataModel()
        {

        }

        public PackageDataModel(XElement tObjectQueryRow, AbstractDataCache abstractDataCache)
        {
            _abstractDataCache = abstractDataCache;

            try
            {
                PackageID = int.Parse(tObjectQueryRow.Element("Package_ID").Value);
                Name = tObjectQueryRow.Element("Name").Value;
                ParentID = int.Parse(tObjectQueryRow.Element("Parent_ID").Value);
                Created = DateTime.Parse(tObjectQueryRow.Element("CreatedDate").Value);
                Modified = DateTime.Parse(tObjectQueryRow.Element("ModifiedDate").Value);
                Notes = tObjectQueryRow.Element("Notes").Value;
                PackageGUID = tObjectQueryRow.Element("ea_guid").Value;

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

        public PackageDataModel(EAAPI.Package apiPackage, AbstractDataCache abstractDataCache)
        {
            _abstractDataCache = abstractDataCache;

            PackageID = apiPackage.PackageID;
            Name = apiPackage.Name;
            ParentID = apiPackage.ParentID;
            Created = apiPackage.Created;
            Modified = apiPackage.Modified;
            Notes = apiPackage.Notes;
            PackageGUID = apiPackage.PackageGUID;
            TreePos = apiPackage.TreePos;
        }

        public string Alias { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int BatchLoad { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
       
        public int BatchSave { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string CodePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GenericCollection<Connector> Connectors => throw new NotImplementedException();

        public DateTime Created { get; set; }

        public GenericCollection<Diagram> Diagrams => throw new NotImplementedException();

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

        public GenericCollection<Element> Elements => throw new NotImplementedException();

        public string Flags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool IsControlled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsModel
        {
            get
            {
                bool result = (ParentID == 0);

                return result;
            }
        }

        public bool IsNamespace { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool IsProtected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsVersionControlled => throw new NotImplementedException();

        public DateTime LastLoadDate => throw new NotImplementedException();

        public DateTime LastSaveDate => throw new NotImplementedException();

        public bool LogXML { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public DateTime Modified { get; set; }
        
        public string Name { get; set; }

        public string Notes { get; set; }

        public ObjectType ObjectType { get; } = ObjectType.otPackage;

        public string Owner { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string PackageGUID { get; set; }

        public int PackageID { get; private set; }

        public Collection Packages
        {
            get
            {
                GenericCollection<Package> result = new GenericCollection<Package>();

                try
                {
                    result.AddRange(_abstractDataCache._packageCache.FindAll(package => package.ParentID == PackageID));
                }
                catch
                {

                }

                return result;
            }
        }

        public int ParentID { get; set; }
        
        public string StereotypeEx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int TreePos { get; set; }
        
        public string UMLVersion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool UseDTD { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Version { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string XMLPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
    }
}
