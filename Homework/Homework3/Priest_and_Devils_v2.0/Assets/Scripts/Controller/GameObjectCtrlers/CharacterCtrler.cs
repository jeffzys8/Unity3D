using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controller.MyGameObject
{
    public class MyCharacterController
    {
        private GameObject character;
        readonly Movement moveableScript;
        readonly View.Scene1.ClickGameObject clickGUI;
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
            clickGUI = character.AddComponent(typeof(View.Scene1.ClickGameObject)) as View.Scene1.ClickGameObject;
            //clickGUI.setController(this); UNFINISHED 选择点那个不是在这里选
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
            coastController = (Director.getInstance().currentSceneController as Controller.Scene.SceneController_1).fromCoast;
            getOnCoast(coastController);
            setPosition(coastController.getEmptyPosition());
            coastController.getOnLand(this);
        }
    }
}
