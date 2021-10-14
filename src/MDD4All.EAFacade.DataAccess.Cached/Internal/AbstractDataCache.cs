using MDD4All.EAFacade.DataModels.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    public abstract class AbstractDataCache
    {
        protected internal List<Package> _packageCache { get; set; } = new List<Package>();
        protected internal List<Element> _elementCache { get; set; } = new List<Element>();
        protected internal List<Connector> _connectorCache { get; set; } = new List<Connector>();
        protected internal List<Diagram> _diagramCache { get; set; } = new List<Diagram>();

        
        
    }
}
