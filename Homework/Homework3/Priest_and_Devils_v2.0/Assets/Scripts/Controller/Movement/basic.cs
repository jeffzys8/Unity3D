using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Controller.Movement_New
{
    //-----------------------------基类-------------------------------
    //----------------------------------------------------------------

    //动作基类
    public class SSAction : ScriptableObject
    {
        public bool enable = true; //是否开始
        public bool destroy = false; //是否销毁

        public GameObject gameObject { get; set; }  //执行动作的物体
        public Transform transform { get; set; }    //执行动作的Transform

        /*
         *  接口-门面模式：与事件管理器/连续事件交互的接口 
         */
        public ISSActionCallback callback { get; set;}

        public virtual void Start()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Update()
        {
            throw new System.NotImplementedException();
        }
    }

    //动作管理基类
    public class SSActionManager : MonoBehaviour
    {
        //所管理的动作集，int是为了能使下面的两个变量识别动作
        private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
        //等待执行的动作
        private List<SSAction> waitingAdd = new List<SSAction>();  //这是什么
        //等待回收的动作
        private List<int> waitingDelete = new List<int>();

        protected void Update()
        {
            //UNFINISHED:这是做什么的？
            foreach (SSAction ac in waitingAdd) actions[ac.GetInstanceID()] = ac;
            waitingAdd.Clear();

            foreach (KeyValuePair<int, SSAction> kv in actions)
            {
                SSAction ac = kv.Value;
                if (ac.destroy)
                    waitingDelete.Add(ac.GetInstanceID());
                else if (ac.enable)
                    ac.Update();
            }

            //动作回收，直接通过unityengine回收！
            foreach(int key in waitingDelete)
            {
                SSAction ac = actions[key];
                actions.Remove(key);
                DestroyObject(ac);
            }
            waitingDelete.Clear();
        }
        public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
        {
            action.gameObject = gameobject;
            action.transform = gameobject.transform;
            action.callback = manager;
            waitingAdd.Add(action);
            action.Start();
        }
    }
    //---------------------------动作事件接口-----------------------------
    //--------------------------------------------------------------------
    public enum SSActionEventType : int { Started, Completed}

    public interface ISSActionCallback
    {
        void SSActionEvent(SSAction source,
            SSActionEventType events = SSActionEventType.Completed,
            int intParam = 0,
            string strParam = null,
            UnityEngine.Object objectParam = null);
            
    }
    
}
