using System;
using System.IO;

namespace MDD4All.EAFacade.DataAccess.Cached.Tests
{
    public class EaDatabaseFixture
    {
        private const string OriginalDatabaseFileName = "Empty.eapx";

        private const string WorkingCopyFileName = "Empty.Test.eapx";

        public string DatabasePath { get; }

        public EaDatabaseFixture()
        {
            string assetsDirectory = Path.Combine(AppContext.BaseDirectory, "Assets");

            string originalDatabasePath = Path.Combine(assetsDirectory, OriginalDatabaseFileName);

            DatabasePath = Path.Combine(assetsDirectory, WorkingCopyFileName);

            File.Copy(originalDatabasePath, DatabasePath, overwrite: true);
        }
    }
}
