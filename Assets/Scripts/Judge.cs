/*********************************
 * Rain 1 Collider (GameObject)
 * ㄴ Judge.cs
 * Rain 2 Collider (GameObject)
 * ㄴ Judge.cs
 * Rain 3 Collider (GameObject)
 * ㄴ Judge.cs
 * Rain 4 Collider (GameObject)
 * ㄴ Judge.cs
 * 
 * 노트 판정에 관한 내용을 처리함
 * ******************************/

using OsuParsers.Beatmaps.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Judge : MonoBehaviour
{
    // 충돌 범위에 들어온 노트를 담을 큐
    public Queue<GameObject> NoteQueue = new Queue<GameObject>();

    // 모든 레인이 같은 콤보와 스코어 수를 공유해야 하므로 static 선언
    static int comboCount = 0;
    static int score = 0;

    // 캔버스의 텍스트에 접근하기 위한 UI Manager
    public GameObject uiManager;

    /// 게임 시작과 동시에 호출됨
    /// uiManager 초기화
    void Start()
    {
        uiManager = GameObject.Find("Canvas");
    }

    /// 판정 박스와 충돌 감지시 자동 호출됨
    /// 판정 박스에 들어온 노트를 큐에 담음
    void OnTriggerEnter2D(Collider2D col)
    {
        NoteQueue.Enqueue(col.gameObject);
    }

    /// 판정 박스에서 빠져나갔을 시 자동 호출됨
    /// 빠져나간 노트를 큐에서 제거한 후 오브젝트를 파괴함
    void OnTriggerExit2D(Collider2D col)
    {
        NoteQueue.Dequeue();
        Destroy(col.gameObject);
    }

    /// 버튼이 터치 되었을 때 호출됨
    /// 터치 되었을 때 진행해야할 판정 작업을 수행함
    public void Touched()
    {
        GameObject Note;

        // 만약 뚝배기를 깰 노트가 없다면 수행하지 않음
        if (NoteQueue.Count < 1)
        {
            return;
        }

        // 큐에서 노트를 하나 긁어옴
        Note = NoteQueue.Peek();

        // 판정 시 수행해야할 루틴
        // 1. 콤보 추가
        // 2. 판정 별 점수 추가
        // 3. 콤보 텍스트 업데이트
        // 4. 점수 텍스트 업데이트
        // 5. 판정 텍스트 업데이트 (Perfect!, Great!, Good!)
        // 6. 로그 출력
     
        // 판정선으로부터 y 3 ~ -3 만큼 떨어진 부분은 Perfect로 처리함
        if (Note.transform.position.y <= 3 && Note.transform.position.y >= -3)
        {
            comboCount++;
            score += 300;
            uiManager.GetComponent<UIManager>().judgeText(1);
            uiManager.GetComponent<UIManager>().comboText(comboCount);
            uiManager.GetComponent<UIManager>().scoreText(score);
            Debug.Log("Perfect!");
        }
        // 판정선으로부터 y (3 ~ 5) ~ (-3 ~ -5) 만큼 떨어진 부분은 Great로 처리함
        else if (Note.transform.position.y <= 5 && Note.transform.position.y >= -5)
        {
            comboCount++;
            score += 200;
            uiManager.GetComponent<UIManager>().judgeText(2);
            uiManager.GetComponent<UIManager>().comboText(comboCount);
            uiManager.GetComponent<UIManager>().scoreText(score);
            Debug.Log("Great!");
        }
        // 나머지는 Good
        else
        {
            comboCount = 0;
            score += 100;
            uiManager.GetComponent<UIManager>().judgeText(3);
            uiManager.GetComponent<UIManager>().comboText(comboCount);
            uiManager.GetComponent<UIManager>().scoreText(score);
            Debug.Log("Good!");
        }

        // 판정 처리가 끝난 노트의 뚝배기를 깨버림
        Destroy(Note);
    }
}
