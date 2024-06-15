using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public float health = 100;

    //Animator animator;

    //Define an event to notify when the player is hit
    public event System.Action OnHit;

    public event System.Action OnDeath;

    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Player has taken damage");
        //animator.SetTrigger("Damage");

        //Notify subscribers that the player was hit
        OnHit?.Invoke();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeath?.Invoke();
        //stateMachine.ChangeState(character.death);
        //Instantiate(ragdoll, transform.position, transform.rotation);
        //Destroy(this.gameObject);
    }
}
