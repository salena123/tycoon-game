using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeToogle : MonoBehaviour
{
    public AudioSource audioSource;
      
    public void OnClick()
    {
        audioSource.mute = !audioSource.mute;
    }
}
