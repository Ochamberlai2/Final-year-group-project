using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "BehaviourTree/Leaf nodes/Colonist/Generic/CheckActive")]
public class CheckActive : BehaviourBase{

    public override Status UpdateFunc(ColonistController Colonist)
    {
       
            return Status.FAILURE;
        
    }
}
