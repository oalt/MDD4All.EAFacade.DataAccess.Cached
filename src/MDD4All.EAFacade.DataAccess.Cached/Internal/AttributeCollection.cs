using MDD4All.EAFacade.DataModels.Contracts;
using Attribute = MDD4All.EAFacade.DataModels.Contracts.Attribute;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class AttributeCollection : GenericCollection<Attribute>
    {
        private readonly Element _owner;

        public AttributeCollection(Element owner)
        {
            _owner = owner;
        }

        public override object AddNew(string Name, string Type)
        {
            AttributeDataModel result;

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            EAAPI.Element? apiElement = null;

            if (apiRepository != null)
            {
                apiElement = apiRepository.GetElementByID(_owner.ElementID);
            }

            if (apiElement != null)
            {
                EAAPI.Attribute apiAttribute = (EAAPI.Attribute)apiElement.Attributes.AddNew(Name, Type);

                apiAttribute.Update();

                apiElement.Attributes.Refresh();

                result = new AttributeDataModel(apiAttribute);
                result.Repository = _owner.Repository;
            }
            else
            {
                result = new AttributeDataModel();
                result.Name = Name;
                result.Type = Type;
                result.Repository = _owner.Repository;
            }

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            Attribute toDelete = this[index];

            EAAPI.Repository? apiRepository = _owner.Repository?.ApiRepository;

            if (apiRepository != null && toDelete.AttributeID != 0)
            {
                EAAPI.Element apiElement = apiRepository.GetElementByID(_owner.ElementID);

                if (apiElement != null)
                {
                    for (short counter = 0; counter < apiElement.Attributes.Count; counter++)
                    {
                        EAAPI.Attribute currentAttribute = (EAAPI.Attribute)apiElement.Attributes.GetAt(counter);

                        if (currentAttribute.AttributeID == toDelete.AttributeID)
                        {
                            apiElement.Attributes.Delete(counter);
                            apiElement.Attributes.Refresh();
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
