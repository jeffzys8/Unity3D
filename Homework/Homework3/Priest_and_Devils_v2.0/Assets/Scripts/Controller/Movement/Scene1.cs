using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Controller.Movement_New
{
    
    public class CCActionManager_Scene1 : SSActionManager, ISSActionCallback
    {
        //场景1的动作管理器，实际上也就是整个游戏的动作管理器了
        //只用到一个动作，CCAction_DirectMoveTo
        //WARNING：动作管理器和场记之间有过多耦合
        public Controller.Scene.SceneController_1 sceneController;
        private bool movable = true;   //当前是否可以移动；
        public bool Movable() { return movable; }   //给场记使用：返回是否可以移动
        readonly Vector3 fromPosition = new Vector3(8, 2.5f, 0);
        readonly Vector3 toPosition = new Vector3(-8, 2.5f, 0);
        protected void Start()
        {
            //UNIFINISHED 为什么这里不能运行

        }
        public void MoveBoat(int from_to)
        {
            if (!movable) return;
            sceneController = Controller.Director.getInstance().currentSceneController as Controller.Scene.SceneController_1;
            if (sceneController.boat.isEmpty()) //船上无人，不能移动
                return;
            sceneController.boat.Move();
            CCAction_DirectMoveTo temp_action;
            if (from_to == -1)
            {
                temp_action = CCAction_DirectMoveTo.GetAction(fromPosition,3);
                RunAction(sceneController.boat.getGameobj(), temp_action, this);
            }
            else
            {
                temp_action = CCAction_DirectMoveTo.GetAction(toPosition,3);
                RunAction(sceneController.boat.getGameobj(), temp_action, this);
            }
            sceneController.SetGameState(sceneController.check_game_over());
        }
        public void SSActionEvent(SSAction source,
            SSActionEventType events = SSActionEventType.Completed,
            int intParam = 0,
            string strParam = null,
            UnityEngine.Object objectParam = null)
        {
            //接受动作的回掉信息，以保证同一时间只有一个动作在执行
            if (events == SSActionEventType.Completed)
            {
                movable = true;
            }
            else if (events == SSActionEventType.Started)
            {
                movable = false;
            }
        }


    }
}
