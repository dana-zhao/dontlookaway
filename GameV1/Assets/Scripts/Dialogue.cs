using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class Dialogue : MonoBehaviour 
{
    // Start is called before the first frame update
    private int i;
    public GameObject obj;
    void Start()
    {
        obj.SetActive(true);
        i = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if(i > 800){
            obj.SetActive(false);
        }
    
        // Debug.Log();
    }
}
