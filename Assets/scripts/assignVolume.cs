using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class assignVolume : MonoBehaviour
{
    public Slider Volume;
    public GameObject SoundManager;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager = GameObject.Find("soundManager"); 
    }

    // Update is called once per frame
    void Update()
    {
        SoundManager.GetComponent<audioScript>()._audioSource.volume = Volume.value;
    }
}
