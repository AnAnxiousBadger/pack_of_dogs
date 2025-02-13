using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

public partial class ScenesController : Node
{
	public static ScenesController Instance { get; private set;}
	[Export] public LevelController currLevelController;
	[Export] public LoadingScreenController _loadingScreen;
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
    public override void _Ready()
    {
        currLevelController.StartLevel();
    }
    public async void HandleLevelChangeAsync(LevelController.LevelScene from, LevelController.LevelScene to, Godot.Collections.Dictionary<string, string> data, bool doLoadScene){
		_loadingScreen.ShowLoadingScreen();

		Task loadingScreenTask = StartLoadingScreenDelayAsync(_loadingScreen.minimumLoadingScreenTimeInMilliSeconds);
		
		currLevelController.FinishLevel(to);	
		currLevelController.QueueFree();

		if(doLoadScene){
			string toPath = LevelController.levelPathDict[LevelController.levelSceneTolevelNameDict[to]];
			StartLoadingNextLevelScene(toPath);
		}

		await _loadingTask;

		LevelController nextLevel = _nextLoadedScene.Instantiate() as LevelController;
		AddChild(nextLevel);

		currLevelController = nextLevel;
		await currLevelController.ReadyLevelAsync(data);

		await loadingScreenTask;
		_loadingScreen.HideLoadingScreen();
		currLevelController.StartLevel();
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
