using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinding : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject firstRain;
    GameObject secondRain;
    GameObject thirdRain;
    GameObject fourthRain;

    void Start()
    {
        firstRain = GameObject.Find("Rain 1 Collider");
        secondRain = GameObject.Find("Rain 2 Collider");
        thirdRain = GameObject.Find("Rain 3 Collider");
        fourthRain = GameObject.Find("Rain 4 Collider");

        StartCoroutine("FirstRainBind");
        StartCoroutine("SecondRainBind");
        StartCoroutine("ThirdRainBind");
        StartCoroutine("FourthRainBind");
    }

    IEnumerator FirstRainBind()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                firstRain.GetComponent<Judge>().Touched();
            }
            yield return null;
        }
    }

    IEnumerator SecondRainBind()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                secondRain.GetComponent<Judge>().Touched();
            }
            yield return null;
        }
    }

    IEnumerator ThirdRainBind()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                thirdRain.GetComponent<Judge>().Touched();
            }
            yield return null;
        }
    }

    IEnumerator FourthRainBind()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                fourthRain.GetComponent<Judge>().Touched();
            }
            yield return null;
        }
    }
}
