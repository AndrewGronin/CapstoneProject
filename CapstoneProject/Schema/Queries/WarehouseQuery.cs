using System.Collections.Generic;
using CapstoneProject.Schema.Queries.Services;
using HotChocolate;
using HotChocolate.Types;

namespace CapstoneProject.Schema.Queries
{
    [ExtendObjectType(typeof(RootQuery))]
    public class WarehouseQuery
    {
        public IEnumerable<SparePart> SpareParts([Service]WarehouseQueryService service)
        {
            return service.GetSpareParts();
        }
    }
}