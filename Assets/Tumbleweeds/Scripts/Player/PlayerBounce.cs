using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerSpeedController), typeof(AudioSource))]
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
    public float MinSpeedForWhooshSound;

    public float CoyoteTime;
    public float SpamPunishTime;

    private Rigidbody _rigidbody;
    private PlayerSpeedController _speedController;
    private ParticleSystem _sparkleParticles;
    private AudioSource _audioSource;

    private float _lastTimeOnGround = float.NaN;
    private float _lastTimeOfJumpButton = float.NaN;
    private float _jumpButtonCooldown = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speedController = GetComponent<PlayerSpeedController>();
        _sparkleParticles = GetComponentInChildren<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Input.GetButtonDown(BOUNCE_BUTTON))
        {
            if (_jumpButtonCooldown <= 0)
                _lastTimeOfJumpButton = Time.time;

            _jumpButtonCooldown = SpamPunishTime;
        }

        if (this.OnGround())
        {
            _lastTimeOnGround = Time.time;
            Bounce(MaxBounceForce, false);

        }

        if (Math.Abs(_lastTimeOnGround - _lastTimeOfJumpButton) < CoyoteTime && _speedController.CurrentSpeed / _speedController.MaxSpeed >= MinSpeedToAllowJumpBounce)
        {
            _lastTimeOfJumpButton = float.NaN;
            _lastTimeOnGround = float.NaN;
            Bounce(MaxJumpBounceForce, true);

            _speedController.CurrentSpeed = Math.Min(_speedController.CurrentSpeed * JumpBounceSpeedMultiplier, _speedController.MaxSpeed);
            _sparkleParticles.Emit(20);
        }

        if (_jumpButtonCooldown > 0)
            _jumpButtonCooldown -= Time.deltaTime;
    }

    private void Bounce(float force, bool goodBounce)
    {
        _rigidbody.velocity = _rigidbody.velocity.SetY(Math.Max(MinBounceForce, force * _speedController.CurrentSpeed / _speedController.MaxSpeed));

        if (_speedController.CurrentSpeed / _speedController.MaxSpeed >= MinSpeedForWhooshSound)
        {
            if (goodBounce)
                _audioSource.pitch = 2f;
            else
                _audioSource.pitch = Mathf.Lerp(0.7f, 1.6f, _speedController.CurrentSpeed / _speedController.MaxSpeed);

            _audioSource.Play();
        }

    }
}
