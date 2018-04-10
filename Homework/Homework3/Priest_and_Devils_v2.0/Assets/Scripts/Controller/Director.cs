using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 在这个只有一个Scene的游戏里，Director显得有点“多余”
 * 如果真的要展现其强大之处，我觉得应该是他来指导SceneController的挂载，而不应该一开始就挂载一个FirstSceneController
 * 思考了一下，可以通过AddComponent 和 Destroy 来实现场景的切换
 * 但还没有好的办法可以使Director作为“第一控制人”（问问老师？）

*/
namespace Controller
{

    public class Director : System.Object
    {
        private static Director _instance;
        public SceneController currentSceneController { get; set; }

        public static Director getInstance()
        {
            GameObject a = new GameObject();
            
            if (_instance == null)
            {
                _instance = new Director();
            }
            return _instance;
        }
    }
}