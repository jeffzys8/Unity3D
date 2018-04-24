using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//交互接口声明
public interface IIMGUICallbackRoundController
{
    void ClickStart(); //点击开始游戏
    void ClickNextLevel(); //点击下一等级游戏
}

public interface IClickGUICallbackRoundController
{
    void ClickDisk(Controller.Disk disk);
}