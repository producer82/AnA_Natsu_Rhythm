/*********************************
 * Canvas
 * ㄴ UIManager.c
 * 
 * UI와 관련한 작업을 수행함
 * ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Text;

public class UIManager : MonoBehaviour
{
    // 판정, 콤보, 점수 텍스트
    public Text judge;
    public Text combo;
    public Text score;

    /// 게임 시작 시 호출됨
    /// 자막을 초기화함
    void Start()
    {
        score.text = "0";
        combo.text = "0";
        judge.text = "";
    }

    /// 매개변수 값에 따라서 판정 텍스트를 출력함
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

    /// 콤보 텍스트를 업데이트함
    public void comboText(int comboCount)
    {
        combo.text = comboCount.ToString();
    }

    /// 점수 텍스트를 업데이트함
    public void scoreText(int scoreCount)
    {
        score.text = scoreCount.ToString();
    }
}
