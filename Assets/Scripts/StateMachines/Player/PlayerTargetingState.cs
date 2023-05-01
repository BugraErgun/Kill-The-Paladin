using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    private readonly int TargetingBlendTree = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    private const float CrossFadeDuraion = 0.1f;

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.animator.CrossFadeInFixedTime(TargetingBlendTree,CrossFadeDuraion);
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        UpdateAnimator(deltaTime);

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

        FaceTarget();
    }
    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }
    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.movementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.movementValue.y;

        return movement;
    }
    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.movementValue.y == 0)
        {
            stateMachine.animator.SetFloat(TargetingForwardHash, 0,.1f,deltaTime);

        }
        else
        {
            float value = stateMachine.InputReader.movementValue.y > 0 ? 1f : -1f;
            stateMachine.animator.SetFloat(TargetingForwardHash, value, .1f, deltaTime);
        }
        if (stateMachine.InputReader.movementValue.x == 0)
        {
            stateMachine.animator.SetFloat(TargetingRightHash, 0, .1f, deltaTime);

        }
        else
        {
            float value = stateMachine.InputReader.movementValue.x > 0 ? 1f : -1f;
            stateMachine.animator.SetFloat(TargetingRightHash, value, .1f, deltaTime);
        }
    }
}
