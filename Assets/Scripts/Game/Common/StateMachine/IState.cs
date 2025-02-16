using Cysharp.Threading.Tasks;

namespace Game.Common.StateMachine
{
    public interface IState
    {
        public UniTask Enter();
        public UniTask Exit();
    }
}