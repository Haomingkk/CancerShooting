using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    // ����Ŀ���transform(��δ������ã���Ĭ��Ϊ��ǰ�����transform)
    public Transform camTransform;

    //����������ʱ��
    public float shake = 0f;
    private float currentShake;

    // �������ȣ����������//���Խ�󶶶�Խ����
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