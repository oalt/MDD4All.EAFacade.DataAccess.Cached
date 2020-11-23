using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class DiagramLinkDataModel : DiagramLink
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DiagramLinkDataModel()
        {

        }

        public DiagramLinkDataModel(XElement tObjectQueryRow)
        {
            try
            {
                DiagramID = int.Parse(tObjectQueryRow.Element("DiagramID").Value);
                ConnectorID = int.Parse(tObjectQueryRow.Element("ConnectorID").Value);
                Geometry = tObjectQueryRow.Element("Geometry").Value;
                Style = tObjectQueryRow.Element("Style").Value;
                IsHidden = bool.Parse(tObjectQueryRow.Element("Hidden").Value);
                Path = tObjectQueryRow.Element("Path").Value;
                InstanceID = int.Parse(tObjectQueryRow.Element("Instance_ID").Value);


            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public DiagramLinkDataModel(EAAPI.DiagramLink apiDiagramLink)
        {
            DiagramID = apiDiagramLink.DiagramID;
            ConnectorID = apiDiagramLink.ConnectorID;
            Geometry = apiDiagramLink.Geometry;
            Style = apiDiagramLink.Style;
            IsHidden = apiDiagramLink.IsHidden;
            Path = apiDiagramLink.Path;
            InstanceID = apiDiagramLink.InstanceID;

        }

        private string GetStyleValue(string key)
        {
            string result = "";
            
            char[] elementSeparator = { ';' };

            char[] keyValueSeparator = { '=' };

            if (!string.IsNullOrEmpty(Style))
            {

                string[] styleElements = Style.Split(elementSeparator);

                foreach (string styleElement in styleElements)
                {
                    if (!string.IsNullOrWhiteSpace(styleElement))
                    {
                        string[] styleElementSplitted = styleElement.Split(keyValueSeparator);

                        if(styleElementSplitted[0] == key)
                        {
                            result = styleElementSplitted[1];
                            break;
                        }

                    }
                }

            }

            return result;
        }

        public int ConnectorID { get; set; }

        public int DiagramID { get; set; }
        
        public string Geometry { get; set; }
        
        public bool HiddenLabels { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int InstanceID { get; set; }
        
        public bool IsHidden { get; set; }
        
        public int LineColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public LinkLineStyle LineStyle 
        { 
            get
            {
                LinkLineStyle result = LinkLineStyle.LineStyleAutoRouting;

                string treeValue = GetStyleValue("TREE");

                if(treeValue == "OR")
                {
                    result = LinkLineStyle.LineStyleOrthogonalRounded;
                }

                return result;
            }
            
            set { } 
        }
        
        public int LineWidth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ObjectType ObjectType { get; } = ObjectType.otDiagramLink;

        public string Path { get; set; }

        public string SourceInstanceUID => throw new NotImplementedException();

        public string Style { get; set; }
        
        public int SuppressSegment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string TargetInstanceUID => throw new NotImplementedException();

        public string GetLastError()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
