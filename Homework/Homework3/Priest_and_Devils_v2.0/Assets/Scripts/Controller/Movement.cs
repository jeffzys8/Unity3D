using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{

    //动作管理，MonoBehaviour
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
                                    v_y * delta_time + 0.5f * accel * delta_time * delta_time, 0);
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
}
