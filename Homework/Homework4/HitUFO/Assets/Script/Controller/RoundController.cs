using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    /* 回合管理器，每一个回合都会重新挂载脚本，Awake会重置信息
     * 回合流程为 Unstart(未开始) --> Start(开始) --> NextLevel(等待下一等级开始) --> Start(开始)...-->Unstart
     * 
     * 另需一个变量记录上一个回合的游戏情况（初始化，通关，中途失败），IMGUI可以根据这个变量进行调整
     * 
     * 文件函数按流程编排
     */    
    public class RoundController : MonoBehaviour,IIMGUICallbackRoundController, IClickGUICallbackRoundController
    {
        /* 
         * 在这个函数里要对【回合的信息进行重置】
         * 一开始是想用Start的，结果发现它只会被调用一次
         */
        readonly float timeEveryLevel = 50;   //每个等级持续时间（秒）
        public int scoreEveryLevel = 100;  //每个等级需要获得的最低分数

        //上一回合的状态
        public enum LastRoundStateType: int { Init, Win, Lose}
        LastRoundStateType lastRoundState = LastRoundStateType.Init;  
        public LastRoundStateType GetLastRoundState(){ return lastRoundState; }

        //回合状态
        public enum RoundStateType : int {Unstart, Start, NextLevel}
        RoundStateType roundState = RoundStateType.Unstart;  
        public RoundStateType GetRoundState() { return roundState; }

        //剩余游戏时间
        float timeLeft = 0;   //当前游戏剩余时间
        public float GetTimeLeft() { return timeLeft; }

        //下一个飞碟飞出的时间 (-1则不在游戏时间内)
        float timeNextDisk = -1;

        //当前游戏等级,范围为[1,10]
        int level = 1;
        public int GetCurrentLevel() { return level; }

        //动作管理器
        MoveController myMoveCtrler;

        //记分管理器
        ScoreController mySocreController;
        public int GetScore() { return mySocreController.GetScore(); }

        private void Start()
        {
            //加载IMGUI
            this.gameObject.AddComponent(typeof(IMGUI));
            //加载动作管理器
            myMoveCtrler = this.gameObject.AddComponent(typeof(MoveController)) as MoveController;
            //加载积分管理器
            mySocreController = new ScoreController();

        }
        public void Reset()
        {
            //初始化回合数据
            mySocreController.Reset();
            roundState = RoundStateType.Unstart;
            level = 1;
            timeNextDisk = -1;
            scoreEveryLevel = 100;
        }

        //开始当前等级游戏
        void StartGame()
        {
            roundState = RoundStateType.Start;
            timeLeft = timeEveryLevel;  //游戏计时
            ArrangeNextDisk();
        }

        void ArrangeNextDisk()
        {
            timeNextDisk =  Random.Range(0.0F, 3.0F);
        }

        private void Update()
        {
            if(roundState == RoundStateType.Start)
            {
                //游戏时间判断
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    Lose();
                   
                }
                //是否飞飞碟判断并执行
                timeNextDisk -= Time.deltaTime;
                if(timeNextDisk <= 0)
                {
                    FlyADisk();
                    ArrangeNextDisk();
                }
            }
        }

        void FlyADisk()
        {
            //交由动作管理器来实现
            myMoveCtrler.FlyADisk(GetCurrentLevel());
        }

        //结束当前等级，等待进入下一等级
        void EndThisLevel()
        {
            roundState = RoundStateType.NextLevel;
            mySocreController.Reset();
        }

        //开始下一回合
        void StartNextLevel()
        {
            ++level;
            scoreEveryLevel *= 2;
            StartGame();
        }

        //胜利
        void Win()
        {
            roundState = RoundStateType.Unstart;
            lastRoundState = LastRoundStateType.Win;

            //通知Director，结束当前回合
            (this.gameObject.GetComponent(typeof(Director)) as Director).EndCurrentRound();
        }

        //结束游戏
        void Lose()
        {
            roundState = RoundStateType.Unstart;
            lastRoundState = LastRoundStateType.Lose;

            //通知Director，结束当前回合
            (this.gameObject.GetComponent(typeof(Director)) as Director).EndCurrentRound();
        }

        //-————————————————————————————————————————
        //这里需要实现ClickGUI, IMGUI的接口
        //---------------------------------------------------------------------------------
        //IMGUI交互接口
        public void ClickStart()
        {
            StartGame();
        }
        public void ClickNextLevel()
        {
            StartNextLevel();
        }

        //ClickGUI交互接口
        public void ClickDisk(Controller.Disk disk)
        {
            //加分
            mySocreController.HitADisk(disk);
            //回收Disk
            DiskFactory.GetInstance().EndUseDisk(disk);

            if(mySocreController.GetScore() >= scoreEveryLevel)
            {
                EndThisLevel();
            }
        }
    }

}