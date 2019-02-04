using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
     
	void Start ()
    {
        keywords.Add("sleep", () => { SleepCalled(); });
        keywords.Add("cat", () => { CatCalled(); });
        keywords.Add("dog", () => { DogCalled(); });
        keywords.Add("green", () => { GreenCalled(); });
        keywords.Add("blue", () => { BlueCalled(); });
        keywords.Add("seven", () => { SevenCalled(); });
        keywords.Add("hello", () => { HelloCalled(); });
        keywords.Add("swim", () => { SwimCalled(); });
        keywords.Add("apple", () => { AppleCalled(); });
        keywords.Add("tell", () => { TellCalled(); });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += PhraseRecognised;
        keywordRecognizer.Start();
	}

    void PhraseRecognised(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    void SleepCalled()
    {
        Debug.Log("Sleep");

        GameObject sleep = GameObject.Find("SleepEnemy(Clone)");
        if (sleep)
        {
            Destroy(sleep);
        }
    }

    void CatCalled()
    {
        Debug.Log("Cat");

        GameObject cat = GameObject.Find("CatEnemy(Clone)");
        if (cat)
        {
            Destroy(cat);
        }
    }

    void DogCalled()
    {
        Debug.Log("Dog");

        GameObject dog = GameObject.Find("DogEnemy(Clone)");
        if (dog)
        {
            Destroy(dog);
        }
    }

    void GreenCalled()
    {
        Debug.Log("Green");

        GameObject green = GameObject.Find("GreenEnemy(Clone)");
        if (green)
        {
            Destroy(green);
        }
    }

    void BlueCalled()
    {
        Debug.Log("Blue");

        GameObject blue = GameObject.Find("BlueEnemy(Clone)");
        if (blue)
        {
            Destroy(blue);
        }
    }

    void SevenCalled()
    {
        Debug.Log("Seven");

        GameObject seven = GameObject.Find("SevenEnemy(Clone)");
        if (seven)
        {
            Destroy(seven);
        }
    }

    void HelloCalled()
    {
        Debug.Log("Hello");

        GameObject hello = GameObject.Find("HelloEnemy(Clone)");
        if (hello)
        {
            Destroy(hello);
        }
    }

    void SwimCalled()
    {
        Debug.Log("Swim");

        GameObject swim = GameObject.Find("SwimEnemy(Clone)");
        if (swim)
        {
            Destroy(swim);
        }
    }

    void AppleCalled()
    {
        Debug.Log("Apple");

        GameObject apple = GameObject.Find("AppleEnemy(Clone)");
        if (apple)
        {
            Destroy(apple);
        }
    }

    void TellCalled()
    {
        Debug.Log("Tell");

        GameObject tell = GameObject.Find("TellEnemy(Clone)");
        if (tell)
        {
            Destroy(tell);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
