using Zenject;

namespace Game.Common.StateMachine
{
    public interface IStateMachine : IInitializable
    {
        public void RegisterState(IState state);
        public void UnregisterState(IState state);
        
        public bool GotoState(IState state);
        public void GotoState<TIState>() where TIState : IState;
        
        public IState CurrentState { get; }
    }
}