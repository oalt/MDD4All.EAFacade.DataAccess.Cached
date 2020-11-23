using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class DiagramDataModel : Diagram
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DiagramDataModel()
        {

        }

        public DiagramDataModel(XElement tObjectQueryRow)
        {
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

            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public DiagramDataModel(EAAPI.Diagram apiDiagram)
        {
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
        }

        public string Author { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public int cx { get; set; }
        
        public int cy { get; set; }

        public string DiagramGUID { get; set; }

        public int DiagramID { get; private set; }

        public GenericCollection<DiagramLink> DiagramLinks { get; set; } = new GenericCollection<DiagramLink>();

        public GenericCollection<DiagramObject> DiagramObjects { get; set; } = new GenericCollection<DiagramObject>();

        public string ExtendedStyle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string FilterElements { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool HighlightImports { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool IsLocked { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string MetaType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public DateTime ModifiedDate { get; set; }
        
        public string Name { get; set; }

        public string Notes { get; set; }

        public ObjectType ObjectType { get; } = ObjectType.otDiagram;

        public string Orientation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int PackageID { get; set; }

        public int PageHeight { get; set; }

        public int PageWidth { get; set; }

        public int ParentID { get; set; }

        public int Scale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public Connector SelectedConnector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Collection SelectedObjects => throw new NotImplementedException();

        public int ShowDetails { get; set; }
        
        public bool ShowPackageContents { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowPrivate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowProtected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowPublic { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Stereotype { get; set; }

        public string StereotypeEx { get; set; }

        public string StyleEx { get; set; }

        public SwimlaneDef SwimlaneDef => throw new NotImplementedException();

        public string Swimlanes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Type { get; set; }

        public string Version { get; set; }

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
