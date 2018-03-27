# 简答题

## 解释 游戏对象（GameObjects） 和 资源（Assets）的区别与联系。


- 区别
	- 游戏对象：<br />我们在游戏中看到的任何场景、角色等对象都是游戏对象或者游戏对象的组合。<br/>游戏对象自身包含了**属性和行为**<br />游戏对象属于**游戏过程**的一员
	- 资源：<br />资源通常包括对象、材质、场景、声音、预设、贴图、脚本、动作等；在项目中被导入到资源文件夹中。<br />资源属于**游戏编程**的一员，在游戏过程中被实例化为游戏对象或游戏对象的一部分
- 联系
	- 游戏对象是资源的整合体现
	- 游戏对象也可以作为资源的一部分

> 可以将游戏对象比喻成一个空容器，再向其加入其中的组件和赋予其的属性后，它变得与其他游戏对象不同

## 编写一个代码，使用 debug 语句来验证 MonoBehaviour 基本行为或事件触发的条件
- 基本行为包括 Awake() Start() Update() FixedUpdate() LateUpdate()
- 常用事件包括 OnGUI() OnDisable() 

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    void Awake()
    {
        Debug.Log("Awake");
    }
    void Start()
    {
        Debug.Log("Start");
    }
    void Update()
    {
        Debug.Log("Update");
    }
    void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }
    void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }
    void OnGUI()
    {
        Debug.Log("OnGUI");
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
    }
    void OnEnable()
    {
        Debug.Log("OnEnable");
    }
}
```
![pro2](https://github.com/zys980808/Unity3D/blob/master/Homework/Homework1/basic_concepts/screenshot1.jpg)


## 查找脚本手册，了解 GameObject，Transform，Component 对象	
- 分别翻译官方对三个对象的描述（Description）
- 描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件 
	- 本题目要求是把可视化图形编程界面与 Unity API 对应起来，当你在 Inspector 面板上每一个内容，应该知道对应 API。
	- 例如：table 的对象是 GameObject，第一个选择框是 activeSelf 属性。
- 用 UML 图描述 三者的关系（请使用 UMLet 14.1.1 stand-alone版本出图）

## 整理相关学习资料，编写简单代码验证以下技术的实现： 
- 查找对象
- 添加子对象
- 遍历对象树
- 清除所有子对象

## 资源预设（Prefabs）与 对象克隆 (clone) 
- 预设（Prefabs）有什么好处？
- 预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系？
- 制作 table 预制，写一段代码将 table 预制资源实例化成游戏对象

## 尝试解释组合模式（Composite Pattern / 一种设计模式）。使用 BroadcastMessage() 方法 向子对象发送消息
