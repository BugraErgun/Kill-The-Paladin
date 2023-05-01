using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField]
    public Attack[] Attacks
    {
        get; private set;
    }
    [field:SerializeField]
    public InputReader InputReader
    {
        get; private set;
    }
    [field: SerializeField]
    public CharacterController characterController
    {
        get; private set;
    }
    [field: SerializeField]
    public Animator animator
    {
        get; private set;
    }
    [field: SerializeField]
    public float FreeLookMovementSpeed
    {
        get; private set;
    }
    [field: SerializeField]
    public Transform mainCamera
    {
        get; private set;
    }
    [field: SerializeField]
    public float RotationSmoothValue
    {
        get; private set;
    }
    [field: SerializeField]
    public Targeter Targeter
    {
        get; private set;
    }
    [field: SerializeField]
    public ForceReceiver forceReceiver
    {
        get; private set;
    }
    [field: SerializeField]
    public float TargetingMovementSpeed
    {
        get; private set;
    }
    [field: SerializeField]
    public WeaponDamage Weapon
    {
        get; private set;
    }



    private void Start()
    {
        mainCamera = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }

}
