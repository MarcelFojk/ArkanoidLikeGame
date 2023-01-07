using TMPro;
using UI.Views;
using UnityEngine;

namespace UI
{
    class UISceneReferences : MonoBehaviour
    {
        internal static GameMenuView GameMenuViewRef;

        internal static TextMeshProUGUI Score;

        internal static TextMeshProUGUI HighScore;

        internal static TextMeshProUGUI Time;

        internal static Transform Health;

        internal static GameObject ProgressUI;

        internal static GameObject GameOverUI;

        internal static TextMeshProUGUI GameOverScore;

        internal static TextMeshProUGUI GameOverHighScore;

        internal static TextMeshProUGUI GameOverTileScore;

        internal static TextMeshProUGUI GameOverBoostScore;

        internal static TextMeshProUGUI GameOverTimeScore;

        internal static TextMeshProUGUI GameOverHealthScore;

        internal static GameObject CompletedUI;

        internal static TextMeshProUGUI CompletedScore;

        internal static TextMeshProUGUI CompletedHighScore;

        internal static TextMeshProUGUI CompletedTileScore;

        internal static TextMeshProUGUI CompletedBoostScore;

        internal static TextMeshProUGUI CompletedTimeScore;

        internal static TextMeshProUGUI CompletedHealthScore;

        [SerializeField]
        GameMenuView _gameMenuView;

        [SerializeField]
        TextMeshProUGUI _score;

        [SerializeField]
        TextMeshProUGUI _highScore;

        [SerializeField]
        TextMeshProUGUI _time;

        [SerializeField]
        Transform _health;

        [SerializeField]
        GameObject _progressUI;

        [SerializeField]
        GameObject _gameOverUI;

        [SerializeField]
        TextMeshProUGUI _gameOverScore;

        [SerializeField]
        TextMeshProUGUI _gameOverHighScore;

        [SerializeField]
        TextMeshProUGUI _gameOverTileScore;

        [SerializeField]
        TextMeshProUGUI _gameOverBoostScore;

        [SerializeField]
        TextMeshProUGUI _gameOverTimeScore;

        [SerializeField]
        TextMeshProUGUI _gameOverHealthScore;

        [SerializeField]
        GameObject _completedUI;

        [SerializeField]
        TextMeshProUGUI _completedScore;

        [SerializeField]
        TextMeshProUGUI _completedHighScore;

        [SerializeField]
        TextMeshProUGUI _completedTileScore;

        [SerializeField]
        TextMeshProUGUI _completedBoostScore;

        [SerializeField]
        TextMeshProUGUI _completedTimeScore;

        [SerializeField]
        TextMeshProUGUI _completedHealthScore;

        private void Awake()
        {
            GameMenuViewRef = _gameMenuView;
            Score = _score;
            HighScore = _highScore;
            Time = _time;
            Health = _health;
            ProgressUI = _progressUI;
            GameOverUI = _gameOverUI;
            GameOverScore = _gameOverScore;
            GameOverHighScore = _gameOverHighScore;
            GameOverTileScore = _gameOverTileScore;
            GameOverBoostScore = _gameOverBoostScore;
            GameOverTimeScore = _gameOverTimeScore;
            GameOverHealthScore = _gameOverHealthScore;
            CompletedUI = _completedUI;
            CompletedScore = _completedScore;
            CompletedHighScore = _completedHighScore;
            CompletedTileScore = _completedTileScore;
            CompletedBoostScore = _completedBoostScore;
            CompletedTimeScore = _completedTimeScore;
            CompletedHealthScore = _completedHealthScore;
        }
    }
}