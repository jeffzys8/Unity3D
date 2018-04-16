using Controller;
using UnityEngine;

namespace Controller.Scene
{
    
    public class SceneController_1 : MonoBehaviour, SceneController, View_Contrler_Interface_Scene1
    {
        int gameState = 0;  //游戏状态；0-->正在游戏; 1-->赢; 2-->输
        View.Scene1.UserGUI userGUI;    //场景1的UI界面
        public Controller.Movement_New.CCActionManager_Scene1 movement_ctrler;

        //该场景游戏对象
        public Controller.MyGameObject.LandController fromCoast;    //这是下策，具体改进方法见于笔记
        public Controller.MyGameObject.LandController toCoast;
        public Controller.MyGameObject.BoatController boat;
        public Controller.MyGameObject.MyCharacterController[] characters;
        private GameObject water;
        
        public void SetGameState(int _g)
        {
            gameState = _g;
        }
        //场景开始
        void Start()
        {
            Director director = Director.getInstance();
            director.currentSceneController = this;
            userGUI = gameObject.AddComponent<View.Scene1.UserGUI>() as View.Scene1.UserGUI;
            movement_ctrler = new Movement_New.CCActionManager_Scene1();
            LoadResources();
        }

        public void UnloadResources()
        {
            //由于只有一个场景，此处留空
        }

        public void LoadResources()
        {
            //加载各种游戏资源
            Vector3 water_pos = new Vector3(0, 0, 0);
            GameObject water = Instantiate(Resources.Load("pf_water", typeof(GameObject)), water_pos, Quaternion.identity, null) as GameObject;
            water.name = "water";

            fromCoast = new Controller.MyGameObject.LandController("from");
            toCoast = new Controller.MyGameObject.LandController("to");
            boat = new Controller.MyGameObject.BoatController();

            characters = new Controller.MyGameObject.MyCharacterController[6];
            for (int i = 0; i < 3; i++)
            {
                //UNFINISHED：为什么要浪费空间新建一个示例呢？不能直接修改吗
                Controller.MyGameObject.MyCharacterController cha = new Controller.MyGameObject.MyCharacterController("priest");
                cha.setName("priest" + i);
                cha.setPosition(fromCoast.getEmptyPosition());
                cha.getOnCoast(fromCoast);
                fromCoast.getOnLand(cha);

                characters[i] = cha;

            }

            for (int i = 0; i < 3; i++)
            {
                Controller.MyGameObject.MyCharacterController cha = new Controller.MyGameObject.MyCharacterController("devil");
                cha.setName("devil" + i);
                cha.setPosition(fromCoast.getEmptyPosition());
                cha.getOnCoast(fromCoast);
                fromCoast.getOnLand(cha);

                characters[i + 3] = cha;
            }
        }
        public int check_game_over()
        {   // 0->not finish, 1->lose, 2->win
            int from_priest = 0;
            int from_devil = 0;
            int to_priest = 0;
            int to_devil = 0;

            int[] fromCount = fromCoast.getCharacterNum();
            from_priest += fromCount[0];
            from_devil += fromCount[1];

            int[] toCount = toCoast.getCharacterNum();
            to_priest += toCount[0];
            to_devil += toCount[1];

            if (to_priest + to_devil == 6)      // win
                return 2;

            int[] boatCount = boat.getCharacterNum();
            if (boat.get_to_or_from() == -1)
            {   // boat at toCoast
                to_priest += boatCount[0];
                to_devil += boatCount[1];
            }
            else
            {   // boat at fromCoast
                from_priest += boatCount[0];
                from_devil += boatCount[1];
            }
            if (from_priest < from_devil && from_priest > 0)
            {       // lose
                return 1;
            }
            if (to_priest < to_devil && to_priest > 0)
            {
                return 1;
            }
            return 0;           // not finish
        }
        /* --------------------------------------------------------------------------------
         * --------------实现UI的接口函数--------------------------------------------------
         * --------------------------------------------------------------------------------*/
        public int GetGameState() { return gameState; }
        public void BoatClicked()
        {
            if (!movement_ctrler.Movable())
            {
                Debug.Log("here");
                return;
            }
            if (boat.isEmpty())
            {
                Debug.Log("not here");
                return;
            }
            boat.Move();
            gameState = check_game_over();
        }

        public void ClickRestart()
        {
            gameState = 0;
            boat.reset();
            fromCoast.reset();
            toCoast.reset();
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].reset();
            }
        }

        public void CharacterClicked(GameObject character)
        {
            if (!Movement.movable) return;
            //找出点击角色的Controller
            Controller.MyGameObject.MyCharacterController characterCtrl = null;
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i].getName() == character.name)
                {
                    characterCtrl = characters[i];
                }
            }
            if (characterCtrl == null) return;//UNFINISHED 异常处理

            if (characterCtrl.isOnBoat())
            {
                Controller.MyGameObject.LandController whichCoast;
                if (boat.get_to_or_from() == -1)
                { // to->-1; from->1
                    whichCoast = toCoast;
                }
                else
                {
                    whichCoast = fromCoast;
                }


                boat.GetOffBoat(characterCtrl.getName());
                characterCtrl.moveToPosition(whichCoast.getEmptyPosition());
                characterCtrl.getOnCoast(whichCoast);
                whichCoast.getOnLand(characterCtrl);

            }
            else
            {                                   // character on coast
                Controller.MyGameObject.LandController whichCoast = characterCtrl.getCoastController();

                if (boat.getEmptyIndex() == -1)
                {       // boat is full
                    return;
                }

                if (whichCoast.get_type() != boat.get_to_or_from())   // boat is not on the side of character
                    return;

                whichCoast.getOffLand(characterCtrl.getName());
                characterCtrl.moveToPosition(boat.getEmptyPosition());
                characterCtrl.getOnBoat(boat);
                boat.GetOnBoat(characterCtrl);
            }
            gameState = check_game_over();
        }
    }
}