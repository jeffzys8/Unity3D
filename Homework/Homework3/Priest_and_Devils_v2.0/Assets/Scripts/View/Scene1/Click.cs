using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
        本文件负责 View_Scene1 --- ClickGameObject，即用户点击游戏对象的事件
        鉴于游戏作扩展时，Scene会增加，不同Scene会有不同效果的界面和接口,因此不同Scene的View用不同的文件夹包装，namespace也不再是简单的View
        如此一来，比如所增添地第二场景需要不同的接口，可以类似地实现SecondView文件夹进行实现而不会造成混乱
        （当然更靠谱的实现是用数字标识，比如Scene1）
 * 
 */
namespace View.Scene1
{
    public class ClickGameObject : MonoBehaviour
    {

        View_Contrler_Interface_Scene1 interaction;              //View与Scene1控制器的交互接口
        //MyCharacterController characterController;  

        /*public void setController(MyCharacterController characterCtrl)  //不应该由View来做这种事情
        {
            characterController = characterCtrl;
        }*/

        void Start()
        {
            interaction =Controller.Director.getInstance().currentSceneController as View_Contrler_Interface_Scene1;
        }
        void OnMouseDown()
        {
            //if (!Movement.movable) return;      //这里应该被删除，因为逻辑判断不应该放在这里
            if (gameObject.name == "boat")
            {
                interaction.BoatClicked();          //这里由moveBoat改名为BoatClicked (用户并不能决定Boat是否移动)
                                                    //另外，由于船只有一只，所以不需要再传递gameObject过去了
            }
            else
            {
                interaction.CharacterClicked(gameObject);  //UNFINISHED: 应该由Controller来确定点的具体“是什么”
            }
        }
    }
}