using Presentation.Models;
using UI;

static class PlayerSystem
{
    internal static bool Initialized;

    public static void Initialize()
    {
        UIReferences.InputController.AddMousePosition(PresentationViewModel.UpdateMousePosition);
        UIReferences.InputController.AddGameMenuAction();
        UIReferences.InputController.AddBallPush();
        Initialized = true;
    }

    public static void Deinitialize()
    {
        UIReferences.InputController.RemoveMousePosition(PresentationViewModel.UpdateMousePosition);
        UIReferences.InputController.RemoveGameMenuAction();
        UIReferences.InputController.RemoveBallPush();
        Initialized = false;
    }

    public static void DeinitializeForGameMenu() => UIReferences.InputController.RemoveBallPush();

    public static void InitializeForGameMenu() => UIReferences.InputController.AddBallPush();
}
