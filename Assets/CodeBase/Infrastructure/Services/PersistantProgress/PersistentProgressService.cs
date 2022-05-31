using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistantProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}
