using Godot;
using System;
using System.Threading.Tasks;

public partial class ClassicModeLevelController : LevelController
{
    [Export] private bool _runUniqueScene = false;
    [Export] private Button _bottomRightMainMenuButton;
    [Export] private Button _victoryPanelMainMenuButton;
    [Export] private Button _bottomRightMenuReplayButton;
    [Export] private Button _victoryPanelReplayButton;
    public override LevelScene CurrLevel {get { return LevelScene.CLASSIC_MODE;}}
    private Godot.Collections.Dictionary<string, string> _settings = new();
    public override void _Ready()
    {
        _bottomRightMainMenuButton.ButtonUp += () => ChangeScene(LevelScene.MAIN_MENU, null, true);
        _victoryPanelMainMenuButton.ButtonUp += () => ChangeScene(LevelScene.MAIN_MENU, null, true);
        _bottomRightMenuReplayButton.ButtonUp += () => ChangeScene(LevelScene.CLASSIC_MODE, _settings,true);
        _victoryPanelReplayButton.ButtonUp += () => ChangeScene(LevelScene.CLASSIC_MODE, _settings, true);

        if(_runUniqueScene){
            _ = RunUniqueScene();
        }
        
    }
    private async Task RunUniqueScene(){
        // Set up basic value for special scene run
        _settings.Add("player_num", "2");
        for (int i = 0; i < 2; i++)
        {                
            _settings.Add($"player_{i}_name", "real_player");
            _settings.Add($"player_{i}_type", "real_player");
        }
        await ReadyLevelAsync(_settings);
    }

    public async override Task ReadyLevelAsync(Godot.Collections.Dictionary<string, string> settings)
    {
        _settings = settings;
        await GlobalClassesHolder.Instance.GameController.ReadyGameAsync(_settings);
        if(!AudioManager.Instance.IsMusicPlaying()){
            AudioManager.Instance.StartMusic();
        }
        // Listened to by ScenesController
        EmitSignal(SignalName.LevelReadied);
    }
    public override void FinishLevel(LevelScene nextLevel)
    {
        if(nextLevel != CurrLevel){
            AudioManager.Instance.StopMusic();
        }
        GlobalClassesHolder.Instance.GameController = null;
    }
}