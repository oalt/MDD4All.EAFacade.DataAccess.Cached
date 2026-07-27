using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class DiagramLinkCollection : GenericCollection<DiagramLink>
    {
        private readonly DiagramDataModel _owner;

        public DiagramLinkCollection(DiagramDataModel owner)
        {
            _owner = owner;
        }

        public override object AddNew(string Name, string Type)
        {
            DiagramLinkDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Diagram? apiDiagram = null;

            if (apiRepository != null)
            {
                apiDiagram = apiRepository.GetDiagramByID(_owner.DiagramID);
            }

            if (apiDiagram != null)
            {
                EAAPI.DiagramLink apiDiagramLink = (EAAPI.DiagramLink)apiDiagram.DiagramLinks.AddNew(Name, Type);

                result = new DiagramLinkDataModel(apiDiagramLink);
                result.Repository = _owner.Repository;
            }
            else
            {
                result = new DiagramLinkDataModel();
                result.DiagramID = _owner.DiagramID;
                result.Repository = _owner.Repository;
            }

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            DiagramLink toDelete = this[index];

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null && toDelete.ConnectorID != 0)
            {
                EAAPI.Diagram apiDiagram = apiRepository.GetDiagramByID(_owner.DiagramID);

                if (apiDiagram != null)
                {
                    for (short counter = 0; counter < apiDiagram.DiagramLinks.Count; counter++)
                    {
                        EAAPI.DiagramLink currentDiagramLink = (EAAPI.DiagramLink)apiDiagram.DiagramLinks.GetAt(counter);

                        if (currentDiagramLink.ConnectorID == toDelete.ConnectorID)
                        {
                            apiDiagram.DiagramLinks.Delete(counter);
                            apiDiagram.DiagramLinks.Refresh();
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
