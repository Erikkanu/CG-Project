using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    HealthManager target;
    [SerializeField] float damage = 40f;

    void Start()
    {
        target = FindObjectOfType<HealthManager>();
    }

    public void AttackHitEvent()
    {
        if (target == null) return;
        target.TakeDamage(damage);
        //target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }

}