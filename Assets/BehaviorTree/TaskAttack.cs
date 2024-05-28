using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttack : Node
{
   public TaskAttack(Transform transform)
    {

    }
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        Debug.Log("attacking");
        state = NodeState.RUNNING;
        return state;
    }
}
