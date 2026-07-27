using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class ElementChildCollection : GenericCollection<Element>
    {
        private readonly ElementDataModel _owner;

        private readonly AbstractDataCache _abstractDataCache;

        public ElementChildCollection(ElementDataModel owner, AbstractDataCache abstractDataCache)
        {
            _owner = owner;
            _abstractDataCache = abstractDataCache;
        }

        public override object AddNew(string Name, string Type)
        {
            ElementDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Element? apiElement = null;

            if (apiRepository != null)
            {
                apiElement = apiRepository.GetElementByID(_owner.ElementID);
            }

            if (apiElement != null)
            {
                EAAPI.Element apiChildElement = (EAAPI.Element)apiElement.Elements.AddNew(Name, Type);

                apiChildElement.Update();

                apiElement.Elements.Refresh();

                result = new ElementDataModel(apiChildElement);
                result.Repository = _owner.Repository;
                result.AbstractDataCache = _abstractDataCache;
            }
            else
            {
                result = new ElementDataModel();
                result.Name = Name;
                result.Type = Type;
                result.ParentID = _owner.ElementID;
                result.Repository = _owner.Repository;
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
                EAAPI.Element apiElement = apiRepository.GetElementByID(_owner.ElementID);

                if (apiElement != null)
                {
                    for (short counter = 0; counter < apiElement.Elements.Count; counter++)
                    {
                        EAAPI.Element currentElement = (EAAPI.Element)apiElement.Elements.GetAt(counter);

                        if (currentElement.ElementID == toDelete.ElementID)
                        {
                            apiElement.Elements.Delete(counter);
                            apiElement.Elements.Refresh();
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
