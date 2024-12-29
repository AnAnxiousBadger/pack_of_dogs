using Godot;
using System;

[GlobalClass]
public partial class RollChance : Resource
{
    [Export] public int result;
    [Export] public int weight;

    public RollChance(){
        this.result = int.MinValue;
        this.weight = int.MinValue;
    }

    public RollChance(int rollResult, int rollChance){
        this.result = rollResult;
        this.weight = rollChance;
    }
}