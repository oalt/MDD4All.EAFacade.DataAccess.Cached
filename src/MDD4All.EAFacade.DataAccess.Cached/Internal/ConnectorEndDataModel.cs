using MDD4All.EAFacade.DataModels.Contracts;
using NLog;
using System;
using System.Xml.Linq;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    public class ConnectorEndDataModel: ConnectorEnd
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ConnectorEndDataModel() 
        { 
        }

        public ConnectorEndDataModel(XElement tConnectorQueryRow, string endType)
        {
            try
            {
                if(endType == "Source")
                {
                    Role = tConnectorQueryRow.Element("SourceRole").Value;
                    Aggregation = int.Parse(tConnectorQueryRow.Element("SourceIsAggregate").Value);
                    Cardinality = tConnectorQueryRow.Element("SourceCard").Value;

                    string style = tConnectorQueryRow.Element("SourceStyle").Value;

                    ConnectorStyleDataModel styleData = new ConnectorStyleDataModel(style);

                    if(styleData.StyleData.ContainsKey("Derived"))
                    {
                        string derivedString = styleData.StyleData["Derived"];
                        if (derivedString != "0")
                        {
                            Derived = true;
                        }
                    }
                    if (styleData.StyleData.ContainsKey("AllowDuplicates"))
                    {
                        string derivedString = styleData.StyleData["AllowDuplicates"];
                        if (derivedString != "0")
                        {
                            AllowDuplicates = true;
                        }
                    }


                }
                else if(endType == "Destination")
                {
                    Role = tConnectorQueryRow.Element("DestRole").Value;
                    Aggregation = int.Parse(tConnectorQueryRow.Element("DestIsAggregate").Value);
                    Cardinality = tConnectorQueryRow.Element("DestCard").Value;

                    string style = tConnectorQueryRow.Element("DestStyle").Value;

                    ConnectorStyleDataModel styleData = new ConnectorStyleDataModel(style);

                    if (styleData.StyleData.ContainsKey("Derived"))
                    {
                        string derivedString = styleData.StyleData["Derived"];
                        if (derivedString != "0")
                        {
                            Derived = true;
                        }
                    }
                    if (styleData.StyleData.ContainsKey("AllowDuplicates"))
                    {
                        string derivedString = styleData.StyleData["AllowDuplicates"];
                        if (derivedString != "0")
                        {
                            AllowDuplicates = true;
                        }
                    }
                }
                

            }
            catch (Exception exception)
            {
                logger.Debug(exception);
            }
        }

        public ConnectorEndDataModel(EA.ConnectorEnd connectorEnd)
        {
            Role = connectorEnd.Role;
            Aggregation = connectorEnd.Aggregation;
            Cardinality = connectorEnd.Cardinality;
            Derived = connectorEnd.Derived;
            AllowDuplicates = connectorEnd.AllowDuplicates;
        }

        public string End => throw new NotImplementedException();

        public string Cardinality { get; set; } = "";
        
        public string Visibility { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Role { get; set; } = "";

        public string RoleType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string RoleNote { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Containment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int Aggregation { get; set; }
        
        public int Ordering { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Qualifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Constraint { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool IsNavigable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string IsChangeable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Collection TaggedValues => throw new NotImplementedException();

        public string Stereotype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ObjectType ObjectType => throw new NotImplementedException();

        public string StereotypeEx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Navigable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool OwnedByClassifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool Derived { get; set; }
        
        public bool DerivedUnion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool AllowDuplicates { get; set; }
        
        public string Alias { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public string GetLastError()
        {
            throw new NotImplementedException();
        }
    }
}
