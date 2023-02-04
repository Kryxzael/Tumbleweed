using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class WalkingActor : MonoBehaviour
{
    public float MinSpeed;
    public float MaxSpeed;
    private float _speed;

    public bool MovingRight;

    private void Start()
    {
        _speed = UnityEngine.Random.Range(MinSpeed, MaxSpeed);
        transform.eulerAngles = transform.eulerAngles.SetY(MovingRight ? 90 : 270);
    }

    private void Update()
    {
        transform.TranslateZ(_speed * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.forward, 1f))
        {
            MovingRight = !MovingRight;
            transform.eulerAngles = transform.eulerAngles.SetY(MovingRight ? 90 : 270);
        }
    }
}