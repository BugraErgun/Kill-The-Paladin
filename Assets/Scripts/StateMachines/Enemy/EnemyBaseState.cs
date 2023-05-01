using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine enemyStateMachine;

    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
    }
    protected bool IsInChaseRange()
    {
        float playerDistanceSqr = (enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= enemyStateMachine.PlayerChasingRange * enemyStateMachine.PlayerChasingRange;    
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }
    protected void Move(Vector3 motion,float deltaTime)
    {
        enemyStateMachine.characterController.Move(
            (motion + enemyStateMachine.forceReceiver.Movement) * deltaTime);
    }
    protected void FacePlayer()
    {
        if (enemyStateMachine.Player == null)
            return;
        Vector3 lookPosition = enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position;
        lookPosition.y = 0;

        enemyStateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }
}
