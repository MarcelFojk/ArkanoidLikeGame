using Universal;
using Universal.CustomAttributes;
using Zenject;

namespace UI.Controllers
{
    [BindAsSingle]
    class GameOverController
    {
        [Inject]
        readonly ScoreController _scoreController;

        internal void GameOver()
        {   
            UISceneReferences.GameOverUI.gameObject.SetActive(true);
            UISceneReferences.GameOverScore.SetText(_scoreController.Score.ToString());
            UISceneReferences.GameOverHighScore.SetText(_scoreController.HighScores[UniversalData.CurrentLevel].ToString());
            UISceneReferences.GameOverTileScore.SetText(_scoreController.TileScore.ToString());
            UISceneReferences.GameOverBoostScore.SetText(_scoreController.BoostScore.ToString());
            UISceneReferences.GameOverTimeScore.SetText(_scoreController.TimeScore.ToString());
            UISceneReferences.GameOverHealthScore.SetText(_scoreController.HealthScore.ToString());
        }

        internal void Reset()
        {
            UISceneReferences.GameOverUI.gameObject.SetActive(false);
        }
    }
}