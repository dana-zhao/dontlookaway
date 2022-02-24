using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUpText : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public float DestroyTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
    }

    void ShowFloatingText()
    {
        if (!Input.GetKeyUp(KeyCode.E)) return;
        Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        ShowFloatingText();
    }
}
