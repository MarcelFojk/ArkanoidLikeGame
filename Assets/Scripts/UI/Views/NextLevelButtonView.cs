using UnityEngine;
using UnityEngine.UI;
using Universal;

namespace UI.Views
{
    class NextLevelButtonView : MonoBehaviour
    {
        [SerializeField]
        Button _button;

        private void Awake() => _button.onClick.AddListener(NextLevel);

        private void NextLevel() => Signals.ChangeLevel?.Invoke();
    }
}