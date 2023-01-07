using UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    class SaveButtonView : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void Awake() => _button.onClick.AddListener(SaveGame);

        private void SaveGame() => UIViewModel.SaveLevel();
    }
}