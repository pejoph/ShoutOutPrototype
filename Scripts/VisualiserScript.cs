///\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\///
///                                                                                             ///
///     File name       :   VisualiserScript.cs                                                 ///
///                                                                                             ///
///     Author          :   Peter Phillips                                                      ///
///                                                                                             ///
///     Date created    :   03.10.2018                                                          ///
///                                                                                             ///
///     Last Modified   :   03.10.2018                                                          ///
///                                                                                             ///
///     Brief           :   Script to control the colour and size of the audio visualiser.      ///
///                                                                                             ///
///\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\///


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualiserScript : MonoBehaviour
{
    public Transform[] audioVisualiserBars;     /// The array for our audio visualiser bars.
    public Transform[] audioVisualiserShadow;   /// The array for our audio visualiser shadow.
    public float sizeMultiplier = 100000;       /// Determines the amount by which the audio visualiser bars expand based on input.
        // Samples = # of bands * 2^8.
    public int numberOfSamples = 4096;          /// Number of audio samples taken (determines frequency range for each frequency band).
    public FFTWindow fftWindow;                 /// Different window options reduce signal leakage across frequency bands by different methods.
    public float stepSize = .2f;                /// The amount by which the lerp function will move what it's lerping (lower values give smoother/slower motion).
                                                  
    private float[] spectrumData;               /// An array for our audio spectrum data;
    private float previousScale = 0f;           /// Used to compare current value to previous value;

	void Start ()
    {
        // Loop through our array of bars and set them to different colours of the rainbow.
		for (int i = 0; i <audioVisualiserBars.Length; i++)
        {
            audioVisualiserBars[i].GetComponent<Image>().color = Color.HSVToRGB(i * .875f / audioVisualiserBars.Length, 1, 1);
            audioVisualiserShadow[i].GetComponent<Image>().color = new Color(audioVisualiserBars[i].GetComponent<Image>().color.r, audioVisualiserBars[i].GetComponent<Image>().color.g, audioVisualiserBars[i].GetComponent<Image>().color.b, .2f);
        }

        // Set our spectrum data array to our sample size.
        spectrumData = new float[numberOfSamples];
	}
	
	void Update ()
    {
        // Put our spectrum data into our spectrum array accross all channels and using the window type we have selected in the inspector.
        GetComponent<AudioSource>().GetSpectrumData(spectrumData, 0, fftWindow);

        // Loop through our audio visualiser bars and update their size.
        for (int i = 0; i < audioVisualiserBars.Length; i++)
        {            
            // Linearly interpolate from the current size to the detected input size by our step size.
            float lerpY = Mathf.Lerp(audioVisualiserBars[i].localScale.y, spectrumData[i + 4] * sizeMultiplier, stepSize);  /// The first 4 frequency bands have been omitted as they are largely affected by background noise.
            float lerpYShadow = Mathf.Lerp(audioVisualiserShadow[i].localScale.y, spectrumData[i + 4] * sizeMultiplier, stepSize / 5);
            
            // Update the size of each bar (only the y-scale is changing).
            audioVisualiserBars[i].localScale = new Vector3(audioVisualiserBars[i].localScale.x, lerpY, audioVisualiserBars[i].localScale.z);
            audioVisualiserShadow[i].localScale = new Vector3(audioVisualiserShadow[i].localScale.x,
                                                                audioVisualiserBars[i].localScale.y > previousScale && audioVisualiserBars[i].localScale.y >= audioVisualiserShadow[i].localScale.y ? audioVisualiserBars[i].localScale.y : lerpYShadow,    /// Shadow quickly jumps up but slowly moves down.
                                                                audioVisualiserShadow[i].localScale.z);

            // Update the scale tracker.
            previousScale = audioVisualiserBars[i].localScale.y;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            sizeMultiplier += 10000;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            sizeMultiplier -= 10000;
        }
    }
}
