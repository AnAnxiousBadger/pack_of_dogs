using Godot;
using System;
using System.Threading.Tasks;

public partial class ClassicModeLevelController : LevelController
{
    [Export] private bool _runUniqueScene = false;
    public override LevelScene CurrLevel {get { return LevelScene.CLASSIC_MODE;}}
    public Godot.Collections.Dictionary<string, string> levelSettings = new();
    public override void _Ready()
    {
        if(_runUniqueScene){
            _ = RunUniqueScene();
        }
        
    }
    private async Task RunUniqueScene(){
        // Set up basic value for special scene run
        levelSettings.Add("player_num", "2");
        for (int i = 0; i < 2; i++)
        {
            levelSettings.Add($"player_{i}_name", $"player_{i}");
            levelSettings.Add($"player_{i}_type", "real_player");
        }
        await ReadyLevelAsync(levelSettings);
        StartLevel();
    }

    public async override Task ReadyLevelAsync(Godot.Collections.Dictionary<string, string> settings)
    {
        levelSettings = settings;
        await GlobalClassesHolder.Instance.GameController.ReadyGameAsync(levelSettings);
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
    public override void StartLevel(){
        GlobalClassesHolder.Instance.GameController.StartGame();
    }
}