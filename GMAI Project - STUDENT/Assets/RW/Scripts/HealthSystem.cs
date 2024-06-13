using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float health = 100;
    //[SerializeField] GameObject hitVFX;
    //[SerializeField] GameObject ragdoll;

    Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("Damage");
        //CameraShake.Instance.ShakeCamera(2f, 0.2f);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
    //public void HitVFX(Vector3 hitPosition)
    //{
    //    GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
    //    Destroy(hit, 3f);

    //}
}
