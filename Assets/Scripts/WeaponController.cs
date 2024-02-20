using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Bow weapon;

    [SerializeField] private float firePowerSpeed;

    private bool fire;

    void Start()
    {
        weapon.Reload();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fire = true;
        }

        if (fire && Input.GetMouseButtonUp(0))
        {
            weapon.Fire(firePowerSpeed);
            fire = false;
        }
    }
}
