using System.Collections.Generic;

using BehaviorTree;

public class MenoetiusBT : Tree
{
    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new TaskMove(),
            new TaskAttack(),
        });

        return root;
    }
}
