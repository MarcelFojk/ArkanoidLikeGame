using UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class BackButtonView : MonoBehaviour
    {
        [SerializeField]
        Button _button;

        private void Awake() => _button.onClick.AddListener(ResumeGame);

        private void ResumeGame() => UIViewModel.OpenCloseGameMenu();
    }
}