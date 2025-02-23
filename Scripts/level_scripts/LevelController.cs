using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

public abstract partial class LevelController : Node3D
{
	[Export] public AudioLibrary LevelMusicAudioLibrary {get; private set;}
	public enum LevelScene {MAIN_MENU, CLASSIC_MODE, MAYHEM_MODE, QUIT};
	public abstract LevelScene CurrLevel {get;}
	public static readonly Dictionary<string, string> levelPathDict = new() {
		{"main_menu", "res://Scenes/levels/main_menu.tscn"},
		{"classic_mode", "res://Scenes/levels/classic_mode.tscn"},
		{"mayhem_mode", "res://Scenes/levels/mayhem_mode.tscn"}
	};
	public static readonly Dictionary<LevelScene, string> levelSceneTolevelNameDict = new() {
		{LevelScene.MAIN_MENU, "main_menu"},
		{LevelScene.CLASSIC_MODE, "classic_mode"},
		{LevelScene.MAYHEM_MODE, "mayhem_mode"}
	};
	
	protected void StartLoadingScene(LevelScene scene){
		ScenesController.Instance.StartLoadingNextLevelScene(levelPathDict[levelSceneTolevelNameDict[scene]]);
	}
    public void ChangeScene(LevelScene to, Godot.Collections.Dictionary<string, string> data, bool doLoadScene){
		ScenesController.Instance.HandleLevelChangeAsync(CurrLevel, to, data, doLoadScene);
	}

	public abstract Task ReadyLevelAsync(Godot.Collections.Dictionary<string, string> data);
	public virtual void FinishLevel(LevelScene nextLevel){
		if(nextLevel != CurrLevel){
            AudioManager.Instance.StopMusic();
        }
	}
	public virtual void StartLevel(){
		if(!AudioManager.Instance.IsMusicPlaying()){
			Dictionary<string, object> randomMusic = LevelMusicAudioLibrary?.GetRandomSound();
			if(randomMusic != null){
				AudioManager.Instance.PlayMusic(randomMusic);
			}
        }
	}
}
