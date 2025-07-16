using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ConnectorDataModel : RepositoryElementDataModel, Connector
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ConnectorDataModel()
        {
        }

        public ConnectorDataModel(XElement tConnectorQueryRow, Repository repository)
        {
            Repository = repository;

            try
            {
                ConnectorID = int.Parse(tConnectorQueryRow.Element("Connector_ID").Value);
                Name = tConnectorQueryRow.Element("Name").Value;
                Direction = tConnectorQueryRow.Element("Direction").Value;
                Notes = tConnectorQueryRow.Element("Notes").Value;
                Type = tConnectorQueryRow.Element("Connector_Type").Value;
                Subtype = tConnectorQueryRow.Element("SubType").Value;
                ClientID = int.Parse(tConnectorQueryRow.Element("Start_Object_ID").Value);
                SupplierID = int.Parse(tConnectorQueryRow.Element("End_Object_ID").Value);

                Stereotype = tConnectorQueryRow.Element("Stereotype").Value;

                ConnectorGUID = tConnectorQueryRow.Element("ea_guid").Value;

                ClientEnd = new ConnectorEndDataModel(tConnectorQueryRow, "Source");
                SupplierEnd = new ConnectorEndDataModel(tConnectorQueryRow, "Destination");

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

            ClientEnd = new ConnectorEndDataModel(apiConnector.ClientEnd);
            SupplierEnd = new ConnectorEndDataModel(apiConnector.SupplierEnd);
        }

        public string Alias { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Element AssociationClass => throw new NotImplementedException();

        public ConnectorEnd ClientEnd { get; set; } = new ConnectorEndDataModel();

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

        public ConnectorEnd SupplierEnd { get; set; } = new ConnectorEndDataModel();

        public int SupplierID { get; set; }

        public GenericCollection<ConnectorTag> TaggedValues { get; set; } = new GenericCollection<ConnectorTag>();

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
