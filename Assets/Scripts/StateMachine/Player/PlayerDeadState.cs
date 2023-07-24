using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    private readonly int DieHash = Animator.StringToHash("Die");

    private const float TransitionDuration = 0.1f;

    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DieHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}
