using MDD4All.EAFacade.DataModels.Contracts;
using MDD4All.EAFacade.DataAccess.Cached.Internal.Collections;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class DiagramDataModel : RepositoryElementDataModel, Diagram
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DiagramDataModel()
        {
            DiagramObjects = new DiagramObjectCollection(this);
            DiagramLinks = new DiagramLinkCollection(this);
        }

        public DiagramDataModel(XElement tObjectQueryRow, Repository repository)
        {
            DiagramObjects = new DiagramObjectCollection(this);
            DiagramLinks = new DiagramLinkCollection(this);

            Repository = repository;

            try
            {
                DiagramID = int.Parse(tObjectQueryRow.Element("Diagram_ID").Value);
                PackageID = int.Parse(tObjectQueryRow.Element("Package_ID").Value);
                ParentID = int.Parse(tObjectQueryRow.Element("ParentID").Value);
                Type = tObjectQueryRow.Element("Diagram_Type").Value;
                Name = tObjectQueryRow.Element("Name").Value;
                Version = tObjectQueryRow.Element("Version").Value;
                Author = tObjectQueryRow.Element("Author").Value;

                int showDetails = 0;

                if (int.TryParse(tObjectQueryRow.Element("ShowDetails").Value, out showDetails))
                {
                    ShowDetails = showDetails;
                }

                Notes = tObjectQueryRow.Element("Notes").Value;

                Stereotype = tObjectQueryRow.Element("Stereotype").Value;
                DiagramGUID = tObjectQueryRow.Element("ea_guid").Value;

                int cx = 0;

                if (int.TryParse(tObjectQueryRow.Element("cx").Value, out cx))
                {
                    this.cx = cx;
                }

                int cy = 0;

                if (int.TryParse(tObjectQueryRow.Element("cy").Value, out cy))
                {
                    this.cy = cy;
                }

                CreatedDate = DateTime.Parse(tObjectQueryRow.Element("CreatedDate").Value);
                ModifiedDate = DateTime.Parse(tObjectQueryRow.Element("ModifiedDate").Value);

                StyleEx = tObjectQueryRow.Element("StyleEx").Value;

                ShowPublic = tObjectQueryRow.Element("AttPub").Value == "1";
                ShowPrivate = tObjectQueryRow.Element("AttPri").Value == "1";
                ShowProtected = tObjectQueryRow.Element("AttPro").Value == "1";
                Orientation = tObjectQueryRow.Element("Orientation").Value;
                ShowPackageContents = tObjectQueryRow.Element("ShowPackageContents").Value == "1";
                IsLocked = tObjectQueryRow.Element("Locked").Value == "1";

                int scale = 0;

                if (int.TryParse(tObjectQueryRow.Element("Scale").Value, out scale))
                {
                    Scale = scale;
                }

            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public DiagramDataModel(EAAPI.Diagram apiDiagram)
        {
            DiagramObjects = new DiagramObjectCollection(this);
            DiagramLinks = new DiagramLinkCollection(this);

            _apiDiagram = apiDiagram;

            DiagramID = apiDiagram.DiagramID;
            Type = apiDiagram.Type;
            Name = apiDiagram.Name;
            Notes = apiDiagram.Notes;
            PackageID = apiDiagram.PackageID;
            Stereotype = apiDiagram.Stereotype;
            DiagramGUID = apiDiagram.DiagramGUID;
            ParentID = apiDiagram.ParentID;
            Version = apiDiagram.Version;
            Author = apiDiagram.Author;
            ShowDetails = apiDiagram.ShowDetails;
            PageWidth = apiDiagram.PageWidth;
            PageHeight = apiDiagram.PageHeight;
            CreatedDate = apiDiagram.CreatedDate;
            ModifiedDate = apiDiagram.ModifiedDate;
            StyleEx = apiDiagram.StyleEx;

            ShowPublic = apiDiagram.ShowPublic;
            ShowPrivate = apiDiagram.ShowPrivate;
            ShowProtected = apiDiagram.ShowProtected;
            Orientation = apiDiagram.Orientation;
            ShowPackageContents = apiDiagram.ShowPackageContents;
            IsLocked = apiDiagram.IsLocked;
            Scale = apiDiagram.Scale;
        }

        private EAAPI.Diagram? _apiDiagram;

        private EAAPI.Diagram? ApiDiagram
        {
            get
            {
                if (_apiDiagram == null)
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        _apiDiagram = apiRepository.GetDiagramByID(DiagramID);
                    }
                }

                return _apiDiagram;
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

                if (ApiDiagram != null)
                {
                    ApiDiagram.Author = value;
                }
            }
        }

        private DateTime _createdDate;

        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }

            set
            {
                _createdDate = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.CreatedDate = value;
                }
            }
        }

        private int _cx;

        public int cx
        {
            get
            {
                return _cx;
            }

            set
            {
                _cx = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.cx = value;
                }
            }
        }

        private int _cy;

        public int cy
        {
            get
            {
                return _cy;
            }

            set
            {
                _cy = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.cy = value;
                }
            }
        }

        private string _diagramGUID = "";

        public string DiagramGUID
        {
            get
            {
                return _diagramGUID;
            }

            set
            {
                _diagramGUID = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.DiagramGUID = value;
                }
            }
        }

        public int DiagramID { get; private set; }

        public GenericCollection<DiagramLink> DiagramLinks { get; set; }

        public GenericCollection<DiagramObject> DiagramObjects { get; set; }

        private string _extendedStyle = "";

        public string ExtendedStyle
        {
            get
            {
                return _extendedStyle;
            }

            set
            {
                _extendedStyle = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.ExtendedStyle = value;
                }
            }
        }

        private string _filterElements = "";

        public string FilterElements
        {
            get
            {
                return _filterElements;
            }

            set
            {
                _filterElements = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.FilterElements = value;
                }
            }
        }

        private bool _highlightImports;

        public bool HighlightImports
        {
            get
            {
                return _highlightImports;
            }

            set
            {
                _highlightImports = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.HighlightImports = value;
                }
            }
        }

        private bool _isLocked;

        public bool IsLocked
        {
            get
            {
                return _isLocked;
            }

            set
            {
                _isLocked = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.IsLocked = value;
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

                if (ApiDiagram != null)
                {
                    ApiDiagram.MetaType = value;
                }
            }
        }

        private DateTime _modifiedDate;

        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }

            set
            {
                _modifiedDate = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.ModifiedDate = value;
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

                if (ApiDiagram != null)
                {
                    ApiDiagram.Name = value;
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

                if (ApiDiagram != null)
                {
                    ApiDiagram.Notes = value;
                }
            }
        }

        public ObjectType ObjectType { get; } = ObjectType.otDiagram;

        private string _orientation = "";

        public string Orientation
        {
            get
            {
                return _orientation;
            }

            set
            {
                _orientation = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.Orientation = value;
                }
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

                if (ApiDiagram != null)
                {
                    ApiDiagram.PackageID = value;
                }
            }
        }

        public int PageHeight { get; set; }

        public int PageWidth { get; set; }

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

                if (ApiDiagram != null)
                {
                    ApiDiagram.ParentID = value;
                }
            }
        }

        private int _scale;

        public int Scale
        {
            get
            {
                return _scale;
            }

            set
            {
                _scale = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.Scale = value;
                }
            }
        }

        public Connector SelectedConnector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Collection SelectedObjects => throw new NotImplementedException();

        private int _showDetails;

        public int ShowDetails
        {
            get
            {
                return _showDetails;
            }

            set
            {
                _showDetails = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.ShowDetails = value;
                }
            }
        }

        private bool _showPackageContents;

        public bool ShowPackageContents
        {
            get
            {
                return _showPackageContents;
            }

            set
            {
                _showPackageContents = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.ShowPackageContents = value;
                }
            }
        }

        private bool _showPrivate;

        public bool ShowPrivate
        {
            get
            {
                return _showPrivate;
            }

            set
            {
                _showPrivate = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.ShowPrivate = value;
                }
            }
        }

        private bool _showProtected;

        public bool ShowProtected
        {
            get
            {
                return _showProtected;
            }

            set
            {
                _showProtected = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.ShowProtected = value;
                }
            }
        }

        private bool _showPublic;

        public bool ShowPublic
        {
            get
            {
                return _showPublic;
            }

            set
            {
                _showPublic = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.ShowPublic = value;
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

                if (ApiDiagram != null)
                {
                    ApiDiagram.Stereotype = value;
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

                if (ApiDiagram != null)
                {
                    ApiDiagram.StereotypeEx = value;
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

                if (ApiDiagram != null)
                {
                    ApiDiagram.StyleEx = value;
                }
            }
        }

        public SwimlaneDef SwimlaneDef => throw new NotImplementedException();

        private string _swimlanes = "";

        public string Swimlanes
        {
            get
            {
                return _swimlanes;
            }

            set
            {
                _swimlanes = value;

                if (ApiDiagram != null)
                {
                    ApiDiagram.Swimlanes = value;
                }
            }
        }

        public string Type { get; set; } = "";

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

                if (ApiDiagram != null)
                {
                    ApiDiagram.Version = value;
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

        public bool FindElementInDiagram(int NewVal)
        {
            throw new NotImplementedException();
        }

        public DiagramObject GetDiagramObjectByID(int nID, string sDUID)
        {
            throw new NotImplementedException();
        }

        public string GetLastError()
        {
            string result = "";

            if (ApiDiagram != null)
            {
                result = ApiDiagram.GetLastError();
            }

            return result;
        }

        public string ReadStyle(string Style)
        {
            throw new NotImplementedException();
        }

        public bool ReleaseUserLock()
        {
            throw new NotImplementedException();
        }

        public void ReorderMessages()
        {
            throw new NotImplementedException();
        }

        public bool SaveAsPDF(string sFilename)
        {
            throw new NotImplementedException();
        }

        public bool SaveImagePage(int x, int y, int sizeX, int sizeY, string FileName, int Flags)
        {
            throw new NotImplementedException();
        }

        public void ShowAsElementList(bool ShowAsList, bool Persist)
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            bool result = true;

            if (ApiDiagram != null)
            {
                result = ApiDiagram.Update();
            }

            return result;
        }

        public bool VirtualizeConnector(int connId, int action, int x, int y)
        {
            throw new NotImplementedException();
        }

        public int VirtualizedEnd(int connId)
        {
            throw new NotImplementedException();
        }

        public void WriteStyle(string Style, string Value)
        {
            throw new NotImplementedException();
        }
    }
}
