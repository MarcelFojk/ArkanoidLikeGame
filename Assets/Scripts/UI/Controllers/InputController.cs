using Presentation.Models;
using System;
using UI.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using Universal.CustomAttributes;

namespace UI.Controllers
{
    [BindAsSingle]
    class InputController
    {
        InputSystem _inputSystem;

        Action<Vector2> _mousePosition;

        internal void Initialize()
        {
            _inputSystem = new();
            _inputSystem.Main.Enable();
        }

        internal void CustomUpdate()
        {
            _mousePosition?.Invoke(_inputSystem.Main.MousePosition.ReadValue<Vector2>());
        }

        void OpenCloseGameMenu(InputAction.CallbackContext callback) => UIViewModel.OpenCloseGameMenu();

        void PushBall(InputAction.CallbackContext callback) => PresentationViewModel.PushBall();


        internal void AddMousePosition(Action<Vector2> mousePositionUpdate) => _mousePosition += mousePositionUpdate;

        internal void AddGameMenuAction() => _inputSystem.Main.OpenGameMenu.performed += OpenCloseGameMenu;

        internal void AddBallPush() => _inputSystem.Main.LeftClick.performed += PushBall;


        internal void RemoveMousePosition(Action<Vector2> mousePositionUpdate) => _mousePosition -= mousePositionUpdate;

        internal void RemoveGameMenuAction() => _inputSystem.Main.OpenGameMenu.performed -= OpenCloseGameMenu;

        internal void RemoveBallPush() => _inputSystem.Main.LeftClick.performed -= PushBall;
    }
}