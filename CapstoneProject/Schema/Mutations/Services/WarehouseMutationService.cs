using System;
using System.Linq;
using CapstoneProject.Model;
using CapstoneProject.Model.Entities;
using CapstoneProject.Model.Exceptions;
using CapstoneProject.Schema.Mutations.inputObjects;

namespace CapstoneProject.Schema.Mutations.Services
{
    public class WarehouseMutationService
    {
        private readonly DataContext _context;

        public WarehouseMutationService(DataContext context)
        {
            _context = context;
        }

        public SparePart ModifySparePart(int id, InputSparePart inputSparePart)
        {
            var sparePart = _context.SpareParts.Find(id);

            if (sparePart == null)
                throw new ResourceNotFoundException($"SparePart with id {id} doesn't exist");

            if (inputSparePart.TypeId != null)
            {
                if (_context.Types.Find(inputSparePart.TypeId) != null)
                {
                    sparePart.TypeId = inputSparePart.TypeId.Value;
                }
                else
                {
                    throw new ResourceNotFoundException($"Type with id {inputSparePart.TypeId} doesn't exist");
                }

            }

            sparePart.Name = inputSparePart.Name ?? sparePart.Name;
            sparePart.Brand = inputSparePart.Brand ?? sparePart.Brand;
            sparePart.Manufacturer = inputSparePart.Manufacturer ?? sparePart.Manufacturer;
            sparePart.Amount = inputSparePart.Amount ?? sparePart.Amount;
            sparePart.DefectiveAmount = inputSparePart.DefectiveAmount ?? sparePart.DefectiveAmount;
            sparePart.Price = inputSparePart.Price ?? sparePart.Price;
            sparePart.TypeId = inputSparePart.TypeId ?? sparePart.TypeId;

            _context.SaveChanges();
            
            return sparePart;
        }

        public SparePartApplication CreateSparePartApplication(int sparePartId, int? requestId = null)
        {
            if (_context.SpareParts.Find(sparePartId) == null)
                throw new ResourceNotFoundException($"Spare part with Id {sparePartId} wasn't found");

            var sparePartApplication = new SparePartApplication()
            {
                SparePartId = sparePartId,
                CreationDate = DateTime.Now,
                RequestId = requestId
            };

            _context.SparePartApplications.Add(sparePartApplication);
            _context.SaveChanges();

            return sparePartApplication;
        }

        public PartRequest CreateSparePartsRequest(InputPartRequest inputPartRequest)
        {
            var partRequest = new PartRequest()
            {
                DueDate = inputPartRequest.DueDate,
                PaymentMethod = inputPartRequest.PaymentMethod,
                IssuingDepartment = inputPartRequest.IssuingDepartment,
            };

            _context.PartRequests.Add(partRequest);

            _context.SaveChanges();

            var sparePartApplications = inputPartRequest.SparePartsIds.Select(id => CreateSparePartApplication(id, partRequest.Id));

            partRequest.SparePartApplications = sparePartApplications.ToArray();

            _context.SaveChanges();

            return partRequest;
        }
    }
}