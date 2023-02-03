using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototype : MonoBehaviour
{
    [SerializeField]
    float leftTrigger, rightTrigger;
    [SerializeField]
    Vector2 leftStick, rightStick;

    [SerializeField]
    Vector3 corePosition;
    [SerializeField]
    Vector3 leftHandPosition;
    [SerializeField]
    Vector3 rightHandPosition;

    [SerializeField]
    bool gripLeft, gripRight;
    
    private void Update()
    {

        leftTrigger = Input.GetAxisRaw("Trigger Left");
        rightTrigger = Input.GetAxisRaw("Trigger Right");

        leftStick.x = Input.GetAxisRaw ("Horizontal");
        leftStick.y = Input.GetAxisRaw ("Vertical");

        rightStick.x = Input.GetAxisRaw("Right Stick X");
        rightStick.y = Input.GetAxisRaw("Right Stick Y");

        gripLeft = Input.GetButton("Left Bumper");
        gripRight = Input.GetButton("Right Bumper");

        Debug.Log(rightStick);

        if (gripLeft)
        {
            leftHandPosition += (Vector3)leftStick * Time.deltaTime;
        }
        if (gripRight)
        {
            rightHandPosition += (Vector3)rightStick * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(corePosition, .5f);

        Gizmos.DrawWireSphere(leftHandPosition, .4f);
        Gizmos.DrawWireSphere(rightHandPosition, .4f);


    }
}
