using Universal;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    class ContinueButtonView : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void Awake() => _button.onClick.AddListener(ContinueGame);

        private void ContinueGame()
        {
            UniversalData.LoadedFromSave = true;
            GameStateMachine<GameState>.ChangeGameState(GameState.GameScene);
        }
    }
}