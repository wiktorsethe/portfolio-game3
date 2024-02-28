using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopupGenerator : MonoBehaviour
{
    public static DamagePopupGenerator current;
    public GameObject prefab;
    private Camera cam;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        cam = Camera.main;
    }
    public void CreatePopup(Vector3 position, string text)
    {
        var popup = Instantiate(prefab, position, Quaternion.identity);
        popup.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
        popup.transform.forward = cam.transform.forward;
        Destroy(popup, 1f);
    }
}
