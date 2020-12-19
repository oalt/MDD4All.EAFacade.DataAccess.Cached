using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using EAAPI = EA;
using EADM = MDD4All.EAFacade.DataAccess.Cached.Internal;

namespace MDD4All.EAFacade.DataAccess.Cached
{
    public class CachedDiagramDataProvider
    {
        private EAAPI.Repository _repository;

        public CachedDiagramDataProvider(EAAPI.Repository repository)
        {
            _repository = repository;
        }

        public List<int> GetShownElementIDs(int diagramID)
        {
            List<int> result = new List<int>();

            string diagramObjectsXml = _repository.SQLQuery("select * from t_diagramobjects where Diagram_ID = " + diagramID);

            XElement diagramObjectRootElement = XElement.Parse(diagramObjectsXml);

            XElement diagramObjectDatasetElement = diagramObjectRootElement.Element("Dataset_0");

            if (diagramObjectDatasetElement != null)
            {
                XElement diagramObjectDataElement = diagramObjectDatasetElement.Element("Data");

                IEnumerable<XElement> diagramObjectRows = diagramObjectDataElement.Elements("Row");

                foreach (XElement diagramObjectRow in diagramObjectRows)
                {
                    EADM.DiagramObjectDataModel diagramObject = new EADM.DiagramObjectDataModel(diagramObjectRow);

                    result.Add(diagramObject.ElementID);
                }
            }

            return result;
        }

        public List<int> GetAllDiagramIDsForElement(int elementID)
        {
            List<int> result = new List<int>();

            string diagramObjectsXml = _repository.SQLQuery("select * from t_diagramobjects where Object_ID = " + elementID);

            XElement diagramObjectRootElement = XElement.Parse(diagramObjectsXml);

            XElement diagramObjectDatasetElement = diagramObjectRootElement.Element("Dataset_0");

            if (diagramObjectDatasetElement != null)
            {
                XElement diagramObjectDataElement = diagramObjectDatasetElement.Element("Data");

                IEnumerable<XElement> diagramObjectRows = diagramObjectDataElement.Elements("Row");

                foreach (XElement diagramObjectRow in diagramObjectRows)
                {
                    EADM.DiagramObjectDataModel diagramObject = new EADM.DiagramObjectDataModel(diagramObjectRow);

                    result.Add(diagramObject.DiagramID);                    
                }
            }

            return result;
        }
    }
}
