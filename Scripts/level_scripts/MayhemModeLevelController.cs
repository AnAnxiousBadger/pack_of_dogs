using Godot;
using System;
using System.Threading.Tasks;

public partial class MayhemModeLevelController : LevelController
{
    [Export] public Button _button;
    public override LevelScene CurrLevel {get { return LevelScene.MAYHEM_MODE;}}
    public override void _Ready()
    {
        //_button.ButtonUp += settings => ChangeScene(LevelScene.MAIN_MENU, settings);
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
    }
}