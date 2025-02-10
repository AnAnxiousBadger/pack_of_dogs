using Godot;
using System;

public partial class MayhemModeLevelController : LevelController
{
    [Export] public Button _button;
    public override LevelScene CurrLevel {get { return LevelScene.MAYHEM_MODE;}}
    public override void _Ready()
    {
        //_button.ButtonUp += settings => ChangeScene(LevelScene.MAIN_MENU, settings);
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