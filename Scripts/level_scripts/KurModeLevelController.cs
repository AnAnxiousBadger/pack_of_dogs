using Godot;
using System;
using System.Threading.Tasks;

public partial class KurModeLevelController : LevelController
{
    [Export] private bool _runUniqueScene = false;
    public override LevelScene CurrLevel {get { return LevelScene.MAYHEM_MODE;}}
    public Godot.Collections.Dictionary<string, string> levelSettings;
    public override void _Ready()
    {
        if(_runUniqueScene){
            _ = RunUniqueScene();
        }
    }

    private async Task RunUniqueScene(){
        // Set up basic value for special scene run
        levelSettings.Add("player_num", "3");
        for (int i = 0; i < 3; i++)
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

    public override void StartLevel()
    {
        base.StartLevel();
        GlobalHelper.Instance.GameController.StartGame();
    }
}