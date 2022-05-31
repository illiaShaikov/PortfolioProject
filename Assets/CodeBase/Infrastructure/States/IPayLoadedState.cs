namespace CodeBase.Infrastructure.States
{
    public interface IPayLoadedState<TPayLoad> : IExitableState
    {
        void Enter(TPayLoad state);
    }
}