using Godot;
using System;

public partial class MainMenuLevelController : LevelController
{
    [Export] public GameModePanelController _classicGameModePanel;
    public override LevelScene CurrLevel {get { return LevelScene.MAIN_MENU;}}
    public override void _Ready()
    {
        _classicGameModePanel.GameModeSelected += level => ChangeScene(LevelScene.MAYHEM_MODE);
    }

    public override void ReadyLevel()
    {
        return;
    }

    public override void FinishLevel()
    {
        return;
    }
}