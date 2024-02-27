using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    private bool isRelaxing;
    public void AttackCooldown()
    {
        if (isRelaxing) return;
        isRelaxing = true;
        StartCoroutine(AttackAfterTime());
    }
    private IEnumerator AttackAfterTime()
    {
        yield return new WaitForSeconds(cooldownTime);
        isRelaxing = false;
    }
    public void Sweep(float attackPower)
    {
        if (isRelaxing) return;
        GetComponent<Animator>().SetTrigger("Attack");


        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("HIT!");
                hit.collider.GetComponent<Enemy>().TakeDamage(10);
                //hit.collider.GetComponent<EnemyController>().TakeDamage(attackDamage);
            }
        }

        AttackCooldown();
    }
    public void ChangeWeapon()
    {
        isRelaxing = false;
    }
}
