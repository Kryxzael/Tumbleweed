using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class CameraKeepDistance : MonoBehaviour
{
    public Transform Target;

    private Vector3 _distance;


    private void Start()
    {
        _distance = transform.position - Target.position;
    }

    private void LateUpdate()
    {
        transform.position = Target.position + _distance;
    }
}
