using Godot;
using System;

public partial class ClassicModeLevelController : LevelController
{
    [Export] private Button _bottomRightMainMenuButton;
    [Export] private Button _victoryPanelMainMenuButton;
    [Export] private Button _bottomRightMenuReplayButton;
    public override LevelScene CurrLevel {get { return LevelScene.CLASSIC_MODE;}}
    public override void _Ready()
    {
        _bottomRightMainMenuButton.ButtonUp += () => ChangeScene(LevelScene.MAIN_MENU, null);
        _victoryPanelMainMenuButton.ButtonUp += () => ChangeScene(LevelScene.MAIN_MENU, null);
        _bottomRightMenuReplayButton.ButtonUp += () => ChangeScene(LevelScene.CLASSIC_MODE, null);
    }

    public override void ReadyLevel(Godot.Collections.Dictionary<string, string> data)
    {
        GD.Print(data); // SET UP DATA AND STORE IT TO CALL WHEN REPLAY
        return;
    }
    public override void FinishLevel()
    {
        GlobalClassesHolder.Instance.GameController = null;
    }
}