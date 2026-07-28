using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class PackageDiagramCollection : GenericCollection<Diagram>
    {
        private readonly PackageDataModel _owner;

        private readonly AbstractDataCache _abstractDataCache;

        public PackageDiagramCollection(PackageDataModel owner, AbstractDataCache abstractDataCache)
        {
            _owner = owner;
            _abstractDataCache = abstractDataCache;
        }

        public override object AddNew(string Name, string Type)
        {
            DiagramDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Package? apiPackage = null;

            if (apiRepository != null)
            {
                apiPackage = apiRepository.GetPackageByID(_owner.PackageID);
            }

            if (apiPackage != null)
            {
                EAAPI.Diagram apiDiagram = (EAAPI.Diagram)apiPackage.Diagrams.AddNew(Name, Type);

                apiDiagram.Update();

                apiPackage.Diagrams.Refresh();

                result = new DiagramDataModel(apiDiagram);
                result.Repository = _owner.Repository!;
            }
            else
            {
                result = new DiagramDataModel();
                result.Name = Name;
                result.Type = Type;
                result.PackageID = _owner.PackageID;
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
                EAAPI.Package apiPackage = apiRepository.GetPackageByID(_owner.PackageID);

                if (apiPackage != null)
                {
                    for (short counter = 0; counter < apiPackage.Diagrams.Count; counter++)
                    {
                        EAAPI.Diagram currentDiagram = (EAAPI.Diagram)apiPackage.Diagrams.GetAt(counter);

                        if (currentDiagram.DiagramID == toDelete.DiagramID)
                        {
                            apiPackage.Diagrams.Delete(counter);
                            apiPackage.Diagrams.Refresh();
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
