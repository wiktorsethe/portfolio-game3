using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private float reloadTime;
    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private Transform spawnPoint;
    private Arrow currentArrow;
    private bool isReloading;

    public void Reload()
    {
        //Checking if bow isn't reloading right now
        if (isReloading || currentArrow != null) return;
        isReloading = true;

        //Start reloading
        StartCoroutine(ReloadAfterTime());
    }

    private IEnumerator ReloadAfterTime()
    {
        //Setting an arrow after the reloading time
        yield return new WaitForSeconds(reloadTime);
        currentArrow = Instantiate(arrowPrefab, spawnPoint);
        currentArrow.gameObject.SetActive(false);
        currentArrow.transform.localPosition = Vector3.zero;
        isReloading = false;
    }

    public void Fire(float firePower)
    {
        //Checking if bow isn't reloading right now
        if (isReloading || currentArrow == null) return;

        //Firing arrow
        currentArrow.gameObject.SetActive(true);
        var force = spawnPoint.TransformDirection(Vector3.forward * firePower); //Crosshair direction
        currentArrow.Fly(force);
        currentArrow = null;
        Reload();
    }
    public void ChangeWeapon()
    {
        isReloading = false;
    }
}
