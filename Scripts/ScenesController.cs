using Godot;
using System;
using System.Collections.Generic;

public partial class ScenesController : Node
{
	[Export] public LevelController _currLevelController;
    public override void _Ready()
    {
        _currLevelController.LevelChanged += HandleLevelChange;
    }
    private void HandleLevelChange(string fromName, string toPath, Godot.Collections.Dictionary<string, string> data){
		_currLevelController.FinishLevel();
		_currLevelController.LevelChanged -= HandleLevelChange;		
		_currLevelController.QueueFree();

		PackedScene nextLevelScene = GD.Load<PackedScene>(toPath);
		LevelController nextLevel = nextLevelScene.Instantiate() as LevelController;
		AddChild(nextLevel);

		_currLevelController = nextLevel;
		_currLevelController.LevelChanged += HandleLevelChange;
		_currLevelController.ReadyLevel(data);
	}
}
