using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Controller.Movement_New
{
    public class CCAction_DirectMoveTo : SSAction
    {
        //直线运动
        public Vector3 target;
        public float speed;



        public static CCAction_DirectMoveTo GetAction(Vector3 target, float speed)
        {

            CCAction_DirectMoveTo action = ScriptableObject.CreateInstance<CCAction_DirectMoveTo>();
            action.target = target;
            action.speed = speed;
            return action;
        }

        public override void Update()
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
            if (this.transform.position == target)
            {
                callback.SSActionEvent(this, SSActionEventType.Completed);
            }
        }

        public override void Start()
        {
            callback.SSActionEvent(this, SSActionEventType.Started);
        }
    }
}
