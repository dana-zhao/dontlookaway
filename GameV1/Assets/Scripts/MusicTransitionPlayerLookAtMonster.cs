using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTransitionPlayerLookAtMonster : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mainmixer;
    [SerializeField]
    private AudioSource mainloop;
    [SerializeField]
    private AudioSource chatterloop;
    [SerializeField]
    private AudioSource lowbeatloop;
    [SerializeField]
    private AudioMixerSnapshot mainloopsnapshot;
    [SerializeField]
    private AudioMixerSnapshot lowbeatsnapshot;
    [SerializeField]
    private AudioMixerSnapshot chatterloopsnapshot;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
