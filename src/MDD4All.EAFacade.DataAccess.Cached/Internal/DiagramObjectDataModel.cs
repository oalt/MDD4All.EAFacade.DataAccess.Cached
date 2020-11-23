using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class DiagramObjectDataModel : DiagramObject
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DiagramObjectDataModel()
        {

        }

        public DiagramObjectDataModel(XElement tObjectQueryRow)
        {
            try
            {
                DiagramID = int.Parse(tObjectQueryRow.Element("Diagram_ID").Value);
                ElementID = int.Parse(tObjectQueryRow.Element("Object_ID").Value);
                top = int.Parse(tObjectQueryRow.Element("RectTop").Value);
                left = int.Parse(tObjectQueryRow.Element("RectLeft").Value);
                right = int.Parse(tObjectQueryRow.Element("RectRight").Value);
                bottom = int.Parse(tObjectQueryRow.Element("RectBottom").Value);
                Sequence = int.Parse(tObjectQueryRow.Element("Sequence").Value);
                Style = tObjectQueryRow.Element("ObjectStyle").Value;
                InstanceID = int.Parse(tObjectQueryRow.Element("Instance_ID").Value);


            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public DiagramObjectDataModel(EAAPI.DiagramObject apiDiagramObject)
        {
            DiagramID = apiDiagramObject.DiagramID;
            ElementID = apiDiagramObject.ElementID;
            top = apiDiagramObject.top;
            left = apiDiagramObject.left;
            right = apiDiagramObject.right;
            bottom = apiDiagramObject.bottom;
            Sequence = apiDiagramObject.Sequence;
            Style = apiDiagramObject.Style;
            InstanceID = apiDiagramObject.InstanceID;
            
        }

        public int BackgroundColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int BorderColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int BorderLineWidth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int bottom { get; set; }
        
        public int DiagramID { get; set; }
        
        public FeatureDisplayMode ElementDisplayMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int ElementID { get; set; }
        
        public string FeatureStereotypesToHide { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool FontBold { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int FontColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool FontItalic { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public object fontName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int fontSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool FontUnderline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object InstanceGUID { get; private set; } ="";

        public int InstanceID { get; set; }
        
        public bool IsSelectable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int left { get; set; }

        public ObjectType ObjectType { get; } = ObjectType.otDiagramObject;

        public int right { get; set; }

        public int Sequence { get; set; }
        
        public bool ShowComposedDiagram { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowConstraints { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowFormattedNotes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowFullyQualifiedTags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowInheritedAttributes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowInheritedConstraints { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowInheritedOperations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowInheritedResponsibilities { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowInheritedTags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowNotes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowPackageAttributes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowPackageOperations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowPortType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowPrivateAttributes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowPrivateOperations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowProtectedAttributes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowProtectedOperations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowPublicAttributes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowPublicOperations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowResponsibilities { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowRunstates { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowStructuredCompartments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool ShowTags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public object Style { get; set; }
        
        public TextAlignment TextAlign { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int top { get; set; }

        public string GetLastError()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
