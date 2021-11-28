using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CapstoneProject.Model;
using CapstoneProject.Model.Entities;
using CapstoneProject.Model.Exceptions;

namespace CapstoneProject.Schema.Queries.Services
{
    public class WarehouseQueryService
    {
        private readonly DataContext _context;

        public WarehouseQueryService(DataContext context)
        {
            _context = context;
        }
        
        public IEnumerable<SparePartApplication> GetSparePartsApplications(
            IEnumerable<int> typeIds,
            IEnumerable<int> responsibleEmployeeIds,
            IEnumerable<string> manufacturers)
        {
            var responsibleEmployeesTypeIds= _context.ResponsibleEmployeesTypes
                .Where(ret=> responsibleEmployeeIds.Contains(ret.EmployeeId))
                .Select(ret => ret.TypeId);

            var filtered = _context.SparePartApplications
                .Where(a => typeIds.Concat(responsibleEmployeesTypeIds).Contains(a.SparePart.TypeId))
                .Where(a => manufacturers.Contains(a.SparePart.Manufacturer));

            return filtered;
        }
        
        public IEnumerable<SparePart> GetSpareParts()
        {
            return _context.SpareParts;
        }
        
        public IEnumerable<ResponsibleEmployee> GetResponsibleEmployee()
        {
            return _context.ResponsibleEmployees;
        }
        
        public IEnumerable<SparePart> GetDefectedSparePart()
        {
            return _context.SpareParts.Where(sp=> sp.DefectiveAmount > 0);
        }

        public IEnumerable<SparePartsIssue> GetSparePartsIssueList()
        {
            var issuedApplications =
                _context.SparePartApplications.Where(spa => spa.Issued ?? false);

            var responsibleEmployeesTypes= _context.ResponsibleEmployeesTypes.AsQueryable();

            var sparePartsIssueList = issuedApplications.Join(
                responsibleEmployeesTypes,
                a => a.SparePart.Type.Id,
                ret => ret.TypeId,
                (a, ret) => new SparePartsIssue()
                {
                    SparePart = a.SparePart,
                    IssuedBy = ret.Employee,
                    IssuingDateTime = a.IssueDate ?? DateTime.MinValue,
                    IssuedTo = a.IssuedTo
                }
                );
                //TODO КИдать ошибку и возвращать корректные данные
                if (sparePartsIssueList.Any(spi => spi.IssuedTo == null || spi.IssuingDateTime == DateTime.MinValue))
                    throw new DataException("Issued Application doesn't contain issuing data");
                return sparePartsIssueList.AsEnumerable();
        }

        public IEnumerable<PartRequest> GetUnissuedRequests(
            IEnumerable<IssuingDepartment> departments,
            IEnumerable<int> typeIds,
            IEnumerable<int> responsibleEmployeeIds,
            DateTime lowerDatetimeBorder,
            DateTime upperDateTimeBorder,
            IEnumerable<PaymentMethod> paymentMethods)
        {
            if (lowerDatetimeBorder > upperDateTimeBorder)
                throw new InvalidClientRequestException("Date gap is impossible");
            
            var responsibleEmployeesTypeIds= _context.ResponsibleEmployeesTypes
                .Where(ret=> responsibleEmployeeIds.Contains(ret.EmployeeId))
                .Select(ret => ret.TypeId);

            var filteredByOwnedFields = _context.PartRequests
                .Where(pr => departments.Contains(pr.IssuingDepartment))
                .Where(pr => paymentMethods.Contains(pr.PaymentMethod))
                .Where(pr => pr.DueDate >= lowerDatetimeBorder && pr.DueDate <= upperDateTimeBorder)
                .Where(pr => 
                    pr.SparePartApplications
                        .Any(a => typeIds.Concat(responsibleEmployeesTypeIds).Contains(a.SparePart.TypeId)));

            return filteredByOwnedFields;
        }
    }
}