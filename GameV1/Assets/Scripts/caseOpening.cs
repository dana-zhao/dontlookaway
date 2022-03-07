using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class caseOpening : MonoBehaviour
{
    Renderer render;
    private Vector3 axis, A, B;
    public GameObject channelBar;
    public short state = 0;
    private bool opening = false;
    private bool lastOpening = false;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        //StartCoroutine(updateOff());
        channelBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (state > 1) return;
        openingAnimation();
        if (channelBar.GetComponent<Slider>().value == 0 && checkItem())
        {
            channelBar.GetComponent<Slider>().value = 1f;
            channelBar.SetActive(false);
            StartCoroutine(updateOff());
            state = 1;
        }
        if (state > 0) return;
        if (lastOpening && !opening)
        {
            channelBar.SetActive(false);
            channelBar.GetComponent<Slider>().value = 1f;
        }
        lastOpening = opening;
        opening = checkItem();
        if (!opening) return;
        if (Input.GetKey(KeyCode.E))
        {
            channelBar.SetActive(true);
            channelBar.GetComponent<Slider>().value -= 0.7f * Time.deltaTime;
        }
        else
        {
            channelBar.SetActive(false);
            channelBar.GetComponent<Slider>().value = 1f;
        }
    }

    bool checkItem()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) > 5f) return false;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f, 1 << 4))
        {
            if (hit.transform.parent != null && hit.transform.parent.name == transform.parent.name) return true;
        }
        return false;
    }

    void openingAnimation()
    {
        if (state == 0) return;
        Vector3 boundPoint1 = render.bounds.min;
        Vector3 boundPoint2 = render.bounds.max;

        A = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z);
        B = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z);

        axis = A - B;
        transform.RotateAround(A, axis, 20f * Time.deltaTime);
    }

    IEnumerator updateOff()
    {
        yield return new WaitForSeconds(1.5f);
        state = 2;
    }
}
