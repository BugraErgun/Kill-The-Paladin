using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");

    private const float CrossFadeDuration = 0.1f;

    private Vector3 momentum;

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ForceReciever.Jump(stateMachine.JumpForce);

        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);

        stateMachine.LedgeDedector.OnLedgeDetect += HandleLedgeDetect;
    }

   

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.CharacterController.velocity.y <= 0f)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.LedgeDedector.OnLedgeDetect -= HandleLedgeDetect;
    }
    
    private void HandleLedgeDetect(Vector3 ledgeForward , Vector3 closesPoint)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine,ledgeForward,closesPoint));
    }
}