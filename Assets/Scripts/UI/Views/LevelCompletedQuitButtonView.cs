using UI.Models;
using UnityEngine;
using UnityEngine.UI;
using Universal;

namespace UI.Views
{
    class LevelCompletedQuitButtonView : MonoBehaviour
    {
        [SerializeField]
        Button _button;

        private void Awake() => _button.onClick.AddListener(QuitAndSave);

        private void QuitAndSave()
        {   
            Signals.OnNextLevel?.Invoke();
            UIViewModel.SaveLevel();
            Signals.QuitAfterLevelCompleted?.Invoke();
            GameStateMachine<GameState>.ChangeGameState(GameState.MainMenu);
        }
    }
}