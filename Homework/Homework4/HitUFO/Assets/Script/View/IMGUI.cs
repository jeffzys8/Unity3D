using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class IMGUI : MonoBehaviour
{
    public GUIStyle textStyle;
    public GUIStyle buttonStyle;
    public GUIStyle scoreStyle;

    void Start()
    {

        textStyle = new GUIStyle();
        textStyle.fontSize = 40;
        textStyle.alignment = TextAnchor.MiddleCenter;
        textStyle.normal.textColor = Color.blue;

        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 30;

        scoreStyle = new GUIStyle();
        scoreStyle.fontSize = 15;
        scoreStyle.alignment = TextAnchor.MiddleCenter;
        scoreStyle.normal.textColor = Color.red;

    }
    void OnGUI()
    {
        //进一步改进，通过导演找到RoundController
        var thisRound = Director.currentRoundController as RoundController;
        if (thisRound.GetRoundState() == RoundController.RoundStateType.Unstart)
        {
            if (thisRound.GetLastRoundState() == RoundController.LastRoundStateType.Init)
            {

                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "欢迎来到打飞碟!", textStyle);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "开始游戏", buttonStyle))
                {
                    (thisRound as IIMGUICallbackRoundController).ClickStart();
                }
            }

            else if (thisRound.GetLastRoundState() == RoundController.LastRoundStateType.Win)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "你通关了！太厉害了!", textStyle);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "再来一局", buttonStyle))
                {
                    (thisRound as IIMGUICallbackRoundController).ClickStart();
                }
            }

            else if (thisRound.GetLastRoundState() == RoundController.LastRoundStateType.Lose)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "可惜，再试试吧？", textStyle);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "再来一局", buttonStyle))
                {
                    (thisRound as IIMGUICallbackRoundController).ClickStart();
                }
            }
        }
        else if (thisRound.GetRoundState() == RoundController.RoundStateType.Start)
        {
            //显示得分等信息
            GUI.Label(new Rect(10, 10, 100, 50), "得分:"+ thisRound.GetScore()+"/"+thisRound.scoreEveryLevel, scoreStyle);
            GUI.Label(new Rect(10, 25, 100, 50), "剩余时间:" + (int)thisRound.GetTimeLeft(), scoreStyle);
        }
        else if (thisRound.GetRoundState() == RoundController.RoundStateType.NextLevel)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "恭喜过关", textStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "下一等级 ", buttonStyle))
            {
                (thisRound as IIMGUICallbackRoundController).ClickNextLevel();
            }
        }
    }
}
