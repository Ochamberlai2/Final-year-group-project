﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/LargeCarnivorSelectState")]
public class LargeCarnivorState : BehaviourBase
{
    public override Status UpdateFunc(MonsterController Monster)
    {

        if (Monster.isDead)
        {
            Monster.currentState = MonsterController.MovementState.Still;
            
            return Status.SUCCESS;
        }
        Transform pos = Monster.transform;
        float Dist = float.MaxValue;
        Transform Closest = null;
        for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
        {
            float thisDist = (BehaviourTreeManager.Colonists[i].transform.position - pos.position).magnitude;
            if (thisDist < Dist)
            {
                Dist = thisDist;
                Closest = BehaviourTreeManager.Colonists[i].transform;
            }
        }

        if (Closest == null || Dist > Monster.viewRange)//if not near enemy, then Wander
        {
            if (Time.time - Monster.lastMatingTime < Monster.matingCooldown)
            {
                Monster.currentState = MonsterController.MovementState.MakeLove;
                return Status.SUCCESS;
            }
            Monster.currentState = MonsterController.MovementState.Wander;
            return Status.SUCCESS;
        }

        if (Monster.health < Monster.maxHealth / 4)//If enemy near, but less than quarter health, Flee
        {
            Monster.currentTarget = Closest;
            Monster.currentState = MonsterController.MovementState.Flee;
            return Status.SUCCESS;
        }
        //if enemy near and health>quarterHealth, attack
        Monster.currentTarget = Closest;
        Monster.currentState = MonsterController.MovementState.Chase;
        return Status.SUCCESS;
    }

}