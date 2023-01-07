using Universal;
using UnityEngine;
using UnityEngine.UI;
using Universal.SaveSystem;

namespace UI.Views
{
    class NewGameButtonView : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void Awake() => _button.onClick.AddListener(StartNewGame);

        private void StartNewGame()
        {
            UniversalData.LoadedFromSave = false;
            GameStateMachine<GameState>.ChangeGameState(GameState.GameScene);
        }
    }
}