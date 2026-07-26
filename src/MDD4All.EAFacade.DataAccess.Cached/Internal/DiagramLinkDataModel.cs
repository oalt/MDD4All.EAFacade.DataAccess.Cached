using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class DiagramLinkDataModel : RepositoryElementDataModel, DiagramLink
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DiagramLinkDataModel()
        {

        }

        public DiagramLinkDataModel(XElement tObjectQueryRow, Repository repository)
        {
            Repository = repository;

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
            _apiDiagramLink = apiDiagramLink;

            DiagramID = apiDiagramLink.DiagramID;
            ConnectorID = apiDiagramLink.ConnectorID;
            Geometry = apiDiagramLink.Geometry;
            Style = apiDiagramLink.Style;
            IsHidden = apiDiagramLink.IsHidden;
            Path = apiDiagramLink.Path;
            InstanceID = apiDiagramLink.InstanceID;

        }

        private EAAPI.DiagramLink? _apiDiagramLink;

        private EAAPI.DiagramLink? ApiDiagramLink
        {
            get
            {
                if (_apiDiagramLink == null && ConnectorID != 0)
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        EAAPI.Diagram apiDiagram = apiRepository.GetDiagramByID(DiagramID);

                        if (apiDiagram != null)
                        {
                            for (short index = 0; index < apiDiagram.DiagramLinks.Count; index++)
                            {
                                EAAPI.DiagramLink currentDiagramLink = (EAAPI.DiagramLink)apiDiagram.DiagramLinks.GetAt(index);

                                if (currentDiagramLink.ConnectorID == ConnectorID)
                                {
                                    _apiDiagramLink = currentDiagramLink;
                                    break;
                                }
                            }
                        }
                    }
                }

                return _apiDiagramLink;
            }
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

        private int _connectorID;

        public int ConnectorID
        {
            get
            {
                return _connectorID;
            }

            set
            {
                _connectorID = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.ConnectorID = value;
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

            set
            {
                _diagramID = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.DiagramID = value;
                }
            }
        }

        private string _geometry = "";

        public string Geometry
        {
            get
            {
                return _geometry;
            }

            set
            {
                _geometry = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.Geometry = value;
                }
            }
        }

        private bool _hiddenLabels;

        public bool HiddenLabels
        {
            get
            {
                return _hiddenLabels;
            }

            set
            {
                _hiddenLabels = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.HiddenLabels = value;
                }
            }
        }

        private int _instanceID;

        public int InstanceID
        {
            get
            {
                return _instanceID;
            }

            set
            {
                _instanceID = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.InstanceID = value;
                }
            }
        }

        private bool _isHidden;

        public bool IsHidden
        {
            get
            {
                return _isHidden;
            }

            set
            {
                _isHidden = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.IsHidden = value;
                }
            }
        }

        private int _lineColor;

        public int LineColor
        {
            get
            {
                return _lineColor;
            }

            set
            {
                _lineColor = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.LineColor = value;
                }
            }
        }

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

            set
            {
                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.LineStyle = (EAAPI.LinkLineStyle)value;
                }
            }
        }

        private int _lineWidth;

        public int LineWidth
        {
            get
            {
                return _lineWidth;
            }

            set
            {
                _lineWidth = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.LineWidth = value;
                }
            }
        }

        public ObjectType ObjectType { get; } = ObjectType.otDiagramLink;

        private string _path = "";

        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.Path = value;
                }
            }
        }

        public string SourceInstanceUID => throw new NotImplementedException();

        private string _style = "";

        public string Style
        {
            get
            {
                return _style;
            }

            set
            {
                _style = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.Style = value;
                }
            }
        }

        private int _suppressSegment;

        public int SuppressSegment
        {
            get
            {
                return _suppressSegment;
            }

            set
            {
                _suppressSegment = value;

                if (ApiDiagramLink != null)
                {
                    ApiDiagramLink.SuppressSegment = value;
                }
            }
        }

        public string TargetInstanceUID => throw new NotImplementedException();

        public string GetLastError()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            bool result = true;

            if (ApiDiagramLink != null)
            {
                result = ApiDiagramLink.Update();
            }

            return result;
        }
    }
}
