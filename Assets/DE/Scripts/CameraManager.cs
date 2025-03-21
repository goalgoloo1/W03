using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraManager : MonoBehaviour
{
    private float _duration = 0.1f;
    private float _shakeRange = 0.06f;
    
    private float MakeDamageFXMagnitude()
    {
        return Random.Range(-_shakeRange, _shakeRange);
    }

    public IEnumerator CoCameraShake()
    {
        var startTime = Time.time;
        while (Time.time - startTime < _duration)
        {
            transform.position += new Vector3(MakeDamageFXMagnitude(), MakeDamageFXMagnitude(), 0);
            yield return null;
        }
        
        transform.position = Vector3.forward * -10f;
    }
}
