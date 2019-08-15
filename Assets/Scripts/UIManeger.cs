using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Text;

public class UIManeger : MonoBehaviour
{
    public GameObject songInfo;

    public Text judge;
    public Text combo;
    public Text title;
    public Text score;

    void Awake()
    {
        songInfo = GameObject.Find("Parser");
    }

    void Start()
    {
        title.text = songInfo.GetComponent<MakeNote>().metadata.title;
        score.text = "0";
        judge.text = "Miss!";
    }

    public void judgeText(int value)
    {
        if (value == 0)
        {
            judge.text = "Miss!";
        }
        else if (value == 1)
        {
            judge.text = "Perfect!";
        }
        else if (value == 2)
        {
            judge.text = "Good!";
        }
        else if (value == 3)
        {
            judge.text = "Bad!";
        }
    }

    public void comboText(int comboCount)
    {
        combo.text = comboCount.ToString();
    }

    public void scoreText(int scoreCount)
    {
        score.text = scoreCount.ToString();
    }
}
