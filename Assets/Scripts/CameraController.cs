using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (PlayerController.instance.isFirstPerspective == true)
        {
            transform.position = target.position;
        }
        else
        {
            transform.position = target.position - target.transform.forward * 6f + new Vector3(0, 2f, 0);
        }
        
        transform.rotation = target.rotation;
    }
}
