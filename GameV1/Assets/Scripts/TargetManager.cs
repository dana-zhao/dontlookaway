using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TargetManager : MonoBehaviour
{
    public GameObject target;
    public Camera cam;
    public GameObject player;
    [SerializeField]
    private AudioMixer mainmixer;
    [SerializeField]
    private AudioMixerSnapshot mainloopsnapshot;
    [SerializeField]
    private AudioMixerSnapshot lowbeatsnapshot;
    [SerializeField]
    private AudioMixerSnapshot chatterloopsnapshot;


    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        if (GeometryUtility.TestPlanesAABB(planes, target.GetComponent<Collider>().bounds))
            return true;
        return false;
    }

    private bool IsVisible2(GameObject target)
    {
        foreach (Transform transform in target.transform)
        {
            
            RaycastHit hit;
            var rayDirection = player.transform.position - transform.position;
            if (Physics.Raycast(transform.position, rayDirection, out hit, Mathf.Infinity))
            {
                if (hit.transform == player.transform)
                {
                    return true;
                }
                
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ghost = target.GetComponent<ghostMovement>();
        ghost.isSeen = false;
        ghost.isFacing = false;
        if (IsVisible(cam, target))
        {
            chatterloopsnapshot.TransitionTo(0.1f);
            ghost.isFacing = true;
            if (IsVisible2(target))
                ghost.isSeen = true;
        }
        if (ghost.isSeen == false)
        {
            mainloopsnapshot.TransitionTo(1f);
        }
    }
}
