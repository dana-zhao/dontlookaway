using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverReturn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text;
    private bool released;
    public GameObject dialogue;
    void Start()
    {
        dialogue.SetActive(false);
        text.SetActive(false);
        released = false;
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        
        if (released){
            
        }
        else{
            text.SetActive(true);
        }
    }
    void OnMouseExit()
    {
        if (released){
            
        }
        else{
            text.SetActive(false);
        }
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            released = true;
            text.SetActive(false);
        }
        if (released){
            dialogue.SetActive(true);
        }
    }
}
