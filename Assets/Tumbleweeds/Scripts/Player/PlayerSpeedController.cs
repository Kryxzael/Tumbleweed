using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerSpeedController : MonoBehaviour
{
    [Header("Speed")]
    public float CurrentSpeed;
    public float MaxSpeed;
    public float MinSpeed;
    public float MaxHorizontalSpeed;
    public float HorizontalSpeedScale;

    [Header("Deceleration")]
    public float DecelerationScale;
    public AnimationCurve DecelerationCurve;

    public bool GameOver;

    /* *** */

    private Rigidbody _rigidbody;


    private const string HORIZONTAL_AXIS = "Horizontal";


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (GameOver)
            return;

        Vector3 velocity = _rigidbody.velocity;

        velocity.x = Mathf.Clamp(
            value: velocity.x + Input.GetAxis(HORIZONTAL_AXIS) * HorizontalSpeedScale * CurrentSpeed * Time.deltaTime, 
            min: -MaxHorizontalSpeed, 
            max: +MaxHorizontalSpeed
        );

        CurrentSpeed -= DecelerationCurve.Evaluate(Mathf.InverseLerp(0, MaxSpeed, CurrentSpeed)) * DecelerationScale * Time.deltaTime;

        velocity.z = CurrentSpeed;

        _rigidbody.velocity = velocity;

        if (CurrentSpeed < MinSpeed)
        {
            GameOver = true;
            CurrentSpeed = 0f;
            _rigidbody.isKinematic = true;
        }
    }

    public void Bump(float amount)
    {
        CurrentSpeed = Mathf.Clamp(CurrentSpeed + amount, 0, MaxSpeed);
    }
}
