using Godot;
using System;

public partial class ScenesController : Node
{
	[Export] public LevelController _currLevelController;
    public override void _Ready()
    {
        _currLevelController.LevelChanged += HandleLevelChange;
    }
    private void HandleLevelChange(string fromName, string toPath){

		PackedScene nextLevelScene = GD.Load<PackedScene>(toPath);
		LevelController nextLevel = nextLevelScene.Instantiate() as LevelController;
		AddChild(nextLevel);

		_currLevelController.FinishLevel();
		_currLevelController.LevelChanged -= HandleLevelChange;		
		_currLevelController.QueueFree();

		_currLevelController = nextLevel;
		_currLevelController.LevelChanged += HandleLevelChange;
		_currLevelController.ReadyLevel();
	}
}
