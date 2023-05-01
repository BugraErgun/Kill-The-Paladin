using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTree = Animator.StringToHash("FreeLookBlendTree");

    private const float AnimatorDampTime = 0.1f;

    private const float CrossFadeDuraion = 0.1f;

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.animator.CrossFadeInFixedTime(FreeLookBlendTree,CrossFadeDuraion);
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        if (stateMachine.InputReader.movementValue == Vector2.zero)
        {
            stateMachine.animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        stateMachine.animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement,deltaTime);
    }
    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }
    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.mainCamera.transform.forward;
        Vector3 right = stateMachine.mainCamera.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.movementValue.y +
            right * stateMachine.InputReader.movementValue.x;
    }
    private void FaceMovementDirection(Vector3 movement,float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,
            Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationSmoothValue);
    }
    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget())
        {
            return;
        }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

}
