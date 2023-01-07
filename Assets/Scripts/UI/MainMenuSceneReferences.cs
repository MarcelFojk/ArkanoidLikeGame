using UnityEngine;

namespace UI
{
    class MainMenuSceneReferences : MonoBehaviour
    {
        internal static GameObject ContinueButton;

        [SerializeField]
        GameObject _continueButton;

        private void OnEnable()
        {
            ContinueButton = _continueButton;
        }
    }
}