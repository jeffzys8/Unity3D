using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class ClickGUI : MonoBehaviour {

	// 点击事件
	void OnMouseDown() {
        //进一步改进，通过导演找到RoundController
        var thisRound = Director.currentRoundController as RoundController;
        (thisRound as IClickGUICallbackRoundController).ClickDisk(DiskFactory.GetDiskFromObject(this.gameObject));
	}
}
