using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class DiagramObjectCollection : GenericCollection<DiagramObject>
    {
        private readonly DiagramDataModel _owner;

        public DiagramObjectCollection(DiagramDataModel owner)
        {
            _owner = owner;
        }

        public override object AddNew(string Name, string Type)
        {
            DiagramObjectDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Diagram? apiDiagram = null;

            if (apiRepository != null)
            {
                apiDiagram = apiRepository.GetDiagramByID(_owner.DiagramID);
            }

            if (apiDiagram != null)
            {
                EAAPI.DiagramObject apiDiagramObject = (EAAPI.DiagramObject)apiDiagram.DiagramObjects.AddNew(Name, Type);

                result = new DiagramObjectDataModel(apiDiagramObject);
                result.Repository = _owner.Repository;
            }
            else
            {
                result = new DiagramObjectDataModel();
                result.DiagramID = _owner.DiagramID;
                result.Repository = _owner.Repository;
            }

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            DiagramObject toDelete = this[index];

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null && toDelete.ElementID != 0)
            {
                EAAPI.Diagram apiDiagram = apiRepository.GetDiagramByID(_owner.DiagramID);

                if (apiDiagram != null)
                {
                    for (short counter = 0; counter < apiDiagram.DiagramObjects.Count; counter++)
                    {
                        EAAPI.DiagramObject currentDiagramObject = (EAAPI.DiagramObject)apiDiagram.DiagramObjects.GetAt(counter);

                        if (currentDiagramObject.ElementID == toDelete.ElementID)
                        {
                            apiDiagram.DiagramObjects.Delete(counter);
                            apiDiagram.DiagramObjects.Refresh();
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
