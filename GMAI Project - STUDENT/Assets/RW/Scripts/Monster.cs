using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] public float health = 3;
    //[SerializeField] GameObject hitVFX;
    //[SerializeField] GameObject ragdoll;

    [Header("Combat")]
    [SerializeField] public float attackCD = 3f;
    [SerializeField] public float attackRange = 1f;
    [SerializeField] public float aggroRange = 4f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
