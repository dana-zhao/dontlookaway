using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void OnMouseOver()
    {

        if (Input.GetKeyDown(KeyCode.E)){
            this.gameObject.SetActive(false);
        }
        
        
    }
}
