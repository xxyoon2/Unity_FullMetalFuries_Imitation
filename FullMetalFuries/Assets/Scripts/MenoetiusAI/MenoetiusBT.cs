using System.Collections.Generic;

using BehaviorTree;

public class MenoetiusBT : Tree
{
    [UnityEngine.SerializeField] private UnityEngine.GameObject _target;
    public static float attackRange = 5f;


    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new TaskMove(),
            new Selector(new List<Node>
            {
                new TaskRushing(),      // 돌진
                new TaskJump(),         // 도약
                new TaskStomp(),        // 발구르기
                new CheckTargetLocation(_target, transform),
                new TaskAnAXAttack(),   // 도끼
            }),
            //new TaskAttack(),
        });

        return root;
    }
}
