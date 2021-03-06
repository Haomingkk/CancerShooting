using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    // 抖动目标的transform(若未添加引用，怎默认为当前物体的transform)
    public Transform camTransform;

    //持续抖动的时长
    public float shake = 0f;
    private float currentShake;

    // 抖动幅度（振幅）　　//振幅越大抖动越厉害
    public float shakeAmount = 0.7f;
    private float currentShakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        currentShake = shake;
        currentShakeAmount = shakeAmount;
    }


    void LateUpdate()
    {
        if (currentShake > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            currentShake -= Time.deltaTime * decreaseFactor;
            Mathf.MoveTowards(currentShakeAmount, 0f, currentShake * Time.deltaTime);
        }
        else
        {
            currentShake = shake;
            camTransform.localPosition = originalPos;
            gameObject.GetComponent<CameraShake>().enabled = false;
        }
    }
    
}