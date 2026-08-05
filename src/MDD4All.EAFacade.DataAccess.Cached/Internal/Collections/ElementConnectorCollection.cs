using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class ElementConnectorCollection : GenericCollection<Connector>
    {
        private readonly ElementDataModel _owner;

        private readonly AbstractDataCache _abstractDataCache;

        public ElementConnectorCollection(ElementDataModel owner, AbstractDataCache abstractDataCache)
        {
            _owner = owner;
            _abstractDataCache = abstractDataCache;
        }

        public override object AddNew(string Name, string Type)
        {
            ConnectorDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Element? apiElement = null;

            if (apiRepository != null)
            {
                apiElement = apiRepository.GetElementByID(_owner.ElementID);
            }

            if (apiElement != null)
            {
                EAAPI.Connector apiConnector = (EAAPI.Connector)apiElement.Connectors.AddNew(Name, Type);

                // Do not call apiConnector.Update() here: a freshly AddNew'd connector
                // only has its client (source) end set, and EA rejects Update() while
                // the supplier (target) end is still unset ("Cannot update connector as
                // either the Start or the End object is NULL"). The caller is expected
                // to set SupplierID and call Update() once the target is known
                // (see ElementManipulationExtensions.AddConnector).

                result = new ConnectorDataModel(apiConnector);
                result.Repository = _owner.Repository!;
            }
            else
            {
                result = new ConnectorDataModel();
                result.Name = Name;
                result.Type = Type;
                result.ClientID = _owner.ElementID;
                result.Repository = _owner.Repository!;
            }

            _abstractDataCache._connectorCache.Add(result);

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            Connector toDelete = this[index];

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null && toDelete.ConnectorID != 0)
            {
                EAAPI.Element apiElement = apiRepository.GetElementByID(_owner.ElementID);

                if (apiElement != null)
                {
                    for (short counter = 0; counter < apiElement.Connectors.Count; counter++)
                    {
                        EAAPI.Connector currentConnector = (EAAPI.Connector)apiElement.Connectors.GetAt(counter);

                        if (currentConnector.ConnectorID == toDelete.ConnectorID)
                        {
                            apiElement.Connectors.Delete(counter);
                            apiElement.Connectors.Refresh();
                            break;
                        }
                    }
                }
            }

            _abstractDataCache._connectorCache.Remove(toDelete);

            RemoveAt(index);
        }

        public override void DeleteAt(short index, bool Refresh)
        {
            Delete(index);
        }
    }
}
