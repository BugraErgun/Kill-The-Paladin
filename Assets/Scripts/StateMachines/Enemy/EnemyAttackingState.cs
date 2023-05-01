using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");

    private const float TransitionDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        enemyStateMachine.Weapon.SetAttack(enemyStateMachine.AttackDamage,enemyStateMachine.AttackKnockback);

        enemyStateMachine.animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(enemyStateMachine.animator) >= 1)
        {
            enemyStateMachine.SwitchState(new EnemyChasingState(enemyStateMachine));
        }
    }

    public override void Exit() { }

}
