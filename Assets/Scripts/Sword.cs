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
        if (Physics.Raycast(cam.transform.position, transform.forward, out hit, 4f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(10);
                DamagePopupGenerator.current.CreatePopup(hit.collider.transform.position, 10.ToString());
            }
        }

        AttackCooldown();
    }
    public void ChangeWeapon()
    {
        isRelaxing = false;
    }
}
