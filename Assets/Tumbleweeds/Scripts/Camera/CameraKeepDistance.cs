using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class CameraKeepDistance : MonoBehaviour
{
    public Transform Target;

    private Vector3 _distance;

    private Vector3 _offset;


    private void Start()
    {
        _distance = transform.position - Target.position;
    }

    private void LateUpdate()
    {
        transform.position = Target.position + _distance + _offset;
    }

    public void Shake(float time, float intensity)
    {
        StartCoroutine(CoShake(time, intensity));
    }

    private IEnumerator CoShake(float time, float intensity)
    {
        float currentTime = 0;

        while (currentTime < time)
        {
            _offset = new Vector3(
                UnityEngine.Random.Range(-intensity, intensity),
                UnityEngine.Random.Range(-intensity, intensity),
                UnityEngine.Random.Range(-intensity, intensity)
            );

            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _offset = default;
    }
}
