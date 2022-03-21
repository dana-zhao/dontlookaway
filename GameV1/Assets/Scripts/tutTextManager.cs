using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutTextManager : MonoBehaviour
{
    public GameObject tutText;
    public GameStatus gameStatus;
    public GameObject cashier;
    private bool shown1 = false;
    private bool shown2 = false;
    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameObject.FindObjectOfType<GameStatus>();
        tutText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shown1 && gameStatus.currentInventory == 1)
        {
            tutText.GetComponent<tutTextController>().setText("press i to open inventory");
            tutText.SetActive(true);
            StartCoroutine(updateOff());
            shown1 = true;
        }

        if (shown2) return;
        if (Vector3.Distance(Camera.main.transform.position, cashier.transform.position) < 5f)
        {
            tutText.GetComponent<tutTextController>().setText("press space to jump");
            tutText.SetActive(true);
            StartCoroutine(updateOff());
            shown2 = true;
        }
    }

    IEnumerator updateOff()
    {
        yield return new WaitForSeconds(3.7f);
        tutText.SetActive(false); ;
    }
}
