using vehicle_management_backend.Models;
using vehicle_management_backend.Services.Dto.Request;

namespace vehicle_management_backend.Services.Interfaces;

public interface IPartsService
{
    Task<List<Part>> GetAllParts();
    Task<Part?> GetPartById(long id);
    Task<Part> CreatePart(CreatePartDto dto);
    Task<Part?> UpdatePart(long id, CreatePartDto dto);
    Task<bool> DeletePart(long id);
}