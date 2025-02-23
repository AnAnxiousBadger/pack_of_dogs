using Godot;
using System;
public interface IPoolable
{
	IPoolManager Pool {get; set;}
    /// <summary>
    /// Basically the effect's name. This is how the <c>IPoolManager</c> finds what Pool should this <c>IPoolable</c> return to.
    /// </summary>
    string Identifier { get; set;}
    Node PoolableNode {get;}

}