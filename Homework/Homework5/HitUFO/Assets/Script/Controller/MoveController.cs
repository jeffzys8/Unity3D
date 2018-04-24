using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controller
{
    public class MoveController : MonoBehaviour
    {
        //存储移动中的飞碟
        List<Disk> movingDisks;
        //标准飞行速度
        float stdSpeed = 1F;

        void Start()
        {
            movingDisks = new List<Disk>();
        }

        // Update is called once per frame
        void Update()
        {
            var thisRound = Director.currentRoundController as RoundController;
            List<Disk> temp_remove = new List<Disk>();
            foreach(Disk disk in movingDisks)
            {
                Transform transform = disk.GetGameObject().transform;
                transform.position += Time.deltaTime * stdSpeed * disk.GetSpeedRate() * (new Vector3(0, 0, 1));
                if(transform.position.z >= 30f || thisRound.GetRoundState() != RoundController.RoundStateType.Start)
                {
                    temp_remove.Add(disk);
                    DiskFactory.GetInstance().EndUseDisk(disk);
                }
            }
            foreach(Disk disk in temp_remove)
            {
                movingDisks.Remove(disk);
            }
        }

        //飞一个飞碟
        public void FlyADisk(int level)
        {
            Disk temp =  DiskFactory.GetInstance().GetAFreeDisk();
            //设置初始位置；X,Y范围 [-5,5]
            temp.GetGameObject().transform.position = new Vector3(Random.Range(-5.0f, 5f), Random.Range(-5.0f, 5f), 0);
            //设置初始旋转度：每个坐标都是[-10,10]
            temp.GetGameObject().transform.rotation = new Quaternion(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
            //设置速度
            temp.SetSpeedRate(Random.Range(1.0f,2.0f) * level);
            //设置颜色
            temp.SetColor(Random.Range(1, 4));
            //设置大小
            float scale = Random.Range(0.5f, 1f);
            temp.GetGameObject().transform.localScale = new Vector3(2*scale, 0.02f*scale, 2*scale);
            //纳入飞行List
            movingDisks.Add(temp);
        }
    }
}
