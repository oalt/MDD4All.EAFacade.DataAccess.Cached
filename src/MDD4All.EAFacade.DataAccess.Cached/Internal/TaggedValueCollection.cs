using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class TaggedValueCollection : GenericCollection<TaggedValue>
    {
        private readonly Element _owner;

        public TaggedValueCollection(Element owner)
        {
            _owner = owner;
        }

        public override object AddNew(string Name, string Type)
        {
            TaggedValueDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Element? apiElement = null;

            if (apiRepository != null)
            {
                apiElement = apiRepository.GetElementByID(_owner.ElementID);
            }

            if (apiElement != null)
            {
                EAAPI.TaggedValue apiTaggedValue = (EAAPI.TaggedValue)apiElement.TaggedValues.AddNew(Name, Type);

                apiTaggedValue.Update();

                apiElement.TaggedValues.Refresh();

                result = new TaggedValueDataModel(apiTaggedValue);
                result.Repository = _owner.Repository;
            }
            else
            {
                result = new TaggedValueDataModel();
                result.Name = Name;
                result.ElementID = _owner.ElementID;
                result.Repository = _owner.Repository;
            }

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            TaggedValue toDelete = this[index];

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null && !string.IsNullOrEmpty(toDelete.PropertyGUID))
            {
                EAAPI.Element apiElement = apiRepository.GetElementByID(_owner.ElementID);

                if (apiElement != null)
                {
                    for (short counter = 0; counter < apiElement.TaggedValues.Count; counter++)
                    {
                        EAAPI.TaggedValue currentTaggedValue = (EAAPI.TaggedValue)apiElement.TaggedValues.GetAt(counter);

                        if (currentTaggedValue.PropertyGUID == toDelete.PropertyGUID)
                        {
                            apiElement.TaggedValues.Delete(counter);
                            apiElement.TaggedValues.Refresh();
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
