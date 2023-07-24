using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");

    private readonly int DodgeForward = Animator.StringToHash("DodgeForward");
    private readonly int DodgeRight = Animator.StringToHash("DodgeRight");

    private Vector3 dodgingDirectionInput;

    private float remainingDodgeTime;

    private const float CrossFadeDuration = 0.1f;

    public PlayerDodgeState(PlayerStateMachine stateMachine,Vector3 dodgingDirection) : base(stateMachine)
    {
        this.dodgingDirectionInput = dodgingDirection;
    }

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeDuration;

        stateMachine.Animator.SetFloat(DodgeForward, dodgingDirectionInput.y);
        stateMachine.Animator.SetFloat(DodgeRight, dodgingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossFadeDuration);

        stateMachine.Health.SetBlocking(true);           
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

        Move(movement, deltaTime);
        FaceTarget();

        remainingDodgeTime -= deltaTime;

        if (remainingDodgeTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetBlocking(false);
    }
}
