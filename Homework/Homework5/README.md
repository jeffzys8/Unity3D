
# Homework 5

## 演示视频
[视频](https://github.com/zys980808/Unity3D/blob/master/Homework/Homework5/Rec%200001.mp4)

## 所做的改进
- 为增加新的动作管理器，将原MoveController更名为MoveController_Kinemical，并将MoveController设为interface
- 增加MoveController_Physics, 使飞碟支持物理学运动
- 调整重力至2，使飞碟呈现出合理的抛物线运动

## 先挂一个UML图吓人

![UML](https://github.com/zys980808/Unity3D/blob/master/Homework/Homework4/UML.png)

## 有关MVC的体现
- 从整个游戏来看，
	- 玩家是Model，
	- 游戏对象和操作界面是View，
	- 控制器是Controller
- 从Director/RoundController来看，
	- RoundController是Model，
	- Director是Controller，
	- 其接口IRoundControllerCallback是View
- 从RoundController/GUI来看，
	- GUI是Model，
	- RoundController是Controller，
	- 其接口IGUICallback是View

## 有关Director的说明
我取消了Director的单例模式，并且把Director的脚本挂到了一个空的游戏对象上，因为在我的理解里，
- Director应该是游戏最初的“发起者”
        
一开始就给一个游戏对象挂上一个SceneController(在这里是RoundController)的做法在我看来有点不够科学，因此Director不应该是一个没有MonoBehaviour的类，所以我挂载了Director到空对象中。

当然
- BestPractice是进一步保证这个项目绝对只有一个Director，不需要人为控制，现在还没找到好的办法


## 游戏流程【RoundController部分】
Unstart(未开始) --> <br/>
Start(开始) --> <br/>
NextLevel(等待下一等级开始) --> <br/>
Start(开始)...--><br/>
Unstart


## 得分公式：色彩得分 * 大小得分 * 速度得分

|色彩|	概率|	得分|
| -- | -- | -- |
|白色	|0.6|	1|
|黄色	|0.3|	2|
|红色	|0.1|	4|

|大小（与预设相比）|	概率|	得分|
| -- | -- | -- | 
|1	|0.6|	1|
|0.5	|0.3|	2|
|0.25	|0.1|	4|

|速度（与预设相比）|	概率|	得分|
| -- | -- | -- | 
|1	|0.6|	1|
|1.5	|0.3|	2|
|2	|0.1|	4|






