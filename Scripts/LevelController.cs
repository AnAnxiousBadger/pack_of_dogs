using Godot;
using System;
using System.Collections.Generic;

public abstract partial class LevelController : Node3D
{
	public enum LevelScene {MAIN_MENU, CLASSIC_MODE, MAYHEM_MODE};
	public abstract LevelScene CurrLevel {get;}
	public static readonly Dictionary<string, string> levelPathDict = new() {
		{"main_menu", "res://Scenes/main_menu.tscn"},
		{"classic_mode", "res://Scenes/classic_mode.tscn"},
		{"mayhem_mode", "res://Scenes/mayhem_mode.tscn"}
	};
	public static readonly Dictionary<LevelScene, string> levelNameDict = new() {
		{LevelScene.MAIN_MENU, "main_menu"},
		{LevelScene.CLASSIC_MODE, "classic_mode"},
		{LevelScene.MAYHEM_MODE, "mayhem_mode"}
	};
	[Signal] public delegate void LevelChangedEventHandler(string fromName, string toPath, Godot.Collections.Dictionary<string, string> data);
	
    protected void ChangeScene(LevelScene to, Godot.Collections.Dictionary<string, string> data){
		// Signal is detected by SceneController
		EmitSignal(SignalName.LevelChanged, levelNameDict[CurrLevel], levelPathDict[levelNameDict[to]], data);
	}

	public abstract void ReadyLevel(Godot.Collections.Dictionary<string, string> data);
	public abstract void FinishLevel();
}
