using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    private bool isRelaxing;
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    public void AttackCooldown()
    {
        //Checking if player can attack
        if (isRelaxing) return;
        isRelaxing = true;

        //Waiting for attack
        StartCoroutine(AttackAfterTime());
    }
    private IEnumerator AttackAfterTime()
    {
        yield return new WaitForSeconds(cooldownTime);
        isRelaxing = false;
    }
    public void Sweep(float attackPower)
    {
        //Checking if player can attack
        if (isRelaxing) return;
        GetComponent<Animator>().SetTrigger("Attack");

        //Checking if Enemy is in sword range
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, transform.forward, out hit, 4f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(10);
                DamagePopupGenerator.current.CreatePopup(hit.collider.transform.position, 10.ToString());
            }
        }

        //Setting up a cooldown
        AttackCooldown();
    }
    public void ChangeWeapon()
    {
        isRelaxing = false;
    }
}
