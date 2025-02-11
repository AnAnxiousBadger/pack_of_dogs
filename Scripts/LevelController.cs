using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract partial class LevelController : Node3D
{
	public enum LevelScene {MAIN_MENU, CLASSIC_MODE, MAYHEM_MODE, QUIT};
	public abstract LevelScene CurrLevel {get;}
	public static readonly Dictionary<string, string> levelPathDict = new() {
		{"main_menu", "res://Scenes/main_menu.tscn"},
		{"classic_mode", "res://Scenes/classic_mode.tscn"},
		{"mayhem_mode", "res://Scenes/mayhem_mode.tscn"}
	};
	public static readonly Dictionary<LevelScene, string> levelSceneTolevelNameDict = new() {
		{LevelScene.MAIN_MENU, "main_menu"},
		{LevelScene.CLASSIC_MODE, "classic_mode"},
		{LevelScene.MAYHEM_MODE, "mayhem_mode"}
	};
	// Listened to by ScenesController
	[Signal] public delegate void LevelReadiedEventHandler();
	
	protected void StartLoadingScene(LevelScene scene){
		ScenesController.Instance.StartLoadingNextLevelScene(levelPathDict[levelSceneTolevelNameDict[scene]]);
	}
    protected void ChangeScene(LevelScene to, Godot.Collections.Dictionary<string, string> data, bool doLoadScene){
		ScenesController.Instance.HandleLevelChangeAsync(CurrLevel, to, data, doLoadScene);
	}

	public abstract Task ReadyLevelAsync(Godot.Collections.Dictionary<string, string> data);
	public abstract void FinishLevel(LevelScene nextLevel);
}
