using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutTextManager : MonoBehaviour
{
    public GameObject tutText;
    public GameStatus gameStatus;
    public GameObject[] counters;
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
        if (shown2) return;

        Debug.Log(counters.Length);

        foreach (GameObject counter in counters)
        {
            if (Vector3.Distance(Camera.main.transform.position, counter.transform.position) < 5f)
            {
                tutText.GetComponent<tutTextController>().setText("Press space to jump");
                tutText.SetActive(true);
                StartCoroutine(updateOff());
                shown2 = true;
            }
        }

    }

    IEnumerator updateOff()
    {
        yield return new WaitForSeconds(3.7f);
        tutText.SetActive(false); ;
    }
}
