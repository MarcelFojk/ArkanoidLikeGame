using Universal;
using Presentation.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UI.Models;
using UnityEngine;

namespace Initialization
{
    public class Initialization : MonoBehaviour
    {
        private void Awake()
        {
            GameStateMachine<GameState> gameStateMachine = new(
             new[]
             { 
                 (GameState.Initialize, GameState.MainMenu, new[] {GameState.UIScene.ToString(), GameState.CoreScene.ToString(), GameState.MainMenu.ToString()},
                 Array.Empty<string>()),
                 (GameState.MainMenu, GameState.GameScene, new[] {GameState.GameScene.ToString()}, new[] {GameState.MainMenu.ToString() }),
                 (GameState.GameScene, GameState.MainMenu, new[] {GameState.MainMenu.ToString()}, new[] {GameState.GameScene.ToString() })
             }, 
             new List<(GameState, Action, Action)>()
             {
                 (GameState.Initialize, null, null),
                 (GameState.MainMenu, MainMenuOnEntry, MainMenuOnExit),
                 (GameState.GameScene, GameSceneOnEntry, GameSceneOnExit),
             }, GameState.Initialize);

            GameStateMachine<GameState>.ChangeGameState(GameState.MainMenu);
        }

        private void Update()
        {   
            UIViewModel.CustomUpdate();
            PresentationViewModel.CustomUpdate();
        }

        private void MainMenuOnEntry()
        {
            UIViewModel.MainMenuOnEntry();
        }

        private void MainMenuOnExit() { }

        private void GameSceneOnEntry()
        {
            UIViewModel.GameSceneOnEntry();
            PresentationViewModel.GameSceneOnEntry();
        }

        private void GameSceneOnExit()
        {
            UIViewModel.GameSceneOnExit();
            PresentationViewModel.GameSceneOnExit();
        }

    }
}