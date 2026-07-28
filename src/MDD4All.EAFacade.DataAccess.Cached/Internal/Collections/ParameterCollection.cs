using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal.Collections
{
    internal class ParameterCollection : GenericCollection<Parameter>
    {
        private readonly MethodDataModel _owner;

        public ParameterCollection(MethodDataModel owner)
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
            ParameterDataModel result;

            EAAPI.Method? apiMethod = FindApiMethod();

            if (apiMethod != null)
            {
                EAAPI.Parameter apiParameter = (EAAPI.Parameter)apiMethod.Parameters.AddNew(Name, Type);

                result = new ParameterDataModel(apiParameter);
                result.Repository = _owner.Repository;
                result.ParentElementID = _owner.ParentID;
            }
            else
            {
                result = new ParameterDataModel();
                result.Name = Name;
                result.Type = Type;
                result.OperationID = _owner.MethodID;
                result.Repository = _owner.Repository;
                result.ParentElementID = _owner.ParentID;
            }

            Add(result);

            return result;
        }

        public override void Delete(short index)
        {
            Parameter toDelete = this[index];

            EAAPI.Method? apiMethod = FindApiMethod();

            if (apiMethod != null && !string.IsNullOrEmpty(toDelete.ParameterGUID))
            {
                for (short counter = 0; counter < apiMethod.Parameters.Count; counter++)
                {
                    EAAPI.Parameter currentParameter = (EAAPI.Parameter)apiMethod.Parameters.GetAt(counter);

                    if (currentParameter.ParameterGUID == toDelete.ParameterGUID)
                    {
                        apiMethod.Parameters.Delete(counter);
                        apiMethod.Parameters.Refresh();
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
