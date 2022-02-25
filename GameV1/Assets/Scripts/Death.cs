using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.SceneManagement;  
public class Death : MonoBehaviour 
{   
    public GameObject ghost; 
    public GameObject Player;     
    // Start is called before the first frame update
    public GameStatus gameStatus;

    private Vector3  playerinitial;
    private Vector3  ghostinitial;


    void Start()     
    {         
        gameStatus = GameObject.FindObjectOfType<GameStatus>();
        // myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/Scenes");         
        // scenePaths = myLoadedAssetBundle.GetAllScenePaths();

        playerinitial = Player.transform.position;
        ghostinitial = ghost.transform.position;
    }       
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {   
        
        if (collision.gameObject.name == "Player")
        {   

            collision.gameObject.transform.position  = playerinitial;
            ghost.transform.position = ghostinitial;  
            
            gameStatus.dead();
            
            if (gameStatus.currentLife == 0) {
                SceneManager.LoadScene("Death"); 
            }
        }     
    }      
    
    void Update()     
    {         
        
        if (Vector3.Distance(Player.transform.position, ghost.transform.position) < 2.5f)         
        {           
            Player.transform.position = playerinitial;
            ghost.transform.position = ghostinitial;  

            gameStatus.dead();
            
            if (gameStatus.currentLife == 0) {
                SceneManager.LoadScene("Death"); 
            }  
        }     
    } 
}