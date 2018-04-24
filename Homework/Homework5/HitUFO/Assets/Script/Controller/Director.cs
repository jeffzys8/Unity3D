using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    /*
         我取消了Director的单例模式，并且把Director的脚本挂到了一个空的游戏对象上，
        因为在我的理解里，【Director应该是游戏最初的“发起者”】
        一开始就给一个游戏对象挂上一个SceneController(在这里是RoundController)的做法在我看来有点不够科学，
        因此Director不应该是一个没有MonoBehaviour的类，所以我挂载了Director到空对象中。
        当然【BestPractice是进一步保证这个项目绝对只有一个Director】，不需要人为控制，现在还没找到好的办法
    */
    public class Director : MonoBehaviour,IRoundCallback
    {
        //当前的RoundController及其获取函数
        public static RoundController currentRoundController;
        public RoundController GetCurrentRoundController() {return currentRoundController; }
        //加载新的Round(创建Round实例)
        void Start()
        {
            currentRoundController = this.gameObject.AddComponent(typeof(RoundController)) as RoundController;
            
        }

        //给RoundController【通知该回合结束的接口】
        //结束当前回合并开始新的回合【重置RoundController】
        public void EndCurrentRound()
        {
            currentRoundController.Reset();
        }
    }

    public interface IRoundCallback
    {
        void EndCurrentRound();
    }
}