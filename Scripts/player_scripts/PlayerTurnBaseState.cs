using Godot;
using System;

public abstract class PlayerTurnBaseState
{
    protected BasePlayerController p;
    public abstract void EnterTurnState();
    public abstract void ExitTurnState();
    public abstract void ProcessTurnState(float delta);

    protected PlayerTurnBaseState(BasePlayerController p){
        this.p = p;
    }

}