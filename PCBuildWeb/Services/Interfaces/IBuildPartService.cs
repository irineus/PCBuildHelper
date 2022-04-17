using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Services.Interfaces
{
    public interface IBuildPartService<T>
    {
        Task<T?> FindByIdAsync(int id);
        Task<List<T>> FindAllAsync();
    }
}
