using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PitchDeathScript : MonoBehaviour
{
    [SerializeField] private int barPosition;
    [SerializeField] private GameObject vs;
    
    private float[] intensities;
    private bool isHit = false;

    void Start ()
    {
        vs = GameObject.Find("VoiceController");
	}
	
	void Update ()
    {
        intensities = new float[vs.GetComponent<VisualiserScript>().audioVisualiserBars.Length];
        
        for (int i = 0; i < vs.GetComponent<VisualiserScript>().audioVisualiserBars.Length; i++)
        {
            intensities[i] = vs.GetComponent<VisualiserScript>().audioVisualiserBars[i].transform.localScale.y;
        }

        if (vs.GetComponent<VisualiserScript>().audioVisualiserBars[barPosition].localScale.y >= intensities.Max() * .9 && vs.GetComponent<VisualiserScript>().audioVisualiserBars[barPosition].transform.localScale.y > 20)
        {
            Debug.Log("HIT : " + barPosition);
            isHit = true;
        }
        else
        {
            isHit = false;
        }

        if (isHit)
        {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime, transform.localScale.y - Time.deltaTime, 1);
        }
        else if (transform.localScale.y < 4)
        {
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime, transform.localScale.y + Time.deltaTime, 1);
        }

        if (transform.localScale.y < 2)
        {
            Destroy(gameObject);
        }
    }
}
