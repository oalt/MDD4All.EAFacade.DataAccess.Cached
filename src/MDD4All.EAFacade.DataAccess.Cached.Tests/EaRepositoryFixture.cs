using System;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Tests
{
    public class EaRepositoryFixture : IDisposable
    {
        private const string RepositoryProgId = "EA.Repository";

        public EaDatabaseFixture DatabaseFixture { get; }

        public EAAPI.Repository ApiRepository { get; }

        public EaRepositoryFixture()
        {
            DatabaseFixture = new EaDatabaseFixture();

            Type? repositoryType = Type.GetTypeFromProgID(RepositoryProgId);

            ApiRepository = (EAAPI.Repository)Activator.CreateInstance(repositoryType!)!;

            bool openResult = ApiRepository.OpenFile(DatabaseFixture.DatabasePath);

            if (!openResult)
            {
                throw new InvalidOperationException("Could not open the Enterprise Architect test database at " + DatabaseFixture.DatabasePath + ".");
            }

            ApiRepository.ShowWindow(1);
        }

        public void Dispose()
        {
            ApiRepository.CloseFile();
            ApiRepository.Exit();
        }
    }
}
