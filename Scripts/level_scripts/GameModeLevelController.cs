using Godot;
using System;
using System.Threading.Tasks;

public partial class GameModeLevelController : LevelController
{
    [Export] private bool _runUniqueScene = false;
    [Export] private int _playerNum;
    [Export] private LevelScene _currLevelScene;
    public override LevelScene CurrLevel {get {return _currLevelScene;}}
    public Godot.Collections.Dictionary<string, string> levelSettings = new();

    public override void _Ready()
    {
        if(_runUniqueScene){
            _ = RunUniqueScene();
        }
        
    }
    private async Task RunUniqueScene(){
        // Set up basic value for special scene run
        levelSettings.Add("player_num", _playerNum.ToString());
        for (int i = 0; i < _playerNum; i++)
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
        await GlobalHelper.Instance.GameController.ReadyGameAsync(levelSettings);
    }
    public override void FinishLevel(LevelScene nextLevel)
    {
        base.FinishLevel(nextLevel);
        GlobalHelper.Instance.GameController = null;
    }
    public override void StartLevel(){
        base.StartLevel();
        GlobalHelper.Instance.GameController.StartGame();
    }
}