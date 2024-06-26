using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float torque;
    [SerializeField] private Rigidbody rb;
    private bool didHit;

    public void Fly(Vector3 force)
    {
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    void OnTriggerEnter(Collider collider)
    {
        //Checking if arrow has already hit object
        if (didHit) return;
        didHit = true;
        
        //Checking if object is Enemy
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<Enemy>().TakeDamage(10);
            DamagePopupGenerator.current.CreatePopup(collider.transform.position, 10.ToString());
            Destroy(gameObject, 1f);
        }

        //Stopping arrow on object
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        transform.SetParent(collider.transform);
    }
}
