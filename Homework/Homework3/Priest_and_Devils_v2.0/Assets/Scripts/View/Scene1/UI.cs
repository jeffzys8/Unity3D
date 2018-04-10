using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

namespace View.Scene1
{
    public class UserGUI : MonoBehaviour
    {
        private View_Contrler_Interface_Scene1 interaction;
        //public int status = 0;      //UNFINISHED: 所应显示状态不应该由UI来记录，应该由SceneController_1保存并提供接口给UI
        GUIStyle style;
        GUIStyle buttonStyle;

        void Start()
        {
            interaction = Director.getInstance().currentSceneController as View_Contrler_Interface_Scene1;

            style = new GUIStyle();
            style.fontSize = 40;
            style.alignment = TextAnchor.MiddleCenter;

            buttonStyle = new GUIStyle("button");
            buttonStyle.fontSize = 30;
        }
        void OnGUI()
        {
            if (interaction.GetGameState() == 1)    //输了
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "你输了!", style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "重新开始", buttonStyle))
                {
                    interaction.ClickRestart();
                }
            }
            else if (interaction.GetGameState() == 2)   //赢了
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "你赢了!", style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "重新开始", buttonStyle))
                {
                    interaction.ClickRestart();
                }
            }
        }
    }
}