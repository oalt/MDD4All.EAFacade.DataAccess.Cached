using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class PackageElementCollection : GenericCollection<Element>
    {
        private readonly PackageDataModel _owner;

        private readonly AbstractDataCache _abstractDataCache;

        public PackageElementCollection(PackageDataModel owner, AbstractDataCache abstractDataCache)
        {
            _owner = owner;
            _abstractDataCache = abstractDataCache;
        }

        public override object AddNew(string Name, string Type)
        {
            ElementDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Package? apiPackage = null;

            if (apiRepository != null)
            {
                apiPackage = apiRepository.GetPackageByID(_owner.PackageID);
            }

            if (apiPackage != null)
            {
                EAAPI.Element apiElement = (EAAPI.Element)apiPackage.Elements.AddNew(Name, Type);

                apiElement.Update();

                apiPackage.Elements.Refresh();

                result = new ElementDataModel(apiElement);
                result.Repository = _owner.Repository!;
                result.AbstractDataCache = _abstractDataCache;
            }
            else
            {
                result = new ElementDataModel();
                result.Name = Name;
                result.Type = Type;
                result.PackageID = _owner.PackageID;
                result.Repository = _owner.Repository!;
                result.AbstractDataCache = _abstractDataCache;
            }

            _abstractDataCache._elementCache.Add(result);

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            Element toDelete = this[index];

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null && toDelete.ElementID != 0)
            {
                EAAPI.Package apiPackage = apiRepository.GetPackageByID(_owner.PackageID);

                if (apiPackage != null)
                {
                    for (short counter = 0; counter < apiPackage.Elements.Count; counter++)
                    {
                        EAAPI.Element currentElement = (EAAPI.Element)apiPackage.Elements.GetAt(counter);

                        if (currentElement.ElementID == toDelete.ElementID)
                        {
                            apiPackage.Elements.Delete(counter);
                            apiPackage.Elements.Refresh();
                            break;
                        }
                    }
                }
            }

            _abstractDataCache._elementCache.Remove(toDelete);

            RemoveAt(index);
        }

        public override void DeleteAt(short index, bool Refresh)
        {
            Delete(index);
        }
    }
}
