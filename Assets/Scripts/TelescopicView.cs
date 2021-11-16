using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopicView : MonoBehaviour
{

    public float zoomLevel = 2.0f;
    public float zoomInSpeed = 100.0f;
    public float zoomOutSpeed = 100.0f;

    private float initFOV;
    public GameObject obj;

    private bool isOpening = false;
    private bool process = false;

    void Start()
    {
        //��ȡ��ǰ���������Ұ��Χ unityĬ��ֵ60
        initFOV = Camera.main.fieldOfView;
    }

    void Update()
    {
        if(obj.activeSelf)
        {
            if (Input.GetMouseButtonDown(1))
            {
                process = true;
            }


            if (process && !isOpening)
            {
                ZoomInView();
                //����ui����
                //obj.SetActive(true);
                
            }
            else if (process && isOpening)
            {
                ZoomOutView();
                //ʧ��ui����
                //obj.SetActive(false);
                
            }
        }
        
    }

    //�Ŵ����������Ұ����
    public void ZoomInView()
    {
        if (Mathf.Abs(Camera.main.fieldOfView - (initFOV / zoomLevel)) < 0f)
        {
            Camera.main.fieldOfView = initFOV / zoomLevel;
        }
        else if (Camera.main.fieldOfView - (Time.deltaTime * zoomInSpeed) >= (initFOV / zoomLevel))
        {
            Camera.main.fieldOfView -= (Time.deltaTime * zoomInSpeed);
        }
        else
        {
            isOpening = true;
            process = false;
        }
    }

    //��С���������Ұ����
    public void ZoomOutView()
    {
        if (Mathf.Abs(Camera.main.fieldOfView - initFOV) < 0f)
        {
            Camera.main.fieldOfView = initFOV;
        }
        else if (Camera.main.fieldOfView + (Time.deltaTime * zoomOutSpeed) <= initFOV)
        {
            Camera.main.fieldOfView += (Time.deltaTime * zoomOutSpeed);
        }
        else
        {
            isOpening = false;
            process = false;
        }
    }
}