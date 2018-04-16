using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.MyGameObject
{
    public class BoatController
    {
        private GameObject boat;    //【船】对象

        readonly Movement move;
        readonly Vector3 fromPosition = new Vector3(8, 2.5f, 0);
        readonly Vector3 toPosition = new Vector3(-8, 2.5f, 0);
        readonly Vector3[] from_positions;
        readonly Vector3[] to_positions;


        int side; // 终点:-1; 起点:1
        MyCharacterController[] passenger = new MyCharacterController[2];

        public BoatController()
        {
            side = 1;

            from_positions = new Vector3[] { new Vector3(6F, 4F, 0), new Vector3(10F, 4F, 0) };
            to_positions = new Vector3[] { new Vector3(-10F, 4F, 0), new Vector3(-6F, 4F, 0) };

            boat = Object.Instantiate(Resources.Load("pf_boat", typeof(GameObject)), fromPosition, Quaternion.identity, null) as GameObject;
            boat.name = "boat";

            move = boat.AddComponent(typeof(Controller.Movement)) as Controller.Movement;
            boat.AddComponent(typeof(View.Scene1.ClickGameObject));//为船只增加可点击事件
        }

        public int getEmptyIndex()
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool isEmpty()
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] != null)
                {
                    return false;
                }
            }
            return true;
        }

        public Vector3 getEmptyPosition()
        {
            Vector3 pos;
            int emptyIndex = getEmptyIndex();
            if (side == -1)
            {
                pos = to_positions[emptyIndex];
            }
            else
            {
                pos = from_positions[emptyIndex];
            }
            return pos;
        }

        public void GetOnBoat(MyCharacterController characterCtrl)
        {
            int index = getEmptyIndex();
            passenger[index] = characterCtrl;
        }

        public MyCharacterController GetOffBoat(string passenger_name)
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] != null && passenger[i].getName() == passenger_name)
                {
                    MyCharacterController charactorCtrl = passenger[i];
                    passenger[i] = null;
                    return charactorCtrl;
                }
            }
            return null;
        }

        public GameObject getGameobj()
        {
            return boat;
        }

        public int get_to_or_from()
        { // to->-1; from->1
            return side;
        }

        public int[] getCharacterNum()
        {
            int[] count = { 0, 0 };
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] == null)
                    continue;
                if (passenger[i].getType() == 0)
                {   // 0->priest, 1->devil
                    count[0]++;
                }
                else
                {
                    count[1]++;
                }
            }
            return count;
        }
        public void Move()
        {
            var a = (Director.getInstance().currentSceneController as Controller.Scene.SceneController_1).movement_ctrler;
            if (side == -1)
            {
                a.MoveBoat(side);
                side = 1;
            }
            else
            {
                a.MoveBoat(side);
                side = -1;
            }
        }
        public void reset()
        {
            move.reset();
            if (side == -1)
            {
                Move();
            }
            passenger = new MyCharacterController[2];
        }
    }
}