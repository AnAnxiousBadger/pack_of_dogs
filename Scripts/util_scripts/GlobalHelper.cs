using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GlobalHelper : Node
{
    public static GlobalHelper Instance { get; private set;}
    private GameController _gameController;
    public GameController GameController {
        get { return _gameController; }
        set { _gameController = value;}
    }

    public List<string> RandomNames { get; private set; }
    public List<string> RandomQuotes { get; private set; }
    public Dictionary<Fate.Luck, List<Fate>> RandomFates { get; private set; }
    public override void _EnterTree()
    {
        if(Instance != null){
            this.QueueFree();
            return;
        }
        Instance = this;
        GameController = null;
    }

    public override void _Ready()
    {
        // Load and store from random text JSON
        RandomNames = new();
        RandomNames = JSONLoader.Instance.DeserializeJSONElement<List<string>>("random_texts", "names");

        RandomQuotes = new();
        RandomQuotes = JSONLoader.Instance.DeserializeJSONElement<List<string>>("random_texts", "quotes");

        List<Fate> fates = new();
        fates = JSONLoader.Instance.DeserializeJSONElement<List<Fate>>("random_texts", "fates");
        RandomFates = fates.GroupBy(fate => fate.LuckScoreRange).ToDictionary(group => group.Key, group => group.ToList());
    }
}