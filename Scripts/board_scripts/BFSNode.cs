using Godot;
using System;

public class BFSNode
{
    private BoardNodeController _node;
    public BoardNodeController Node {
        get { return _node; }
    }
    private BFSNode _parent;
    public BFSNode Parent {
        get { return _parent; }
    }
    public BoardNodeController NodeController { 
        get { return _node; } 
    }
    private bool _isVisited;
    public bool IsVisited {
        get { return _isVisited; }
    } 
    public int Level;

    public BFSNode(BoardNodeController node, BFSNode parent){
        this._node = node;
        this._parent = parent;
        this._isVisited = true;
    }
}