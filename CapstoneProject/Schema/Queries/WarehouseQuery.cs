using System;
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

        public IEnumerable<SparePartApplication> GetSparePartsApplications(
            [Service]WarehouseQueryService service,
            IEnumerable<int> typeIds,
            IEnumerable<int> responsibleEmployeeIds,
            IEnumerable<string> manufacturers)
        {
            return service.GetSparePartsApplications(typeIds, responsibleEmployeeIds, manufacturers);
        }
        
        public IEnumerable<ResponsibleEmployee> GetResponsibleEmployee([Service]WarehouseQueryService service)
        {
            return service.GetResponsibleEmployee();
        }
        
        public IEnumerable<SparePart> GetDefectedSparePart([Service]WarehouseQueryService service)
        {
            return service.GetDefectedSparePart();
        }

        public IEnumerable<SparePartsIssue> GetSparePartsIssueList([Service] WarehouseQueryService service)
        {
            return service.GetSparePartsIssueList();
        }

        public IEnumerable<PartRequest> GetUnissuedRequests(
            [Service] WarehouseQueryService service,
            IEnumerable<IssuingDepartment> departments,
            IEnumerable<int> typeIds,
            IEnumerable<int> responsibleEmployeeIds,
            DateTime? dueDateLowerDatetimeBorder,
            DateTime? dueDateUpperDateTimeBorder,
            IEnumerable<PaymentMethod> paymentMethods)
        {
            return service.GetUnissuedRequests(
                departments,
                typeIds,
                responsibleEmployeeIds,
                dueDateLowerDatetimeBorder,
                dueDateUpperDateTimeBorder,
                paymentMethods);
        }
    }
}