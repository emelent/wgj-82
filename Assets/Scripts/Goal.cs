using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : CollectableItem
{
    public override void ActivateEffect()
    {
        print("Goal reached");
        GM.instance.GoToNextLevel();
    }
}
