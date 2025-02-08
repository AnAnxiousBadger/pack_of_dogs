using Godot;
using System;
public interface IPoolable
{
	IPoolManager Pool {get; set;}
    /// <summary>
    /// Basically the effect's name. THis is how the IPoolManager finds what Pool should this IPoolable return to.
    /// </summary>
    string Identifier { get; set;}
    Node PoolableNode {get;}

}