using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistantProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}