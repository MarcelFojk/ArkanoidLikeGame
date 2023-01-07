using UnityEngine;
using UnityEngine.UI;
using Universal;

namespace UI.Views
{
    class GameOverQuitButtonView : MonoBehaviour
    {
        [SerializeField]
        Button _button;

        private void Awake() => _button.onClick.AddListener(ChangeScene);

        private void ChangeScene() => GameStateMachine<GameState>.ChangeGameState(GameState.MainMenu);
    }
}