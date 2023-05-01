using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        enemyStateMachine.animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange())
        {
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            return;
        }
        else if (IsInAttackRange())
        {
            enemyStateMachine.SwitchState(new EnemyAttackingState(enemyStateMachine));
            return;
        }

        MoveToPlayer(deltaTime);

        FacePlayer();

        enemyStateMachine.animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        enemyStateMachine.navMeshAgent.ResetPath();
        enemyStateMachine.navMeshAgent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        enemyStateMachine.navMeshAgent.destination = enemyStateMachine.Player.transform.position;

        Move(enemyStateMachine.navMeshAgent.desiredVelocity.normalized * enemyStateMachine.MovementSpeed, deltaTime);

        enemyStateMachine.navMeshAgent.velocity = enemyStateMachine.characterController.velocity;
    }

    private bool IsInAttackRange()
    {
        float playerDistanceSqr = (enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= enemyStateMachine.AttackRange * enemyStateMachine.AttackRange;
    }
}


