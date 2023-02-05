using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

//The directions in this script have been flipped

public class WalkingActor : MonoBehaviour
{
    public float MinSpeed = 0.75f;
    public float MaxSpeed = 3f;
    public float WallDetectionRange = 1f;
    private float _speed;

    public bool MovingRight;

    private void Start()
    {
        _speed = UnityEngine.Random.Range(MinSpeed, MaxSpeed);
        transform.eulerAngles = transform.eulerAngles.SetY(MovingRight ? 270 : 90);
    }

    private void Update()
    {
        transform.TranslateZ(-_speed * Time.deltaTime);

        if (Physics.Raycast(transform.position, -transform.forward, WallDetectionRange))
        {
            MovingRight = !MovingRight;
            transform.eulerAngles = transform.eulerAngles.SetY(MovingRight ? 270 : 90);
        }
    }
}