using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

namespace Controller
{

    public class Director : System.Object
    {
        private static Director _instance;
        public SceneController currentSceneController { get; set; }

        public static Director getInstance()
        {
            if (_instance == null)
            {
                _instance = new Director();
            }
            return _instance;
        }
    }

    public interface SceneController
    {
        void loadResources();
    }

    public interface UserAction
    {
        void moveBoat();
        void characterIsClicked(MyCharacterController characterCtrl);
        void restart();
    }

    /*-----------------------------------船/角色的动作管理（抛物线运动）------------------------------------------*/
    public class Movement : MonoBehaviour
    {

        readonly float move_speed = 20;

        public static bool movable; 
        int moving_status;  // 0:未移动，1:移动
        Vector3 dest;
        Vector3 start;
        float v_y;  //纵轴速度
        float v_x;  //横轴速度
        float accel; //纵轴加速度
        float time_left;

        private void Start()
        {
            movable = true;
        }

        void Update()
        {
            if (moving_status == 1)
            {
                float delta_time = Time.deltaTime;
                transform.position += new Vector3(v_x * delta_time, 
                                    v_y * delta_time + 0.5f * accel * delta_time * delta_time,0);
                time_left -= delta_time;
                v_y += accel * delta_time;
                if (time_left <= 0)
                {
                    moving_status = 0;
                    movable = true;
                }
            }
        }
        public void setDestination(Vector3 _dest)
        {
            dest = _dest;
            start = transform.position;
            float y_diff = dest.y - start.y;
            float x_diff = dest.x - start.x;
            accel = 2 * y_diff * move_speed * move_speed / (x_diff * x_diff);
            v_y = 0;
            v_x = (x_diff > 0) ? move_speed : -move_speed;
            time_left = System.Math.Abs(x_diff / v_x);
            moving_status = 1;
            movable = false;
        }

        public void reset()
        {
            moving_status = 0;
        }
    }


    /*-----------------------------------MyCharacterController------------------------------------------*/
    public class MyCharacterController
    {
        readonly GameObject character;
        readonly Movement moveableScript;
        readonly ClickGUI clickGUI;
        readonly int characterType; // 0->priest, 1->devil

        bool _isOnBoat;
        LandController coastController;


        public MyCharacterController(string which_character)
        {

            if (which_character == "priest")
            {
                character = Object.Instantiate(Resources.Load("pf_priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                characterType = 0;
            }
            else
            {
                character = Object.Instantiate(Resources.Load("pf_devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                characterType = 1;
            }
            moveableScript = character.AddComponent(typeof(Movement)) as Movement;
            clickGUI = character.AddComponent(typeof(ClickGUI)) as ClickGUI;
            clickGUI.setController(this);
        }

        public void setName(string name)
        {
            character.name = name;
        }

        public void setPosition(Vector3 pos)
        {
            character.transform.position = pos;
        }

        public void moveToPosition(Vector3 destination)
        {
            moveableScript.setDestination(destination);
        }

        public int getType()
        {   // 0->牧师, 1->恶魔
            return characterType;
        }

        public string getName()
        {
            return character.name;
        }

        public void getOnBoat(BoatController boatCtrl)
        {
            coastController = null;
            character.transform.parent = boatCtrl.getGameobj().transform;
            _isOnBoat = true;
        }

        public void getOnCoast(LandController coastCtrl)
        {
            coastController = coastCtrl;
            character.transform.parent = null;
            _isOnBoat = false;
        }

        public bool isOnBoat()
        {
            return _isOnBoat;
        }

        public LandController getCoastController()
        {
            return coastController;
        }

        public void reset()
        {
            moveableScript.reset();
            coastController = (Director.getInstance().currentSceneController as ISceneController).fromCoast;
            getOnCoast(coastController);
            setPosition(coastController.getEmptyPosition());
            coastController.getOnLand(this);
        }
    }

    /*-----------------------------------CoastController------------------------------------------*/
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

    /*-----------------------------------BoatController------------------------------------------*/
    public class BoatController
    {
        readonly GameObject boat;
        readonly Movement moveableScript;
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

            moveableScript = boat.AddComponent(typeof(Movement)) as Movement;
            boat.AddComponent(typeof(ClickGUI));
        }


        public void Move()
        {
            if (side == -1)
            {
                moveableScript.setDestination(fromPosition);
                side = 1;
            }
            else
            {
                moveableScript.setDestination(toPosition);
                side = -1;
            }
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

        public void reset()
        {
            moveableScript.reset();
            if (side == -1)
            {
                Move();
            }
            passenger = new MyCharacterController[2];
        }
    }
}