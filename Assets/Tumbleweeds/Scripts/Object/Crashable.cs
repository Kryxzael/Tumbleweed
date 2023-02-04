using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Crashable : MonoBehaviour
{
    public float SpeedBoost;
    public Vector3 KnockbackForce;
    public Vector3 KnockbackTorque;

    private Collider _collider;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _collider = GetComponent<Collider>();    
        _rigidbody = GetComponent<Rigidbody>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerSpeedController player = collision.gameObject.GetComponent<PlayerSpeedController>();

        if (player)
        {
            player.Bump(SpeedBoost);
            _collider.enabled = false;
            _rigidbody.AddForce(KnockbackForce, ForceMode.Impulse);
            _rigidbody.AddTorque(KnockbackTorque);
        }
    }
}
