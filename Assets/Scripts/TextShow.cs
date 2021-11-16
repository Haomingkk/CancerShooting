using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShowText(false);
    }
    public void ChangeText(string text)
    {
        this.GetComponent<Text>().text = text;
    }

    public void ShowText(bool show)
    {
        this.GetComponent<Text>().enabled = show;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
