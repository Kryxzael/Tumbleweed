using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerSpeedController))]
public class PlayerBounce : MonoBehaviour
{
    public const string BOUNCE_BUTTON = "Bounce";

    public float MaxBounceForce;
    public float MaxJumpBounceForce;
    public float MinBounceForce;

    [Range(0f, 1f)]
    public float JumpBounceSpeedCap;
    public float JumpBounceSpeedMultiplier;

    [Range(0f, 1f)]
    public float MinSpeedToAllowJumpBounce;

    public float CoyoteTime;

    private Rigidbody _rigidbody;
    private PlayerSpeedController _speedController;
    private ParticleSystem _sparkleParticles;

    private float _lastTimeOnGround = float.NaN;
    private float _lastTimeOfJumpButton = float.NaN;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speedController = GetComponent<PlayerSpeedController>();
        _sparkleParticles = GetComponentInChildren<ParticleSystem>();
    }


    private void Update()
    {
        if (Input.GetButtonDown(BOUNCE_BUTTON))
            _lastTimeOfJumpButton = Time.time;

        if (this.OnGround())
        {
            _lastTimeOnGround = Time.time;
            Bounce(MaxBounceForce);

        }

        if (_lastTimeOnGround - _lastTimeOfJumpButton < CoyoteTime && _speedController.CurrentSpeed / _speedController.MaxSpeed >= MinSpeedToAllowJumpBounce)
        {
            _lastTimeOfJumpButton = float.NaN;
            _lastTimeOnGround = float.NaN;
            Bounce(MaxJumpBounceForce);

            _speedController.CurrentSpeed = Math.Min(_speedController.CurrentSpeed * JumpBounceSpeedMultiplier, _speedController.MaxHorizontalSpeed);
            _sparkleParticles.Emit(5);
        }
    }

    private void Bounce(float force)
    {
        _rigidbody.velocity = _rigidbody.velocity.SetY(Math.Max(MinBounceForce, force * _speedController.CurrentSpeed / _speedController.MaxSpeed));
    }
}
