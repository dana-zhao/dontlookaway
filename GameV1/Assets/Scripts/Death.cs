using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.SceneManagement;  
public class Death : MonoBehaviour 
{ public GameObject ghost; public GameObject Player;     
    // Start is called before the first frame update
    void Start()     
    {         
        // myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/Scenes");         
        // scenePaths = myLoadedAssetBundle.GetAllScenePaths();
    }       
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {         
        if (collision.gameObject.name == "Player")
        {              
            SceneManager.LoadScene("Death");         
        }     
    }      
    
    void Update()     
    {         
        if (Vector3.Distance(Player.transform.position, ghost.transform.position) < 2.5f)         
        {             
            SceneManager.LoadScene("Death");         
        }     
    } 
}