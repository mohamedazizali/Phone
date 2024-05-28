using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class Check : Node
{
    private Transform _transform;
    private static int _enemyLayerMask = 1 << 6;

    public Check(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position,
                GuardBT.fovRange, _enemyLayerMask);

            // Loop through all detected colliders to find the one within the field of view
            foreach (Collider collider in colliders)
            {
                // Calculate the direction to the target
                Vector3 directionToTarget = collider.transform.position - _transform.position;
                directionToTarget.y = 0f; // Ignore vertical difference

                // Calculate the angle between the NPC's forward direction and the direction to the target
                float angleToTarget = Vector3.Angle(_transform.forward, directionToTarget);

                // If the angle is within the field of view angle, set the target and return success
                if (angleToTarget <= GuardBT.coneAngle / 2)
                {
                    parent.parent.SetData("target", collider.transform);
                    state = NodeState.SUCCESS;
                    return state;
                }
            }

            // No valid target found within the field of view
            state = NodeState.FAILURE;
            return state;
        }

        // Target already set
        state = NodeState.SUCCESS;
        return state;
    }
}
