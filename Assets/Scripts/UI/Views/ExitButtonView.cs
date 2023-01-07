using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class ExitButtonView : MonoBehaviour
    {
        [SerializeField]
        Button _button;

        private void Awake() => _button.onClick.AddListener(Application.Quit);
    }
}