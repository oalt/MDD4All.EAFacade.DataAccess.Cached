using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ConnectorDataModel : Connector
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ConnectorDataModel()
        {

        }

        public ConnectorDataModel(XElement tObjectQueryRow)
        {
            try
            {
                ConnectorID = int.Parse(tObjectQueryRow.Element("Connector_ID").Value);
                Name = tObjectQueryRow.Element("Name").Value;
                Direction = tObjectQueryRow.Element("Direction").Value;
                Notes = tObjectQueryRow.Element("Notes").Value;
                Type = tObjectQueryRow.Element("Connector_Type").Value;
                Subtype = tObjectQueryRow.Element("SubType").Value;
                ClientID = int.Parse(tObjectQueryRow.Element("Start_Object_ID").Value);
                SupplierID = int.Parse(tObjectQueryRow.Element("End_Object_ID").Value);

                Stereotype = tObjectQueryRow.Element("Stereotype").Value;

                ConnectorGUID = tObjectQueryRow.Element("ea_guid").Value;

                

            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public ConnectorDataModel(EAAPI.Connector apiConnector)
        {
            ConnectorID = apiConnector.ConnectorID;
            Name = apiConnector.Name;
            Direction = apiConnector.Direction;
            Notes = apiConnector.Notes;
            Type = apiConnector.Type;
            Subtype = apiConnector.Subtype;
            ClientID = apiConnector.ClientID;
            SupplierID = apiConnector.SupplierID;

            Stereotype = apiConnector.Stereotype;

            ConnectorGUID = apiConnector.ConnectorGUID;
        }

        public string Alias { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Element AssociationClass => throw new NotImplementedException();

        public ConnectorEnd ClientEnd => throw new NotImplementedException();

        public int ClientID { get; set; }

        public int Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ConnectorGUID { get; private set; }

        public int ConnectorID { get; private set; }

        public Collection Constraints => throw new NotImplementedException();

        public Collection ConveyedItems => throw new NotImplementedException();

        public Collection CustomProperties => throw new NotImplementedException();

        public int DiagramID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Direction { get; set; }
        
        public int EndPointX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int EndPointY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string EventFlags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ForeignKeyInformation => throw new NotImplementedException();

        public string FQStereotype => throw new NotImplementedException();

        public bool IsLeaf { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool IsRoot { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool IsSpec { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string MessageArguments => throw new NotImplementedException();

        public string MetaType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string MiscData => throw new NotImplementedException();

        public string Name { get; set; } = "";

        public string Notes { get; set; } = "";

        public ObjectType ObjectType { get; } = ObjectType.otConnector;

        public Properties Properties => throw new NotImplementedException();

        public string ReturnValueAlias => throw new NotImplementedException();

        public int RouteStyle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int SequenceNo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int StartPointX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int StartPointY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string StateFlags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Stereotype { get; set; }
        
        public string StereotypeEx { get; set; }
        
        public string StyleEx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Subtype { get; set; }

        public ConnectorEnd SupplierEnd => throw new NotImplementedException();

        public int SupplierID { get; set; }

        public GenericCollection<ConnectorTag> TaggedValues => throw new NotImplementedException();

        public Collection TemplateBindings => throw new NotImplementedException();

        public string TransitionAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string TransitionEvent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string TransitionGuard { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Type { get; set; }
        
        public string VirtualInheritance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int Width { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetLastError()
        {
            throw new NotImplementedException();
        }

        public bool IsConnectorValid()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
