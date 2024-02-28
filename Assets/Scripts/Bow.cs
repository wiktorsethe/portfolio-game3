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
        if (isReloading || currentArrow != null) return;
        isReloading = true;
        StartCoroutine(ReloadAfterTime());
    }

    private IEnumerator ReloadAfterTime()
    {
        yield return new WaitForSeconds(reloadTime);
        currentArrow = Instantiate(arrowPrefab, spawnPoint); //obj.pool
        currentArrow.gameObject.SetActive(false);
        currentArrow.transform.localPosition = Vector3.zero;
        isReloading = false;
    }

    public void Fire(float firePower)
    {
        if (isReloading || currentArrow == null) return;
        currentArrow.gameObject.SetActive(true);
        var force = spawnPoint.TransformDirection(Vector3.forward * firePower);
        currentArrow.Fly(force);
        currentArrow = null;
        Reload();
    }
    public void ChangeWeapon()
    {
        isReloading = false;
    }
}
