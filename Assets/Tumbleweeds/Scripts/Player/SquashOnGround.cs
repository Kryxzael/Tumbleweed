using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class SquashOnGround : MonoBehaviour
{
    public float Time;
    public float MaxScaleXZ;
    public float MaxScaleY;

    private PlayerSpeedController _player;
    private IEnumerator _currentCoroutine;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerSpeedController>();
    }

    private void Update()
    {
        if (_currentCoroutine == null && _player.gameObject.OnGround())
        {
            _currentCoroutine = CoSquashStretch();
            StartCoroutine(_currentCoroutine);
        }
    }

    private IEnumerator CoSquashStretch()
    {
        float time = 0f;

        Vector3 originalScale = transform.localScale;

        while (time < Time)
        {
            float progress = time / Time;

            transform.localScale = new Vector3(
                Mathf.Lerp(originalScale.x, Mathf.Lerp(originalScale.x, originalScale.x * MaxScaleXZ, _player.CurrentSpeed / _player.MaxSpeed), progress),
                Mathf.Lerp(originalScale.y, Mathf.Lerp(originalScale.y, originalScale.y * MaxScaleY , _player.CurrentSpeed / _player.MaxSpeed), progress),
                Mathf.Lerp(originalScale.z, Mathf.Lerp(originalScale.z, originalScale.z * MaxScaleXZ, _player.CurrentSpeed / _player.MaxSpeed), progress)
            );

            time += UnityEngine.Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        time = 0;

        while (time < Time)
        {
            float progress = (Time - time) / Time;

            transform.localScale = new Vector3(
                Mathf.Lerp(originalScale.x, Mathf.Lerp(originalScale.x, originalScale.x * MaxScaleXZ, _player.CurrentSpeed / _player.MaxSpeed), progress),
                Mathf.Lerp(originalScale.y, Mathf.Lerp(originalScale.y, originalScale.y * MaxScaleY, _player.CurrentSpeed / _player.MaxSpeed), progress),
                Mathf.Lerp(originalScale.z, Mathf.Lerp(originalScale.z, originalScale.z * MaxScaleXZ, _player.CurrentSpeed / _player.MaxSpeed), progress)
            );

            time += UnityEngine.Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.localScale = originalScale;
        _currentCoroutine = null;
    }
}