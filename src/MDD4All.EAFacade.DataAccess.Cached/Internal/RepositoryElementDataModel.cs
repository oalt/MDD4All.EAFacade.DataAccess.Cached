using MDD4All.EAFacade.DataModels.Contracts;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal abstract class RepositoryElementDataModel
    {
        public Repository Repository { get; set; }

        public AbstractDataCache AbstractDataCache { get; set; }
    }
}
