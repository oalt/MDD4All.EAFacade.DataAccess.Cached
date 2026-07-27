using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class ConnectorTagCollection : GenericCollection<ConnectorTag>
    {
        private readonly Connector _owner;

        public ConnectorTagCollection(Connector owner)
        {
            _owner = owner;
        }

        public override object AddNew(string Name, string Type)
        {
            ConnectorTagDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Connector? apiConnector = null;

            if (apiRepository != null)
            {
                apiConnector = apiRepository.GetConnectorByID(_owner.ConnectorID);
            }

            if (apiConnector != null)
            {
                EAAPI.ConnectorTag apiConnectorTag = (EAAPI.ConnectorTag)apiConnector.TaggedValues.AddNew(Name, Type);

                apiConnectorTag.Update();

                apiConnector.TaggedValues.Refresh();

                result = new ConnectorTagDataModel(apiConnectorTag);
                result.Repository = _owner.Repository;
            }
            else
            {
                result = new ConnectorTagDataModel();
                result.Name = Name;
                result.ConnectorID = _owner.ConnectorID;
                result.Repository = _owner.Repository;
            }

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            ConnectorTag toDelete = this[index];

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null && !string.IsNullOrEmpty(toDelete.TagGUID))
            {
                EAAPI.Connector apiConnector = apiRepository.GetConnectorByID(_owner.ConnectorID);

                if (apiConnector != null)
                {
                    for (short counter = 0; counter < apiConnector.TaggedValues.Count; counter++)
                    {
                        EAAPI.ConnectorTag currentConnectorTag = (EAAPI.ConnectorTag)apiConnector.TaggedValues.GetAt(counter);

                        if (currentConnectorTag.TagGUID == toDelete.TagGUID)
                        {
                            apiConnector.TaggedValues.Delete(counter);
                            apiConnector.TaggedValues.Refresh();
                            break;
                        }
                    }
                }
            }

            RemoveAt(index);
        }

        public override void DeleteAt(short index, bool Refresh)
        {
            Delete(index);
        }
    }
}
