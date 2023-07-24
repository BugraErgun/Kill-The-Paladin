using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReciever : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private float drag = 0.2f;

    private Vector3 impact;
    private Vector3 dampingVelocity;

    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && characterController.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        if (navMeshAgent != null)
        {
            if (impact.sqrMagnitude < 0.2f * 0.2f)
            {
                impact = Vector3.zero;
                navMeshAgent.enabled = true;
            }
        }
    }
    public void AddForce(Vector3 force)
    {
        impact += force;
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }

    internal void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0;
    }
}
