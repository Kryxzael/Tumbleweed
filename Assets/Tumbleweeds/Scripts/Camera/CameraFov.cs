using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFov : MonoBehaviour
{
    public float MaxFOV;
    public float MinFOV;

    public PlayerSpeedController Controller;

    [Range(0f, 1f)]
    public float Smoothening;

    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, Mathf.Lerp(MinFOV, MaxFOV, Controller.CurrentSpeed / Controller.MaxSpeed), Smoothening);
    }
}
