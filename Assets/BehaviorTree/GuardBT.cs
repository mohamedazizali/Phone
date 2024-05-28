using System.Collections.Generic;
using BehaviorTree;
public class GuardBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    public static float speed = 2f;
    public static float fovRange=6f;
    public static float attackRange = 2f;
    public static float coneAngle = 90f;
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            //new Sequence(new List<Node>
            //{
            //    new CheckEnemyInAttackRange(transform),
            //    new TaskAttack(transform),
            //}),
            //new Sequence(new List<Node>
            //{
            //    new Check(transform),
            //    new GoToTarget(transform),
            //}),
            new TaskPatrol(transform,waypoints),

        });
        return root;
    }
}
