using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimal : MonoBehaviour
{
    Vector3 turnAnimal;

    // Update is called once per frame
    void Update()
    {
        turnAnimal = new Vector3(0f, 45f, 0f);
        transform.Rotate(turnAnimal * Time.deltaTime);
    }
}
