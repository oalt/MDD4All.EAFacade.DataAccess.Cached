using MDD4All.EAFacade.DataAccess.Cached;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using EAAPI = EA;
using EAF = MDD4All.EAFacade.DataModels.Contracts;

namespace TestCachedDataAccess
{
    public class TestPackageCache
    {
        private EAAPI.Repository _originalRepo; 

        public TestPackageCache()
        {
            if(OpenEA())
            {
                CachedRepository cachedRepository = new CachedRepository(_originalRepo);

                cachedRepository.InitializePackageCache();

                EAF.Collection models = cachedRepository.Models;

                _originalRepo.Exit();

            }


        }

        private bool OpenEA()
        {
            string progId = "EA.Repository";
            Type type = Type.GetTypeFromProgID(progId);
            EAAPI.Repository repository = Activator.CreateInstance(type) as EAAPI.Repository;

            bool openResult = repository.OpenFile(AssemblyDirectory + "/Assets/TestEA.eapx");

            if (openResult)
            {
                _originalRepo = repository;
                repository.ShowWindow(1);

            }

            return openResult;
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
