using System.Collections.Generic;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ConnectorStyleDataModel
    {
        public Dictionary<string, string> StyleData { get; set; } = new Dictionary<string, string>(); 

        public ConnectorStyleDataModel(string connectorStyle) 
        {
            StyleData = new Dictionary<string, string>();
            string[] styleTokens = connectorStyle.Split(new char[] { ';' });

            foreach (string token in styleTokens)
            {
                if (!string.IsNullOrEmpty(token.Trim()))
                {
                    string[] styleKeyValuePair = token.Split(new char[] { '=' });
                    if (!StyleData.ContainsKey(styleKeyValuePair[0]))
                    {
                        StyleData.Add(styleKeyValuePair[0], styleKeyValuePair[1]);
                    }
                }
            }
        }
    }
}
