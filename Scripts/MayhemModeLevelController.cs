using Godot;
using System;

public partial class MayhemModeLevelController : LevelController
{
    [Export] public Button _button;
    public override LevelScene CurrLevel {get { return LevelScene.MAYHEM_MODE;}}
    public override void _Ready()
    {
        _button.ButtonUp += () => ChangeScene(LevelScene.MAIN_MENU);
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