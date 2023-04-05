using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType // 음식들이 플레이어에게 주는 영향 구분 Good = 0, Bad = 1
{
    Dog,
    Cat,
}

public class MeowAndBarkInteraction : MonoBehaviour
{
    public SoundType soundType;
    private AudioSource audioSource;
    public AudioClip meow;
    public AudioClip bark;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        switch (soundType)
        {
            case SoundType.Dog:
                {
                    audioSource.clip = bark;
                    break;
                }
            case SoundType.Cat:
                {
                    audioSource.clip = meow;
                    break;
                }
            default:
                break;
        }
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            audioSource.PlayDelayed(2.0f);
        }
    }
}
