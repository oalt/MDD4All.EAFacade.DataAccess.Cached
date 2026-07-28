using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class MethodTagCollection : GenericCollection<MethodTag>
    {
        private readonly MethodDataModel _owner;

        public MethodTagCollection(MethodDataModel owner)
        {
            _owner = owner;
        }

        private EAAPI.Method? FindApiMethod()
        {
            EAAPI.Method? result = null;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null)
            {
                EAAPI.Element? apiElement = apiRepository.GetElementByID(_owner.ParentID);

                if (apiElement != null)
                {
                    for (short counter = 0; counter < apiElement.Methods.Count; counter++)
                    {
                        EAAPI.Method currentMethod = (EAAPI.Method)apiElement.Methods.GetAt(counter);

                        if (currentMethod.MethodID == _owner.MethodID)
                        {
                            result = currentMethod;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public override object AddNew(string Name, string Type)
        {
            MethodTagDataModel result;

            EAAPI.Method? apiMethod = FindApiMethod();

            if (apiMethod != null)
            {
                EAAPI.MethodTag apiMethodTag = (EAAPI.MethodTag)apiMethod.TaggedValues.AddNew(Name, Type);

                result = new MethodTagDataModel(apiMethodTag);
                result.Repository = _owner.Repository;
                result.ParentElementID = _owner.ParentID;
            }
            else
            {
                result = new MethodTagDataModel();
                result.Name = Name;
                result.MethodID = _owner.MethodID;
                result.Repository = _owner.Repository;
                result.ParentElementID = _owner.ParentID;
            }

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            MethodTag toDelete = this[index];

            EAAPI.Method? apiMethod = FindApiMethod();

            if (apiMethod != null && !string.IsNullOrEmpty(toDelete.TagGUID))
            {
                for (short counter = 0; counter < apiMethod.TaggedValues.Count; counter++)
                {
                    EAAPI.MethodTag currentMethodTag = (EAAPI.MethodTag)apiMethod.TaggedValues.GetAt(counter);

                    if (currentMethodTag.TagGUID == toDelete.TagGUID)
                    {
                        apiMethod.TaggedValues.Delete(counter);
                        apiMethod.TaggedValues.Refresh();
                        break;
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
