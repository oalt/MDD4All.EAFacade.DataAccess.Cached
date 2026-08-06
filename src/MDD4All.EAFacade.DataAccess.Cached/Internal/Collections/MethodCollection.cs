using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class MethodCollection : GenericCollection<Method>
    {
        private readonly ElementDataModel _owner;

        private readonly AbstractDataCache _abstractDataCache;

        public MethodCollection(ElementDataModel owner, AbstractDataCache abstractDataCache)
        {
            _owner = owner;
            _abstractDataCache = abstractDataCache;
        }

        public override object AddNew(string Name, string Type)
        {
            MethodDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Element? apiElement = null;

            if (apiRepository != null)
            {
                apiElement = apiRepository.GetElementByID(_owner.ElementID);
            }

            if (apiElement != null)
            {
                EAAPI.Method apiMethod = (EAAPI.Method)apiElement.Methods.AddNew(Name, Type);

                apiMethod.Update();

                apiElement.Methods.Refresh();

                result = new MethodDataModel(apiMethod);
                result.Repository = _owner.Repository!;
            }
            else
            {
                result = new MethodDataModel();
                result.Name = Name;
                result.ReturnType = Type;
                result.ParentID = _owner.ElementID;
                result.Repository = _owner.Repository!;
            }

            _abstractDataCache._methodCache.Add(result);

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            Method toDelete = this[index];

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null && toDelete.MethodID != 0)
            {
                EAAPI.Element apiElement = apiRepository.GetElementByID(_owner.ElementID);

                if (apiElement != null)
                {
                    for (short counter = 0; counter < apiElement.Methods.Count; counter++)
                    {
                        EAAPI.Method currentMethod = (EAAPI.Method)apiElement.Methods.GetAt(counter);

                        if (currentMethod.MethodID == toDelete.MethodID)
                        {
                            apiElement.Methods.Delete(counter);
                            apiElement.Methods.Refresh();
                            break;
                        }
                    }
                }
            }

            _abstractDataCache._methodCache.Remove(toDelete);

            RemoveAt(index);
        }

        public override void DeleteAt(short index, bool Refresh)
        {
            Delete(index);
        }
    }
}
