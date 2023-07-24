using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Targeter : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField]
    private List<Target> targets = new List<Target>();

    public Target currentTarget { get; private set; }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        RemoveTarget(target);
    }

    public bool SelectTarget()
    {
        if (targets.Count== 0 )
        {
            return false;
        }

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            if (!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }

            Vector2 toCenter = viewPos - new Vector2(.5f, .5f);
            if (toCenter.sqrMagnitude<closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null)
        {
            return false;
        }

        currentTarget = closestTarget;
        cinemachineTargetGroup.AddMember(currentTarget.transform,1f,2f);
        return true;
    }

    public void Cancel()
    {
        if (currentTarget == null)
        {
            return;
        }
        cinemachineTargetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if (currentTarget == target)
        {
            cinemachineTargetGroup.RemoveMember(currentTarget.transform);
            currentTarget = null;
        }
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}

