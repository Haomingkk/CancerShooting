using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public GameObject ball;
    public float distance;
    public float x;
    public float y;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public float Mspeed = 10f;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        distance = (transform.position - ball.transform.position).magnitude;
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        }
        x = this.transform.eulerAngles.y;
        y = this.transform.eulerAngles.x;
    }
    public float CheckAngle(float value)
    {
        float angle = value - 180;

        if (angle > 0)
            return angle - 180;

        return angle + 180;
    }

    void LateUpdate()
    {
        if (ball)
        {
            //我的分析：1.根据垂直方向的增减量修改摄像机距离参照物的距离
            distance -= Input.GetAxis("Mouse ScrollWheel") * Mspeed;
            //根据鼠标移动修改摄像机的角度
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            if (CheckAngle(y) >= 90)
            {
                rotation = Quaternion.Euler(90, x, 0);
            }
            else if (CheckAngle(y) <= 0)
            {
                rotation = Quaternion.Euler(0, x, 0);
            }




            Vector3 position = rotation * new Vector3(0.0f, 1.0f, -distance) + ball.transform.position;

            //设置摄像机的位置与旋转
            transform.rotation = rotation;
            transform.position = position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // transform.position = distance + ball.transform.position;
    }
}
