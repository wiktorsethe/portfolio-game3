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
        if (didHit) return;
        didHit = true;
        
        if (collider.tag == "Enemy")
        {
            Debug.Log("HIT!");
            collider.GetComponent<Enemy>().TakeDamage(10);
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        transform.SetParent(collider.transform);
    }
}
