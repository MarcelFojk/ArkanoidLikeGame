using System.Collections.Generic;
using UnityEngine;
using Universal;

namespace Presentation.Views
{
    class BoostCreatorView : MonoBehaviour
    {
        internal AbstractBoostView SpawnBoost(Vector3 position, BoostType type)
        {
            AbstractBoostView view = Instantiate(GameSceneReferences.BoostPrefabs[(int)type], position, Quaternion.identity);
            view.transform.parent = GameSceneReferences.BoostContainer;
            return view;
        }

        internal List<AbstractBoostView> LoadBoosts(List<(float, float, BoostType)> boostData)
        {
            List<AbstractBoostView> boostViews = new();
            for (int i = 0; i < boostData.Count; i++)
            {
                AbstractBoostView view = Instantiate(GameSceneReferences.BoostPrefabs[(int)boostData[i].Item3]);
                view.transform.position = new Vector3(boostData[i].Item1, boostData[i].Item2);
                boostViews.Add(view);
            }

            return boostViews;
        }

        internal void DestroyBoost(AbstractBoostView view) => Destroy(view.gameObject);
    }
}