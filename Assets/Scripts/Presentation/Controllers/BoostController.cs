using Presentation.Views;
using System;
using System.Collections.Generic;
using UnityEngine;
using Universal;
using Universal.CustomAttributes;
using Zenject;
using Random = System.Random;

namespace Presentation.Controllers
{
    [BindAsSingle]
    class BoostController
    {
        [Inject]
        readonly BallsController _ballController;

        internal List<AbstractBoostView> BoostViews = new();
        internal List<(float, float, BoostType)> BoostData = new();

        private Random random = new();

        private Dictionary<BoostType, float> _spawnChance = new()
        {
            {BoostType.CountBoost, 0.05f },
            {BoostType.StrengthBoost, 0.08f }
        };


        internal void SpawnBoost(Vector3 position)
        {
            BoostType boostType = (BoostType)random.Next(0, Enum.GetValues(typeof(BoostType)).Length);
            float randomChance = (float)random.NextDouble();

            if (_spawnChance[boostType] >= randomChance)
            {
                AbstractBoostView view =  GameSceneReferences.BoostCreatorViewRef.SpawnBoost(position, boostType);
                BoostViews.Add(view);
            }
        }

        internal void LoadBoosts(List<(float, float, BoostType)> boostData) => BoostViews.AddRange(GameSceneReferences.BoostCreatorViewRef.LoadBoosts(boostData));

        internal void OnGather(AbstractBoostView view)
        {   
            switch (view.Type)
            {
                case BoostType.CountBoost:
                    CountBoostView countBoostView = (CountBoostView)view;
                    _ballController.SpawnBalls(countBoostView.BallsToSpawn);
                    break;
                case BoostType.StrengthBoost:
                    StrengthBoostView strengthBoostView = (StrengthBoostView)view;
                    _ballController.ChangeBallsDamage(strengthBoostView.DamageBoost);
                    break;
            }

            Signals.OnBoostGather?.Invoke(view.Type);

            if (BoostViews.Contains(view))
                RemoveBoostView(view);
        }

        internal void DestroyAllBoosts()
        {   
            int counter = BoostViews.Count;
            for (int i = counter - 1; i >= 0; i--)
                RemoveBoostView(BoostViews[i]);

            BoostData.Clear();
        }

        internal void PauseBoosts()
        {
            for (int i = 0; i < BoostViews.Count; i++)
            {   
                BoostViews[i].PauseCoroutine();
                BoostData.Add((BoostViews[i].transform.position.x, BoostViews[i].transform.position.y, BoostViews[i].Type));
            }
        }

        internal void ResumeBoosts()
        {
            for (int i = 0; i < BoostViews.Count; i++)
                BoostViews[i].ResumeCoroutine();

            BoostData.Clear();
        }

        private void RemoveBoostView(AbstractBoostView view)
        {   
            int index = BoostViews.IndexOf(view);
            BoostViews.RemoveAt(index); 
            GameSceneReferences.BoostCreatorViewRef.DestroyBoost(view);
        }
    }
}