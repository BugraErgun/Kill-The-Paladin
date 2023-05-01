using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private List<Collider> allreadyCollidedWith = new List<Collider>();

    private int damage;
    private float knockBack;

    private void OnEnable()
    {
        allreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider)
        {
            return;
        }
        if (allreadyCollidedWith.Contains(other))
        {
            return;
        }

        allreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector2 direction = (other.transform.position - myCollider.transform.position).normalized * knockBack;
            forceReceiver.AddForce(direction * knockBack);
        }

    }
    public void SetAttack(int damage,float knockBack)
    {
        this.damage = damage;
        this.knockBack = knockBack;
    }
}
