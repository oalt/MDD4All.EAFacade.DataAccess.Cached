using Xunit;

namespace MDD4All.EAFacade.DataAccess.Cached.Tests
{
    [CollectionDefinition(Name)]
    public class EaDatabaseCollection : ICollectionFixture<EaRepositoryFixture>
    {
        public const string Name = "EA Database";
    }
}
