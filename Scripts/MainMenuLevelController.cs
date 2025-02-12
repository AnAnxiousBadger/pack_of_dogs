using Godot;
using System;
using System.Threading.Tasks;

public partial class MainMenuLevelController : LevelController
{
    [Export] public GameModePanelController _classicGameModePanel;
    [Export] public AudioLibrary UIAudioLibrary;
    public override LevelScene CurrLevel {get { return LevelScene.MAIN_MENU;}}
    public override void _Ready()
    {
        _classicGameModePanel.GameModeClicked += () => StartLoadingScene(LevelScene.CLASSIC_MODE);
        _classicGameModePanel.GameModeSelected += settings => ChangeScene(LevelScene.CLASSIC_MODE, settings, false);
    }

    public async override Task ReadyLevelAsync(Godot.Collections.Dictionary<string, string> data)
    {
        await Task.Run(() => 
            {
                // Listened to by ScenesController
                EmitSignal(SignalName.LevelReadied);
            }
        );
    }

    public override void FinishLevel(LevelScene nextLevel)
    {
        return;
    }

    public override void StartLevel()
    {
        return;
    }
}