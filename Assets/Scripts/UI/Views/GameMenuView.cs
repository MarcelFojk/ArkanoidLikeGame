using UnityEngine;

namespace UI.Views
{
    public class GameMenuView : MonoBehaviour
    {
        internal bool GameMenuOpen
        {
            get { return _gameMenuOpen; }
            set
            {
                _gameMenuOpen = value;
                gameObject.SetActive(value);
            }

        }

        bool _gameMenuOpen;
    }
}