using Godot;
using System;

public partial class ClassicModeLevelController : LevelController
{
    [Export] private Button _bottomRightMainMenuButton;
    [Export] private Button _victoryPanelMainMenuButton;
    public override LevelScene CurrLevel {get { return LevelScene.CLASSIC_MODE;}}
    public override void _Ready()
    {
        _bottomRightMainMenuButton.ButtonUp += () => ChangeScene(LevelScene.MAIN_MENU);
        _victoryPanelMainMenuButton.ButtonUp += () => ChangeScene(LevelScene.MAIN_MENU);
    }

    public override void ReadyLevel()
    {
        return;
    }
    public override void FinishLevel()
    {
        //GameController.RemoveInstance();
    }
}