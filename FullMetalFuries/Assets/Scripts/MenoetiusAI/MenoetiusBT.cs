using System.Collections.Generic;

using BehaviorTree;

public class MenoetiusBT : Tree
{
    [UnityEngine.SerializeField] private float _attackRange = 5f;
    public float attackRange
    {
        get { return _attackRange; }
        set { attackRange = _attackRange; }
    }


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
                new CheckTargetLocation(transform),
                new TaskAnAXAttack(),   // 도끼
            }),
        });

        return root;
    }
}
