///\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\///
///                                                                                             ///
///     File name       :   VoiceScript.cs                                                      ///
///                                                                                             ///
///     Author          :   Peter Phillips                                                      ///
///                                                                                             ///
///     Date created    :   03.10.2018                                                          ///
///                                                                                             ///
///     Last Modified   :   03.10.2018                                                          ///
///                                                                                             ///
///     Brief           :   Script which records and plays microphone input.                    ///
///                                                                                             ///
///\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\///


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MicrophoneScript : MonoBehaviour
{
    public AudioMixerGroup audioOutput;         /// This is our silent mixer to prevent audio playback.

    private const int SampleFrequency = 44100;  /// Common sampling rate for recording analog audio.

    // Start on awake so that it's set up before any other scripts that are dependent on this one.
    void Awake()
    {
        // Add an audio source Unity component to our game object.
        gameObject.AddComponent<AudioSource>();

        // Throw an error message if no microphone is detected and exit out of the script.
        if (Microphone.devices.Length == 0)
        {
            Debug.Log("No microphone detected.");
            return;
        }

        // Start recording from the microphone.
        GetComponent<AudioSource>().clip = Microphone.Start(null, false, 3600, SampleFrequency);

        // Set the audio mixer to our custom made silent mixer (this prevents audio playback).
        GetComponent<AudioSource>().outputAudioMixerGroup = audioOutput;

        // Check to make sure microphone is recording.
        if (Microphone.IsRecording(""))
        {
            // Wait until recording actually starts.
            while (Microphone.GetPosition(null) == 0);  

            // Play our audio clip (plays the microphone recording in real-time).
            GetComponent<AudioSource>().Play();
        }
        // If microphone isn't recording, throw an error message.
        else
        {
            Debug.Log("Problem with microphone: " + Microphone.devices[0]);
        }
    }
}
