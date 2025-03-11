using Godot;
using System;
using System.Threading.Tasks;

public partial class MainMenuLevelController : LevelController
{
    [Export] public GameModePanelController _classicGameModePanel;
    [Export] public GameModePanelController _kurGameModePanel;
    [Export] public AudioLibrary UIAudioLibrary;
    [Export] private MainMenuZigguratController zigguratController;
    public override LevelScene CurrLevel {get { return LevelScene.MAIN_MENU;}}
    public override void _Ready()
    {
        _classicGameModePanel.GameModeClicked += () => StartLoadingScene(LevelScene.CLASSIC_MODE);
        _classicGameModePanel.GameModeSelected += settings => ChangeScene(LevelScene.CLASSIC_MODE, settings, false);

        _kurGameModePanel.GameModeClicked += () => StartLoadingScene(LevelScene.KUR_MODE);
        _kurGameModePanel.GameModeSelected += settings => ChangeScene(LevelScene.KUR_MODE, settings, false);
    }

    public async override Task ReadyLevelAsync(Godot.Collections.Dictionary<string, string> data)
    {
        await Task.Run(() =>{});
    }

    public override void FinishLevel(LevelScene nextLevel)
    {
        base.FinishLevel(nextLevel);
    }

    public override void StartLevel()
    {
        base.StartLevel();
        zigguratController.scaleZiggurats = true;
    }
}