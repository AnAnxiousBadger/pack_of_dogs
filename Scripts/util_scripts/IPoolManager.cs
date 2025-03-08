using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
public interface IPoolManager {
    PoolableSettings[] PoolableScenes { get; set;}
    Dictionary<string, Queue<IPoolable>> PoolablesDict { get; set;}

    public void SpawnPoolables(Node3D poolablesParent){
        PoolablesDict = new();

        foreach (PoolableSettings poolSet in PoolableScenes)
        {
            Queue<IPoolable> poolQueue = new();

            for (int i = 0; i < poolSet.poolCount; i++)
            {
                IPoolable poolable = poolSet.scene.Instantiate() as IPoolable;
                poolable.Pool = this;
                poolable.Identifier = poolSet.identifier;
                poolQueue.Enqueue(poolable);
                poolablesParent.AddChild(poolable.PoolableNode);
            }
            

            PoolablesDict.Add(poolSet.identifier, poolQueue);
        }
    }

    public IPoolable GetPoolable(string identifier, Vector3 pos){
        IPoolable poolable = PoolablesDict[identifier].Dequeue();
        DoOnPoolingAction(poolable, pos);
        return poolable;
    }
    public IPoolable GetPoolable(string identifier, Vector3 pos, Vector3 rot){
        IPoolable poolable = PoolablesDict[identifier].Dequeue();
        DoOnPoolingAction(poolable, pos, rot);
        return poolable; 
    }
    IPoolable DoOnPoolingAction(IPoolable poolable, Vector3 pos);
    IPoolable DoOnPoolingAction(IPoolable poolable, Vector3 pos, Vector3 rot);

    public void ReQueuePoolable(IPoolable poolable){
        DoOnReQueueToPoolAction(poolable);
        PoolablesDict[poolable.Identifier].Enqueue(poolable);
    }
    void DoOnReQueueToPoolAction(IPoolable poolable);
}