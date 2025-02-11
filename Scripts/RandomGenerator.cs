using Godot;
using System;

public partial class RandomGenerator : Node
{
    public static RandomGenerator Instance { get;  private set;}
    private RandomNumberGenerator _rand = new();

    public override void _Ready(){
        Instance = this;
    }
    public int GetRandomInt(){
        return (int)_rand.Randi();
    }
    public int GetRandIntInRange(int inf, int sup){
        return _rand.RandiRange(inf, sup);
    }

    public float GetRandFInRange(float inf, float sup){
        return _rand.RandfRange(inf, sup);
    }

    public bool GetRandomBool(){
        if(_rand.RandiRange(0, 1) == 0){
            return true;
        }
        else{
            return false;
        }
    }
}