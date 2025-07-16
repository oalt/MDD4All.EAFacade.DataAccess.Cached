using System.Collections.Generic;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class PackageFlagDataModel
    {
        public Dictionary<string, string> PackageFlagData { get; set; } = new Dictionary<string, string>();

        public PackageFlagDataModel(string packageFlags)
        {
            PackageFlagData = new Dictionary<string, string>();
            string[] styleTokens = packageFlags.Split(new char[] { ';' });

            foreach (string token in styleTokens)
            {
                if (!string.IsNullOrEmpty(token.Trim()))
                {
                    string[] styleKeyValuePair = token.Split(new char[] { '=' });
                    if (!PackageFlagData.ContainsKey(styleKeyValuePair[0]))
                    {
                        PackageFlagData.Add(styleKeyValuePair[0], styleKeyValuePair[1]);
                    }
                }
            }
        }
    }
}
