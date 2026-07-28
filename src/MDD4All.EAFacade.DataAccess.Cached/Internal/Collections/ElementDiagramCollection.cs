using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class ElementDiagramCollection : GenericCollection<Diagram>
    {
        private readonly ElementDataModel _owner;

        private readonly AbstractDataCache _abstractDataCache;

        public ElementDiagramCollection(ElementDataModel owner, AbstractDataCache abstractDataCache)
        {
            _owner = owner;
            _abstractDataCache = abstractDataCache;
        }

        public override object AddNew(string Name, string Type)
        {
            DiagramDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Element? apiElement = null;

            if (apiRepository != null)
            {
                apiElement = apiRepository.GetElementByID(_owner.ElementID);
            }

            if (apiElement != null)
            {
                EAAPI.Diagram apiDiagram = (EAAPI.Diagram)apiElement.Diagrams.AddNew(Name, Type);

                apiDiagram.Update();

                apiElement.Diagrams.Refresh();

                result = new DiagramDataModel(apiDiagram);
                result.Repository = _owner.Repository!;
            }
            else
            {
                result = new DiagramDataModel();
                result.Name = Name;
                result.Type = Type;
                result.ParentID = _owner.ElementID;
                result.Repository = _owner.Repository!;
            }

            _abstractDataCache._diagramCache.Add(result);

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            Diagram toDelete = this[index];

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null && toDelete.DiagramID != 0)
            {
                EAAPI.Element apiElement = apiRepository.GetElementByID(_owner.ElementID);

                if (apiElement != null)
                {
                    for (short counter = 0; counter < apiElement.Diagrams.Count; counter++)
                    {
                        EAAPI.Diagram currentDiagram = (EAAPI.Diagram)apiElement.Diagrams.GetAt(counter);

                        if (currentDiagram.DiagramID == toDelete.DiagramID)
                        {
                            apiElement.Diagrams.Delete(counter);
                            apiElement.Diagrams.Refresh();
                            break;
                        }
                    }
                }
            }

            _abstractDataCache._diagramCache.Remove(toDelete);

            RemoveAt(index);
        }

        public override void DeleteAt(short index, bool Refresh)
        {
            Delete(index);
        }
    }
}
