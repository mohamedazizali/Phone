using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    private static int _enemyLayerMask = 1 << 6;
    private Transform _transform;
    private Animator _animator;

    // Public variable to adjust field of view angle in the editor
    public float fieldOfViewAngle = 45f;

    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        Transform target = (Transform)t;

        // Calculate the direction to the target
        Vector3 directionToTarget = target.position - _transform.position;
        directionToTarget.y = 0f; // Ignore vertical difference

        // Calculate the angle between the NPC's forward direction and the direction to the target
        float angleToTarget = Vector3.Angle(_transform.forward, directionToTarget);

        // Determine the cross product to check if the target is to the left or right of the NPC
        Vector3 crossProduct = Vector3.Cross(_transform.forward, directionToTarget);

        // If the cross product's y component is negative, the target is to the left, otherwise it's to the right
        bool targetToLeft = crossProduct.y < 0;

        // If the target is behind the NPC, invert the target to left flag
        if (angleToTarget > 45f)
            targetToLeft = !targetToLeft;

        // If the angle is within the field of view angle and the target is within attack range and in front of the NPC, consider it a success
        if (angleToTarget <= fieldOfViewAngle / 2 && Vector3.Distance(_transform.position, target.position) <= GuardBT.attackRange && !targetToLeft)
        {
            _animator.SetBool("Walking", false);
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }

    // Draw the field of view in the editor
    private void OnDrawGizmos()
    {
        // Ensure the character's transform is not null
        if (_transform == null)
            return;

        Gizmos.color = Color.yellow;

        // Calculate cone direction based on the character's forward vector
        Vector3 coneDirection = _transform.forward;

        // Calculate the cone's position based on the character's position
        Vector3 conePosition = _transform.position;

        // Calculate the two points that represent the edges of the field of view
        float halfFOVAngleRad = fieldOfViewAngle * Mathf.Deg2Rad / 2;
        float coneLength = 5f; // Adjust length as needed

        Vector3 leftFOVEdge = conePosition + Quaternion.Euler(0, -halfFOVAngleRad * Mathf.Rad2Deg, 0) * coneDirection * coneLength;
        Vector3 rightFOVEdge = conePosition + Quaternion.Euler(0, halfFOVAngleRad * Mathf.Rad2Deg, 0) * coneDirection * coneLength;

        // Draw lines to represent the field of view
        Gizmos.DrawLine(conePosition, leftFOVEdge);
        Gizmos.DrawLine(conePosition, rightFOVEdge);
    }
}
