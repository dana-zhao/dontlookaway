using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutTextController : MonoBehaviour
{
    public float destroyTime = 3.7f;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, destroyTime);
    }

    public void setText(string someText)
    {
        GetComponent<UnityEngine.UI.Text>().text = someText;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
