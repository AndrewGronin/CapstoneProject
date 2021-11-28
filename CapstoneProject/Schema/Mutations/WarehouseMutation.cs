using CapstoneProject.Schema.Mutations.inputObjects;
using CapstoneProject.Schema.Mutations.Services;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Authorization;

namespace CapstoneProject.Schema.Mutations
{
    [ExtendObjectType(typeof(RootMutation))]
    public class WarehouseMutation
    {
        public SparePartApplication CreateSparePartApplication(
            [Service] WarehouseMutationService service,
            int sparePartId)
        {
            return service.CreateSparePartApplication(sparePartId);
        }

        public PartRequest CreateSparePartsRequest(
            [Service] WarehouseMutationService service,
            InputPartRequest inputPartRequest)
        {
             return service.CreateSparePartsRequest(inputPartRequest);
        }

        public SparePart ModifySparePart(
            [Service] WarehouseMutationService service,
            int sparePartId,
            InputSparePart sparePart)
        {
            return service.ModifySparePart(sparePartId, sparePart);
        }
    }
}