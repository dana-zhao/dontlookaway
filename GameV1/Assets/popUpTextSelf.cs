using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class popUpTextSelf : MonoBehaviour
{
    public float DestroyTime = 3f;
    public Vector3 targetSize = Vector3.one * 1.0f; // Example 10x scale
    [SerializeField]
    public TMPro.TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        //transform.localScale/=50f;
    }

    public void setText(string someText, int fontSize = 177)
    {
        text.text = someText;
        text.fontSize = fontSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
