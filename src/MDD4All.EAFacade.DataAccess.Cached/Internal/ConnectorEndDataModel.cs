using MDD4All.EAFacade.DataModels.Contracts;
using System;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    public class ConnectorEndDataModel: ConnectorEnd
    {
        public string End => throw new NotImplementedException();

        public string Cardinality { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Visibility { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Role { get; set; } = "";

        public string RoleType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string RoleNote { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Containment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int Aggregation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public int Ordering { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Qualifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Constraint { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool IsNavigable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string IsChangeable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Collection TaggedValues => throw new NotImplementedException();

        public string Stereotype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ObjectType ObjectType => throw new NotImplementedException();

        public string StereotypeEx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Navigable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool OwnedByClassifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool Derived { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool DerivedUnion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public bool AllowDuplicates { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public string Alias { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public string GetLastError()
        {
            throw new NotImplementedException();
        }
    }
}
