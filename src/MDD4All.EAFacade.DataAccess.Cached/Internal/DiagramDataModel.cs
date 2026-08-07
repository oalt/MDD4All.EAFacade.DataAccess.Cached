using MDD4All.EAFacade.DataModels.Contracts;
using MDD4All.EAFacade.DataAccess.Cached.Internal.Collections;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class DiagramDataModel : RepositoryElementDataModel, Diagram
    {
        public DiagramDataModel()
        {
            _diagramObjects = new DiagramObjectCollection(this);
            _diagramLinks = new DiagramLinkCollection(this);
        }

        public DiagramDataModel(XElement tObjectQueryRow, Repository repository)
        {
            _diagramObjects = new DiagramObjectCollection(this);
            _diagramLinks = new DiagramLinkCollection(this);

            Repository = repository;

            _diagramID = int.Parse(tObjectQueryRow.Element("Diagram_ID").Value);
            _packageID = int.Parse(tObjectQueryRow.Element("Package_ID").Value);
            _parentID = int.Parse(tObjectQueryRow.Element("ParentID").Value);
            _type = tObjectQueryRow.Element("Diagram_Type").Value;
            _name = tObjectQueryRow.Element("Name").Value;
            _version = tObjectQueryRow.Element("Version").Value;
            _author = tObjectQueryRow.Element("Author").Value;

            int showDetails = 0;

            if (int.TryParse(tObjectQueryRow.Element("ShowDetails").Value, out showDetails))
            {
                _showDetails = showDetails;
            }

            _notes = tObjectQueryRow.Element("Notes").Value;

            _stereotype = tObjectQueryRow.Element("Stereotype").Value;
            _diagramGUID = tObjectQueryRow.Element("ea_guid").Value;

            int cx = 0;

            if (int.TryParse(tObjectQueryRow.Element("cx").Value, out cx))
            {
                _cx = cx;
            }

            int cy = 0;

            if (int.TryParse(tObjectQueryRow.Element("cy").Value, out cy))
            {
                _cy = cy;
            }

            _createdDate = DateTime.Parse(tObjectQueryRow.Element("CreatedDate").Value);
            _modifiedDate = DateTime.Parse(tObjectQueryRow.Element("ModifiedDate").Value);

            _styleEx = tObjectQueryRow.Element("StyleEx").Value;

            _showPublic = tObjectQueryRow.Element("AttPub").Value == "1";
            _showPrivate = tObjectQueryRow.Element("AttPri").Value == "1";
            _showProtected = tObjectQueryRow.Element("AttPro").Value == "1";
            _orientation = tObjectQueryRow.Element("Orientation").Value;
            _showPackageContents = tObjectQueryRow.Element("ShowPackageContents").Value == "1";
            _isLocked = tObjectQueryRow.Element("Locked").Value == "1";

            int scale = 0;

            if (int.TryParse(tObjectQueryRow.Element("Scale").Value, out scale))
            {
                _scale = scale;
            }
        }

        public DiagramDataModel(EAAPI.Diagram apiDiagram)
        {
            _diagramObjects = new DiagramObjectCollection(this);
            _diagramLinks = new DiagramLinkCollection(this);

            _apiDiagram = apiDiagram;

            _diagramID = apiDiagram.DiagramID;
            _type = apiDiagram.Type;
            _name = apiDiagram.Name;
            _notes = apiDiagram.Notes;
            _packageID = apiDiagram.PackageID;
            _stereotype = apiDiagram.Stereotype;
            _diagramGUID = apiDiagram.DiagramGUID;
            _parentID = apiDiagram.ParentID;
            _version = apiDiagram.Version;
            _author = apiDiagram.Author;
            _showDetails = apiDiagram.ShowDetails;
            _pageWidth = apiDiagram.PageWidth;
            _pageHeight = apiDiagram.PageHeight;
            _createdDate = apiDiagram.CreatedDate;
            _modifiedDate = apiDiagram.ModifiedDate;
            _styleEx = apiDiagram.StyleEx;

            _showPublic = apiDiagram.ShowPublic;
            _showPrivate = apiDiagram.ShowPrivate;
            _showProtected = apiDiagram.ShowProtected;
            _orientation = apiDiagram.Orientation;
            _showPackageContents = apiDiagram.ShowPackageContents;
            _isLocked = apiDiagram.IsLocked;
            _scale = apiDiagram.Scale;
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

        private int _diagramID;

        public int DiagramID
        {
            get
            {
                return _diagramID;
            }

            private set
            {
                _diagramID = value;
            }
        }

        private GenericCollection<DiagramLink> _diagramLinks = null!;

        public GenericCollection<DiagramLink> DiagramLinks
        {
            get
            {
                return _diagramLinks;
            }

            set
            {
                _diagramLinks = value;
            }
        }

        private GenericCollection<DiagramObject> _diagramObjects = null!;

        public GenericCollection<DiagramObject> DiagramObjects
        {
            get
            {
                return _diagramObjects;
            }

            set
            {
                _diagramObjects = value;
            }
        }

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

        private int _pageHeight;

        public int PageHeight
        {
            get
            {
                return _pageHeight;
            }

            set
            {
                _pageHeight = value;
            }
        }

        private int _pageWidth;

        public int PageWidth
        {
            get
            {
                return _pageWidth;
            }

            set
            {
                _pageWidth = value;
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
