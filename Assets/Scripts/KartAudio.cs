﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class produces audio for various states of the vehicle's movement.
/// </summary>
public class KartAudio : MonoBehaviour
{
    [Tooltip("What audio clip should play when the kart starts?")]
    public AudioSource StartSound;
    [Tooltip("What audio clip should play when the kart does nothing?")]
    public AudioSource IdleSound;
    [Tooltip("What audio clip should play when the kart moves around?")]
    public AudioSource RunningSound;
    [Tooltip("Maximum Volume the running sound will be at full speed")]
    [Range(0.1f, 1.0f)] public float RunningSoundMaxVolume = 1.0f;
    [Tooltip("Maximum Pitch the running sound will be at full speed")]
    [Range(0.1f, 2.0f)] public float RunningSoundMaxPitch = 1.0f;
    [Tooltip("What audio clip should play when the kart moves in Reverse?")]
    public AudioSource ReverseSound;
    [Tooltip("Maximum Volume the Reverse sound will be at full Reverse speed")]
    [Range(0.1f, 1.0f)] public float ReverseSoundMaxVolume = 0.5f;
    [Tooltip("Maximum Pitch the Reverse sound will be at full Reverse speed")]
    [Range(0.1f, 2.0f)] public float ReverseSoundMaxPitch = 0.6f;

    public KartController kart;
    public float soundSpeedThreshold = 40;

    void Update()
    {
        float kartSpeed = kart != null ? kart.GetSpeed()/soundSpeedThreshold : 0.0f;
        IdleSound.volume = Mathf.Lerp(0.6f, 0.0f, kartSpeed * 4);

        if (kartSpeed < 0.0f)
        {
            // In reverse
            RunningSound.volume = 0.0f;
            ReverseSound.volume = Mathf.Lerp(0.1f, ReverseSoundMaxVolume, -kartSpeed * 1.2f);
            ReverseSound.pitch = Mathf.Lerp(0.1f, ReverseSoundMaxPitch, -kartSpeed + (Mathf.Sin(Time.time) * .1f));
        }
        else
        {
            // Moving forward
            ReverseSound.volume = 0.0f;
            RunningSound.volume = Mathf.Lerp(0.1f, RunningSoundMaxVolume, kartSpeed * 1.2f);
            RunningSound.pitch = Mathf.Lerp(0.3f, RunningSoundMaxPitch, kartSpeed + (Mathf.Sin(Time.time) * .1f));
        }
    }
}