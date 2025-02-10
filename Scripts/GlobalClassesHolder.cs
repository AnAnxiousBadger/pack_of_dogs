using Godot;
using System;

public partial class GlobalClassesHolder : Node
{
    public static GlobalClassesHolder Instance { get; private set;}
    private GameController _gameController;
    public GameController GameController {
        get { return _gameController; }
        set { _gameController = value;}
    }
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
        
        //GD.Print("lenull");
    }
}