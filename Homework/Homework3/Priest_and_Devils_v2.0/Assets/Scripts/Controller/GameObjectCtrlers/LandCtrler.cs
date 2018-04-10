using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.MyGameObject
{
    public class LandController
    {
        readonly GameObject coast;
        readonly Vector3 from_pos = new Vector3(22.5f, 2, 0);
        readonly Vector3 to_pos = new Vector3(-22.5f, 2, 0);
        readonly Vector3[] positions;
        readonly int type;    // to->-1, from->1


        MyCharacterController[] landers;

        public LandController(string _type)
        {
            positions = new Vector3[] {
                new Vector3(16f,7f,0), new Vector3(18f,7f,0), new Vector3(20f,7f,0),
                new Vector3(22f,7f,0), new Vector3(24f,7f,0), new Vector3(26f,7f,0)};

            landers = new MyCharacterController[6];

            if (_type == "from")
            {
                coast = Object.Instantiate(Resources.Load("pf_land", typeof(GameObject)), from_pos, Quaternion.identity, null) as GameObject;
                coast.name = "from";
                type = 1;
            }
            else
            {
                coast = Object.Instantiate(Resources.Load("pf_land", typeof(GameObject)), to_pos, Quaternion.identity, null) as GameObject;
                coast.name = "to";
                type = -1;
            }
        }

        public int getEmptyIndex()
        {
            for (int i = 0; i < landers.Length; i++)
            {
                if (landers[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public Vector3 getEmptyPosition()
        {
            Vector3 pos = positions[getEmptyIndex()];
            pos.x *= type;
            return pos;
        }

        public void getOnLand(MyCharacterController characterCtrl)
        {
            int index = getEmptyIndex();
            landers[index] = characterCtrl;
        }

        public MyCharacterController getOffLand(string lander_name)
        {   // 0->牧师, 1->恶魔
            for (int i = 0; i < landers.Length; i++)
            {
                if (landers[i] != null && landers[i].getName() == lander_name)
                {
                    MyCharacterController charactorCtrl = landers[i];
                    landers[i] = null;
                    return charactorCtrl;
                }
            }
            return null;
        }

        public int get_type()
        {
            return type;
        }

        public int[] getCharacterNum()
        {
            int[] count = { 0, 0 };
            for (int i = 0; i < landers.Length; i++)
            {
                if (landers[i] == null)
                    continue;
                if (landers[i].getType() == 0)
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

        public void reset()
        {
            landers = new MyCharacterController[6];
        }
    }
}
