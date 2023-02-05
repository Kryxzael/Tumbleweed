using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedometerNeedle : MonoBehaviour
{
    private PlayerSpeedController _player;

    [Range(0f, 1f)]
    public float Smoothening = 0.1f;

    private void Awake()
    {
        _player = this.GetPlayer().GetComponent<PlayerSpeedController>();
    }

    private void Update()
    {
        transform.eulerAngles = transform.eulerAngles.SetZ(Mathf.Lerp(90, -90, _player.CurrentSpeed / _player.MaxSpeed));
    }
}
