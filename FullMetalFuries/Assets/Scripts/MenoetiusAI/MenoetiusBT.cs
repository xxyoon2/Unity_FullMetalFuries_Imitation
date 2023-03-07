using System.Collections.Generic;

using BehaviorTree;

public class MenoetiusBT : Tree
{
    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new TaskMove(),
            new Selector(new List<Node>
            {
                new TaskAnAXAttack(),   // 도끼
                new TaskRushing(),      // 돌진
                new TaskJump(),         // 도약
                new TaskStomp(),        // 발구르기
            }),
            //new TaskAttack(),
        });

        return root;
    }
}
