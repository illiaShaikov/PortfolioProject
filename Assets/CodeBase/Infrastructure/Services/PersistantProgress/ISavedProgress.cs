using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistantProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress playerProgress);
    }
}