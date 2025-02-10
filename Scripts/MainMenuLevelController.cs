using Godot;
using System;

public partial class MainMenuLevelController : LevelController
{
    [Export] public GameModePanelController _classicGameModePanel;
    public override LevelScene CurrLevel {get { return LevelScene.MAIN_MENU;}}
    public override void _Ready()
    {
        _classicGameModePanel.GameModeSelected += settings => ChangeScene(LevelScene.CLASSIC_MODE, settings);
    }

    public override void ReadyLevel(Godot.Collections.Dictionary<string, string> data)
    {
        
        return;
    }

    public override void FinishLevel()
    {
        return;
    }
}