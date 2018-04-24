using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class Disk
    {

        GameObject gameObject = null;
        public GameObject GetGameObject() { return gameObject; }

        int color;//1:white; 2:yellow; 3:red
        public int GetColor() { return color; }
        public void SetColor(int _c)
        {
            color = _c;
            switch (color)
            {
                case 1: gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow; break;
                case 2: gameObject.GetComponent<MeshRenderer>().material.color = Color.red; break;
                case 3: gameObject.GetComponent<MeshRenderer>().material.color = Color.blue; break;
            }
        }

        float sizeRate;
        public float GetSizeRate() { return sizeRate; }
        public void SetSizeRate(float _s) { sizeRate = _s; }

        float speedRate;
        public float GetSpeedRate() { return speedRate; }
        public void SetSpeedRate(float _s) { speedRate = _s; }

        readonly Vector3 defalut_scale = new Vector3(2, 0.02F, 2);

        public Disk(int _color, float _size, float _speed)
        {
            color = _color;
            sizeRate = _size;
            speedRate = _speed;
            //生成
            gameObject = GameObject.Instantiate(Resources.Load("prefab_white", typeof(GameObject))) as GameObject;
            SetColor(_color);
            gameObject.transform.localScale = defalut_scale * _size;

        }
    }
}