using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SceneController
{
    void LoadResources();
    void UnloadResources();//留空
}

public interface View_Contrler_Interface_Scene1  //View和Scene1交互的接口（本游戏只有一个场景-_-）
{
    void BoatClicked();
    void CharacterClicked(GameObject character);    //按引用传递，交由SceneController_1 来决定“是哪个”
    void ClickRestart();     //点击重新开始，别忘了设置GameState为0
    int GetGameState();      //接口，检测游戏状态；0-->正在游戏; 1-->赢; 2-->输
}

