using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controller
{
    /*
     * 单例模式
    * 根据 DEAFAULT_DISK_NUM 来生成默认数量的飞碟；
    * 和RoundController也形成 接口-门面 模式
    * 提供外部【使用飞碟/完成使用】的接口
    */
    public interface IDiskFactory
    {
        Disk GetAFreeDisk();
        void EndUseDisk(Disk disk);
    }

    public class DiskFactory:System.Object,IDiskFactory
    {
        readonly int DEFAULT_DISK_NUM = 10;
        List<Disk> usedDisks;
        List<Disk> freeDisks;
        static DiskFactory instantce;
        public static DiskFactory GetInstance()
        {
            if (instantce == null)
                instantce = new DiskFactory();
            return instantce;
        }
        DiskFactory()
        {
            usedDisks = new List<Disk>();
            freeDisks = new List<Disk>();
            for (int i = 0; i < DEFAULT_DISK_NUM; ++i)
            {
                Disk temp = new Disk(1, 1.0f, 1.0f);
                //将飞碟移出场景
                temp.GetGameObject().transform.position += new Vector3(0,-100,0);
                //为飞船添加点击事件
                temp.GetGameObject().AddComponent(typeof(ClickGUI));
                //将该飞碟添加到空置列表
                freeDisks.Add(temp);
            }
        }
        public Disk GetAFreeDisk()
        {
            if (freeDisks.Count != 0)
            {
                usedDisks.Add(freeDisks[freeDisks.Count - 1]);
                freeDisks.RemoveAt(freeDisks.Count - 1);
                return usedDisks[usedDisks.Count - 1];
            }
            else
            {
                Disk temp = new Disk(1, 1, 1);
                usedDisks.Add(temp);
                return temp;

            }
        }
        public void EndUseDisk(Disk disk)
        {
            if (usedDisks.Contains(disk))
            {
                disk.GetGameObject().transform.position = new Vector3(0, -100, 0);
                freeDisks.Add(disk);
                usedDisks.Remove(disk);
            }
        }
        public static Disk GetDiskFromObject(GameObject obj)
        {
            foreach(Disk a in GetInstance().usedDisks) {
                if (a.GetGameObject() == obj)
                    return a;
            }
            foreach (Disk a in GetInstance().freeDisks)
            {
                if (a.GetGameObject() == obj)
                    return a;
            }
            return null;
        }
    }
}
