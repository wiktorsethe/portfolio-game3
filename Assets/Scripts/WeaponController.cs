using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Bow bow;
    [SerializeField] private Sword sword;

    [SerializeField] private float firePowerSpeed;
    [SerializeField] private float attackPower;

    private bool attack;

    void Start()
    {
        bow.Reload();
        //sword.AttackCooldown();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack = true;
        }

        if (attack && Input.GetMouseButtonUp(0))
        {
            bow.Fire(firePowerSpeed);
            //sword.Sweep(attackPower);
            attack = false;
        }
    }
}
