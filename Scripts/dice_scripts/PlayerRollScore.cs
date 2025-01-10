using Godot;
using System;
using System.Collections.Generic;

public class PlayerRollScore
{
    public BasePlayerController Player { get; private set; }
    public List<RollScore> RollScores{ get; private set; }
    public bool isAffected { 
        get {
            return IsAffected();
    }}

    public PlayerRollScore(BasePlayerController player, RollSettings rollSettings = null){
        this.Player = player;
        RollScores = new ();
        if(rollSettings != null){
            for (int i = 0; i < rollSettings.rollChances.Length; i++)
            {
                RollScores.Add(new RollScore(rollSettings.rollChances[i].result, 0f));
            }
            
        }
    }
    private bool IsAffected(){
        for (int i = 0; i < RollScores.Count; i++)
        {
            if(RollScores[i].score != 0f){
                return true;
            }
        }
        return false;
    }
    public void SetRollScore(RollScore rollScore){
        RollScores.Find(r => r.roll == rollScore.roll).score = rollScore.score;
    }
    public float GetRollScore(int roll){
        return RollScores.Find(r => r.roll == roll).score;
    }

    public void PrintScores(){
        GD.Print(Player.PlayerName);
        for (int i = 0; i < RollScores.Count; i++)
        {
            GD.Print("roll: " + RollScores[i].roll + " - " + RollScores[i].score);
        }
    }
}