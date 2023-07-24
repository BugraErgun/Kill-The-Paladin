using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private readonly int HangingHash = Animator.StringToHash("Hanging");

    private const float CrossFadeDuration = 0.1f;

    private Vector3 ledgeForward;
    private Vector3 closesPoint;

    public PlayerHangingState(PlayerStateMachine stateMachine,Vector3 ledgeForward,Vector3 closesPoint) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
        this.closesPoint = closesPoint;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);

        stateMachine.CharacterController.enabled = false;

        stateMachine.transform.position = closesPoint - (stateMachine.LedgeDedector.transform.position - stateMachine.transform.position);

        stateMachine.CharacterController.enabled = true;

        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y > 0f)
        {
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }
        else if (stateMachine.InputReader.MovementValue.y < 0f)
        {
            stateMachine.CharacterController.Move(Vector3.zero);
            stateMachine.ForceReciever.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }   
}
