using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class PackageChildCollection : GenericCollection<Package>
    {
        private readonly PackageDataModel _owner;

        private readonly AbstractDataCache _abstractDataCache;

        public PackageChildCollection(PackageDataModel owner, AbstractDataCache abstractDataCache)
        {
            _owner = owner;
            _abstractDataCache = abstractDataCache;
        }

        public override object AddNew(string Name, string Type)
        {
            PackageDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Package? apiParentPackage = null;

            if (apiRepository != null)
            {
                apiParentPackage = apiRepository.GetPackageByID(_owner.PackageID);
            }

            if (apiParentPackage != null)
            {
                EAAPI.Package apiChildPackage = (EAAPI.Package)apiParentPackage.Packages.AddNew(Name, Type);

                apiChildPackage.Update();

                apiParentPackage.Packages.Refresh();

                result = new PackageDataModel(apiChildPackage, _abstractDataCache, _owner.Repository!);
            }
            else
            {
                result = new PackageDataModel();
                result.Name = Name;
                result.ParentID = _owner.PackageID;
                result.Repository = _owner.Repository!;
                result._abstractDataCache = _abstractDataCache;
            }

            _abstractDataCache._packageCache.Add(result);

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            Package toDelete = this[index];

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null && toDelete.PackageID != 0)
            {
                EAAPI.Package apiParentPackage = apiRepository.GetPackageByID(_owner.PackageID);

                if (apiParentPackage != null)
                {
                    for (short counter = 0; counter < apiParentPackage.Packages.Count; counter++)
                    {
                        EAAPI.Package currentPackage = (EAAPI.Package)apiParentPackage.Packages.GetAt(counter);

                        if (currentPackage.PackageID == toDelete.PackageID)
                        {
                            apiParentPackage.Packages.Delete(counter);
                            apiParentPackage.Packages.Refresh();
                            break;
                        }
                    }
                }
            }

            _abstractDataCache._packageCache.Remove(toDelete);

            RemoveAt(index);
        }

        public override void DeleteAt(short index, bool Refresh)
        {
            Delete(index);
        }
    }
}
