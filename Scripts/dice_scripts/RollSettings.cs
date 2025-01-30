using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class RollSettings : Resource
{
    [Export] public RollChance[] rollChances;

    public List<int> GetRollables(){
        List<int> rollables = new();
        for (int i = 0; i < rollChances.Length; i++)
        {
            if(rollChances[i].weight != 0){
                rollables.Add(rollChances[i].result);
            }
        }
        return rollables;
    }
    public int GetTotalWeight(){
        int totalWeight = 0;
        for (int i = 0; i < rollChances.Length; i++)
        {
            totalWeight += rollChances[i].weight;
        }
        return totalWeight;
    }

    public Dictionary<int, int> GetRollChanceDict(){
        Dictionary<int, int> rollChanceDict = new ();
        for (int i = 0; i < rollChances.Length; i++)
        {
            rollChanceDict.Add(rollChances[i].result, rollChances[i].weight);
        }
        return rollChanceDict;
    }
    public Dictionary<int, float> GetRollChanceDictWithChances(){
        Dictionary<int, float> rollChanceDictWithChances = new ();
        int totalWeight = GetTotalWeight();
        for (int i = 0; i < rollChances.Length; i++)
        {
            rollChanceDictWithChances.Add(rollChances[i].result, (float)rollChances[i].weight / totalWeight);
        }
        return rollChanceDictWithChances;
    }

    public float GetAverageRoll(){
        int totalWeights = GetTotalWeight();
        int a = 0;
        foreach (RollChance rollChance in rollChances)
        {
            a += rollChance.weight * rollChance.result;
        }

        return a / totalWeights;
    }

}