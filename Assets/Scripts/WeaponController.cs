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

    [Range(0,1)]
    public int selectedWeapon;
    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack = true;
        }

        if (attack && Input.GetMouseButtonUp(0))
        {
            if(selectedWeapon == 0) bow.Fire(firePowerSpeed);
            else if(selectedWeapon == 1) sword.Sweep(attackPower);
            attack = false;
        }

        int previousSelectedWeapon = selectedWeapon;
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= 1) selectedWeapon = 0;
            else selectedWeapon++;
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0) selectedWeapon = 1;
            else selectedWeapon--;
        }

        if(previousSelectedWeapon != selectedWeapon) SelectWeapon();
    }
    private void SelectWeapon()
    {
        if (selectedWeapon == 0)
        {
            sword.ChangeWeapon();
            bow.gameObject.SetActive(true);
            sword.gameObject.SetActive(false);
            bow.Reload();
        }
        else
        {
            bow.ChangeWeapon();
            bow.gameObject.SetActive(false);
            sword.gameObject.SetActive(true);
            sword.AttackCooldown();
        }
    }
}
