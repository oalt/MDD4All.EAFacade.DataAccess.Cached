using MDD4All.EAFacade.DataModels.Contracts;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class DiagramObjectDataModel : RepositoryElementDataModel, DiagramObject
    {
        public DiagramObjectDataModel()
        {

        }

        public DiagramObjectDataModel(XElement tObjectQueryRow, Repository repository)
        {
            Repository = repository;

            _diagramID = int.Parse(tObjectQueryRow.Element("Diagram_ID").Value);
            _elementID = int.Parse(tObjectQueryRow.Element("Object_ID").Value);
            _top = int.Parse(tObjectQueryRow.Element("RectTop").Value);
            _left = int.Parse(tObjectQueryRow.Element("RectLeft").Value);
            _right = int.Parse(tObjectQueryRow.Element("RectRight").Value);
            _bottom = int.Parse(tObjectQueryRow.Element("RectBottom").Value);
            _sequence = int.Parse(tObjectQueryRow.Element("Sequence").Value);
            _style = tObjectQueryRow.Element("ObjectStyle").Value;
            _instanceID = int.Parse(tObjectQueryRow.Element("Instance_ID").Value);
        }

        public DiagramObjectDataModel(EAAPI.DiagramObject apiDiagramObject)
        {
            _apiDiagramObject = apiDiagramObject;

            _diagramID = apiDiagramObject.DiagramID;
            _elementID = apiDiagramObject.ElementID;
            _top = apiDiagramObject.top;
            _left = apiDiagramObject.left;
            _right = apiDiagramObject.right;
            _bottom = apiDiagramObject.bottom;
            _sequence = apiDiagramObject.Sequence;
            _style = apiDiagramObject.Style;
            _instanceID = apiDiagramObject.InstanceID;
        }

        private EAAPI.DiagramObject? _apiDiagramObject;

        private EAAPI.DiagramObject? ApiDiagramObject
        {
            get
            {
                if (_apiDiagramObject == null && ElementID != 0)
                {
                    EAAPI.Repository? apiRepository = Repository?.ApiRepository;

                    if (apiRepository != null)
                    {
                        EAAPI.Diagram apiDiagram = apiRepository.GetDiagramByID(DiagramID);

                        if (apiDiagram != null)
                        {
                            string duid = InstanceGUID as string ?? "";

                            _apiDiagramObject = apiDiagram.GetDiagramObjectByID(ElementID, duid);
                        }
                    }
                }

                return _apiDiagramObject;
            }
        }

        private int _backgroundColor;

        public int BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }

            set
            {
                _backgroundColor = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.BackgroundColor = value;
                }
            }
        }

        private int _borderColor;

        public int BorderColor
        {
            get
            {
                return _borderColor;
            }

            set
            {
                _borderColor = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.BorderColor = value;
                }
            }
        }

        private int _borderLineWidth;

        public int BorderLineWidth
        {
            get
            {
                return _borderLineWidth;
            }

            set
            {
                _borderLineWidth = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.BorderLineWidth = value;
                }
            }
        }

        private int _bottom;

        public int bottom
        {
            get
            {
                return _bottom;
            }

            set
            {
                _bottom = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.bottom = value;
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

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.DiagramID = value;
                }
            }
        }

        private FeatureDisplayMode _elementDisplayMode;

        public FeatureDisplayMode ElementDisplayMode
        {
            get
            {
                return _elementDisplayMode;
            }

            set
            {
                _elementDisplayMode = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ElementDisplayMode = (EAAPI.FeatureDisplayMode)value;
                }
            }
        }

        private int _elementID;

        public int ElementID
        {
            get
            {
                return _elementID;
            }

            set
            {
                _elementID = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ElementID = value;
                }
            }
        }

        private string _featureStereotypesToHide = "";

        public string FeatureStereotypesToHide
        {
            get
            {
                return _featureStereotypesToHide;
            }

            set
            {
                _featureStereotypesToHide = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.FeatureStereotypesToHide = value;
                }
            }
        }

        private bool _fontBold;

        public bool FontBold
        {
            get
            {
                return _fontBold;
            }

            set
            {
                _fontBold = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.FontBold = value;
                }
            }
        }

        private int _fontColor;

        public int FontColor
        {
            get
            {
                return _fontColor;
            }

            set
            {
                _fontColor = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.FontColor = value;
                }
            }
        }

        private bool _fontItalic;

        public bool FontItalic
        {
            get
            {
                return _fontItalic;
            }

            set
            {
                _fontItalic = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.FontItalic = value;
                }
            }
        }

        private object _fontName = null!;

        public object fontName
        {
            get
            {
                return _fontName;
            }

            set
            {
                _fontName = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.fontName = value;
                }
            }
        }

        private int _fontSize;

        public int fontSize
        {
            get
            {
                return _fontSize;
            }

            set
            {
                _fontSize = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.fontSize = value;
                }
            }
        }

        private bool _fontUnderline;

        public bool FontUnderline
        {
            get
            {
                return _fontUnderline;
            }

            set
            {
                _fontUnderline = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.FontUnderline = value;
                }
            }
        }

        private object _instanceGUID = "";

        public object InstanceGUID
        {
            get
            {
                return _instanceGUID;
            }

            private set
            {
                _instanceGUID = value;
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

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.InstanceID = value;
                }
            }
        }

        private bool _isSelectable;

        public bool IsSelectable
        {
            get
            {
                return _isSelectable;
            }

            set
            {
                _isSelectable = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.IsSelectable = value;
                }
            }
        }

        private int _left;

        public int left
        {
            get
            {
                return _left;
            }

            set
            {
                _left = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.left = value;
                }
            }
        }

        public ObjectType ObjectType { get; } = ObjectType.otDiagramObject;

        private int _right;

        public int right
        {
            get
            {
                return _right;
            }

            set
            {
                _right = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.right = value;
                }
            }
        }

        private int _sequence;

        public int Sequence
        {
            get
            {
                return _sequence;
            }

            set
            {
                _sequence = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.Sequence = value;
                }
            }
        }

        private bool _showComposedDiagram;

        public bool ShowComposedDiagram
        {
            get
            {
                return _showComposedDiagram;
            }

            set
            {
                _showComposedDiagram = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowComposedDiagram = value;
                }
            }
        }

        private bool _showConstraints;

        public bool ShowConstraints
        {
            get
            {
                return _showConstraints;
            }

            set
            {
                _showConstraints = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowConstraints = value;
                }
            }
        }

        private bool _showFormattedNotes;

        public bool ShowFormattedNotes
        {
            get
            {
                return _showFormattedNotes;
            }

            set
            {
                _showFormattedNotes = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowFormattedNotes = value;
                }
            }
        }

        private bool _showFullyQualifiedTags;

        public bool ShowFullyQualifiedTags
        {
            get
            {
                return _showFullyQualifiedTags;
            }

            set
            {
                _showFullyQualifiedTags = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowFullyQualifiedTags = value;
                }
            }
        }

        private bool _showInheritedAttributes;

        public bool ShowInheritedAttributes
        {
            get
            {
                return _showInheritedAttributes;
            }

            set
            {
                _showInheritedAttributes = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowInheritedAttributes = value;
                }
            }
        }

        private bool _showInheritedConstraints;

        public bool ShowInheritedConstraints
        {
            get
            {
                return _showInheritedConstraints;
            }

            set
            {
                _showInheritedConstraints = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowInheritedConstraints = value;
                }
            }
        }

        private bool _showInheritedOperations;

        public bool ShowInheritedOperations
        {
            get
            {
                return _showInheritedOperations;
            }

            set
            {
                _showInheritedOperations = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowInheritedOperations = value;
                }
            }
        }

        private bool _showInheritedResponsibilities;

        public bool ShowInheritedResponsibilities
        {
            get
            {
                return _showInheritedResponsibilities;
            }

            set
            {
                _showInheritedResponsibilities = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowInheritedResponsibilities = value;
                }
            }
        }

        private bool _showInheritedTags;

        public bool ShowInheritedTags
        {
            get
            {
                return _showInheritedTags;
            }

            set
            {
                _showInheritedTags = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowInheritedTags = value;
                }
            }
        }

        private bool _showNotes;

        public bool ShowNotes
        {
            get
            {
                return _showNotes;
            }

            set
            {
                _showNotes = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowNotes = value;
                }
            }
        }

        private bool _showPackageAttributes;

        public bool ShowPackageAttributes
        {
            get
            {
                return _showPackageAttributes;
            }

            set
            {
                _showPackageAttributes = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowPackageAttributes = value;
                }
            }
        }

        private bool _showPackageOperations;

        public bool ShowPackageOperations
        {
            get
            {
                return _showPackageOperations;
            }

            set
            {
                _showPackageOperations = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowPackageOperations = value;
                }
            }
        }

        private bool _showPortType;

        public bool ShowPortType
        {
            get
            {
                return _showPortType;
            }

            set
            {
                _showPortType = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowPortType = value;
                }
            }
        }

        private bool _showPrivateAttributes;

        public bool ShowPrivateAttributes
        {
            get
            {
                return _showPrivateAttributes;
            }

            set
            {
                _showPrivateAttributes = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowPrivateAttributes = value;
                }
            }
        }

        private bool _showPrivateOperations;

        public bool ShowPrivateOperations
        {
            get
            {
                return _showPrivateOperations;
            }

            set
            {
                _showPrivateOperations = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowPrivateOperations = value;
                }
            }
        }

        private bool _showProtectedAttributes;

        public bool ShowProtectedAttributes
        {
            get
            {
                return _showProtectedAttributes;
            }

            set
            {
                _showProtectedAttributes = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowProtectedAttributes = value;
                }
            }
        }

        private bool _showProtectedOperations;

        public bool ShowProtectedOperations
        {
            get
            {
                return _showProtectedOperations;
            }

            set
            {
                _showProtectedOperations = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowProtectedOperations = value;
                }
            }
        }

        private bool _showPublicAttributes;

        public bool ShowPublicAttributes
        {
            get
            {
                return _showPublicAttributes;
            }

            set
            {
                _showPublicAttributes = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowPublicAttributes = value;
                }
            }
        }

        private bool _showPublicOperations;

        public bool ShowPublicOperations
        {
            get
            {
                return _showPublicOperations;
            }

            set
            {
                _showPublicOperations = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowPublicOperations = value;
                }
            }
        }

        private bool _showResponsibilities;

        public bool ShowResponsibilities
        {
            get
            {
                return _showResponsibilities;
            }

            set
            {
                _showResponsibilities = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowResponsibilities = value;
                }
            }
        }

        private bool _showRunstates;

        public bool ShowRunstates
        {
            get
            {
                return _showRunstates;
            }

            set
            {
                _showRunstates = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowRunstates = value;
                }
            }
        }

        private bool _showStructuredCompartments;

        public bool ShowStructuredCompartments
        {
            get
            {
                return _showStructuredCompartments;
            }

            set
            {
                _showStructuredCompartments = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowStructuredCompartments = value;
                }
            }
        }

        private bool _showTags;

        public bool ShowTags
        {
            get
            {
                return _showTags;
            }

            set
            {
                _showTags = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.ShowTags = value;
                }
            }
        }

        private object _style = null!;

        public object Style
        {
            get
            {
                return _style;
            }

            set
            {
                _style = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.Style = value;
                }
            }
        }

        private TextAlignment _textAlign;

        public TextAlignment TextAlign
        {
            get
            {
                return _textAlign;
            }

            set
            {
                _textAlign = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.TextAlign = (EAAPI.TextAlignment)value;
                }
            }
        }

        private int _top;

        public int top
        {
            get
            {
                return _top;
            }

            set
            {
                _top = value;

                if (ApiDiagramObject != null)
                {
                    ApiDiagramObject.top = value;
                }
            }
        }

        public string GetLastError()
        {
            string result = "";

            if (ApiDiagramObject != null)
            {
                result = ApiDiagramObject.GetLastError();
            }

            return result;
        }

        public void ResetFont()
        {
            throw new NotImplementedException();
        }

        public bool SetFontStyle(object fontName, int fontSize, bool bold, bool italic, bool underline)
        {
            throw new NotImplementedException();
        }

        public void SetStyleEx(string sParameter, string sValue)
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            bool result = true;

            if (ApiDiagramObject != null)
            {
                result = ApiDiagramObject.Update();
            }

            return result;
        }
    }
}
