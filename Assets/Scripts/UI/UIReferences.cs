using UI.Controllers;
using Universal.CustomAttributes;
using Zenject;

namespace UI
{
    [BindAsSingle]
    class UIReferences : IInitializable
    {
        internal static InputController InputController => _instance._inputController;

        [Inject]
        readonly InputController _inputController;

        static UIReferences _instance;
        
        public void Initialize() => _instance = this;
    }
}