using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Crashable : MonoBehaviour
{
    public float SpeedBoost = 5f;
    private Vector3 KnockbackForce = new Vector3(0, 250, 500);
    private Vector3 KnockbackTorque = new Vector3(0, 200, 500);

    private Collider _collider;
    private Rigidbody _rigidbody;
    private CameraKeepDistance _cam;

    private void Awake()
    {
        _collider = GetComponent<Collider>();    
        _rigidbody = GetComponent<Rigidbody>();
        _cam = Camera.main.GetComponent<CameraKeepDistance>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerSpeedController player = collision.gameObject.GetComponent<PlayerSpeedController>();

        if (player)
        {
            player.Bump(SpeedBoost);

            _rigidbody.constraints = RigidbodyConstraints.None;
            _collider.enabled = false;

            _rigidbody.AddForce(KnockbackForce, ForceMode.VelocityChange);
            _rigidbody.AddTorque(KnockbackTorque);
            _cam.Shake(0.25f, 0.1f);
        }
    }
}
