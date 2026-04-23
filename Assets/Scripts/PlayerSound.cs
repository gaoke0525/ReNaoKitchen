using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    private Player player;
    private float stepSoundRate = .13f;
    private float stepSoundTimer = 0;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        stepSoundTimer += Time.deltaTime;
        if(stepSoundTimer> stepSoundRate)
        {
            stepSoundTimer = 0;
            if (player.IsWalking)
            {
                float volume = .1f;
                SoundManager.Instance.PlayStepSound(volume);
            }
            
        }
    }
}
