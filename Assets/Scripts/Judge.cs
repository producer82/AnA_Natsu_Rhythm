using OsuParsers.Beatmaps.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Judge : MonoBehaviour
{
    public Queue<GameObject> NoteQueue = new Queue<GameObject>();
    public GameObject Note;
    
    static int comboCount = 0;
    static int score = 0;

    public GameObject uiManeger;

    void Awake()
    {
        uiManeger = GameObject.Find("Canvas");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        NoteQueue.Enqueue(col.gameObject);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        NoteQueue.Dequeue();
        Destroy(col.gameObject);
    }

    public void Touched()
    {
        if (NoteQueue.Count < 1)
        {
            return;
        }

        Note = NoteQueue.Peek();
        // 2,1,0,1,-2
        if (Note.transform.position.y <= 3 && Note.transform.position.y >= -3)
        {
            comboCount++;
            score += 300;
            uiManeger.GetComponent<UIManeger>().judgeText(1);
            uiManeger.GetComponent<UIManeger>().comboText(comboCount);
            uiManeger.GetComponent<UIManeger>().scoreText(score);
            Debug.Log("Perfect!");
        }
        else if (Note.transform.position.y <= 5 && Note.transform.position.y >= -5)
        {
            comboCount++;
            score += 200;
            uiManeger.GetComponent<UIManeger>().judgeText(2);
            uiManeger.GetComponent<UIManeger>().comboText(comboCount);
            uiManeger.GetComponent<UIManeger>().scoreText(score);
            Debug.Log("Great!");
        }
        else
        {
            comboCount = 0;
            score += 100;
            uiManeger.GetComponent<UIManeger>().judgeText(3);
            uiManeger.GetComponent<UIManeger>().comboText(comboCount);
            uiManeger.GetComponent<UIManeger>().scoreText(score);
            Debug.Log("Good!");
        }

        Destroy(Note);
    }
}
