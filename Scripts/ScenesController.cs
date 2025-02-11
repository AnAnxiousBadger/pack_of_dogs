using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

public partial class ScenesController : Node
{
	public static ScenesController Instance { get; set;}
	[Export] public LevelController _currLevelController;
	[Export] public Control _loadingScreen;
	private PackedScene _nextLoadedScene;
	private Task _loadingTask;
    public override void _EnterTree()
    {
        if(Instance != null){
			QueueFree();
			return;
		}
		Instance = this;
    }
    public async void HandleLevelChangeAsync(LevelController.LevelScene from, LevelController.LevelScene to, Godot.Collections.Dictionary<string, string> data, bool doLoadScene){
		_loadingScreen.Visible = true;
		Task loadingScreenTask = StartLoadingScreenDelayAsync(1500);
		
		_currLevelController.FinishLevel(to);	
		_currLevelController.QueueFree();

		if(doLoadScene){
			string toPath = LevelController.levelPathDict[LevelController.levelSceneTolevelNameDict[to]];
			StartLoadingNextLevelScene(toPath);
		}

		await _loadingTask;

		LevelController nextLevel = _nextLoadedScene.Instantiate() as LevelController;
		AddChild(nextLevel);

		_currLevelController = nextLevel;
		await _currLevelController.ReadyLevelAsync(data);

		await loadingScreenTask;
		_loadingScreen.Visible = false;
	}
	public void StartLoadingNextLevelScene(string path){
		_loadingTask = LoadSceneAsync(path);
	}
	private async Task LoadSceneAsync(string path){
		await Task.Run(() => {
			_nextLoadedScene = GD.Load<PackedScene>(path);
		}
		);
	}
	private async Task StartLoadingScreenDelayAsync(int milisecs){
		await Task.Delay(milisecs);
	}
}
