using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CameraManager : MonoBehaviour
{
    private float _duration = 0.1f;
    private float _shakeRange = 0.06f;

    private Image _transitionImage;
    private float _transitionDuration = 1.5f;
    private float _transitionDelay = 1f;

    private bool _isTransition = false;

    private void Start()
    {
        _transitionImage = FindAnyObjectByType<TransitionImage>().GetComponent<Image>();
    }
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

    public IEnumerator ActivateTransitionImage()
    {
        _isTransition = true;

        _transitionImage.fillOrigin = 1;
        _transitionImage.fillAmount = 0;
        
        var startTime = Time.time;
        var endTime = startTime + _duration;

        while (Time.time < endTime)
        {
            var t = (Time.time - startTime) / _duration;
            _transitionImage.fillAmount = Mathf.Clamp01(t);
            yield return null;
        }

        _transitionImage.fillAmount = 1;
    }

    public IEnumerator DeactivateTransitionImage()
    {
        if (!_isTransition)
        {
            yield break;
        }
        
        yield return new WaitForSeconds(_transitionDelay);
        
        _transitionImage.fillOrigin = 0;
        _transitionImage.fillAmount = 1;

        var startTime = Time.time;
        var endTime = startTime + _duration;

        while (Time.time < endTime)
        {
            var t = (Time.time - startTime) / _duration;
            _transitionImage.fillAmount = Mathf.Clamp01(1f - t);
            yield return null;
        }
        
        _transitionImage.fillAmount = 0;
        _isTransition = false;
    }
}
