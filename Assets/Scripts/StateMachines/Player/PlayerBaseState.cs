using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(float deltTime)
    {
        Move(Vector3.zero, deltTime);
    }
    protected void Move(Vector3 motion, float deltTime)
    {
        stateMachine.characterController.Move(
            (motion + stateMachine.forceReceiver.Movement) * deltTime);
    }
    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null)
            return;
        Vector3 lookPosition = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPosition.y = 0;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }
}
