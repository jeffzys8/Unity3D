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
![screenshot1](https://github.com/zys980808/Unity3D/blob/master/Homework/Homework1/basic_concepts/screenshot1.jpg)


## 查找脚本手册，了解 GameObject，Transform，Component 对象	
- 分别翻译官方对三个对象的描述（Description）
	- GameObject: 游戏对象是Unity中最基础的对象，它可以代表角色、 物品或场景。它们本身并不发挥很大的作用，但他们是**组件**的容器，后者真正地展现了游戏的功能。
	- Transform: Transform存储了场景中每一个物体的位置、旋转角度、缩放大小和继承状态，因此非常重要。每一个游戏对象都有一个Transform组件，无法被移除。
	- Component: 添加到游戏对象的一切事物的基类。
- 描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件 
	- table对象（实体）的属性
		- GameObject.activeSelf (bool)
		- Object.name (string)
		- GameObject.isStatic(bool)
		- GameObject.tag (string)
		- GameObject.layer (int)
	- Transform 的属性
		- Transform.position(Vector3)
		- Transform.rotation(Quaternion)
		- Transform.scale(Vector3)
- 用 UML 图描述 三者的关系（请使用 UMLet 14.1.1 stand-alone版本出图）

## 整理相关学习资料，编写简单代码验证以下技术的实现： 
- 查找对象
	

```
/*Name*/
	GameObject.Find("Hand");//Find by name
/*Tag*/
	GameObject.FindGameObjectsWithTag("bodyparts");//Find all with the same tag
	GameObject.FindWithTag("bodyparts");//Find the first with the tag

```

- 添加子对象
```
/*
We here assume a GameObject child has created 
(using CreatePrimitive or any other ways)
And there is also a GameObject parent.
The key to "create" a child object is to link the child to the parent.
*/	
child.transform.SetParent(parent);
```
- 遍历对象树
```
/*Presume here the co-parent is parent*/
foreach (Transform child in parent.transform) {
      //do something about child.gameObject, 
      //for the component "Transform" is always attached to a GameObject
}
```
- 清除所有子对象
```
/*Still we presume here the co-parent is a GameObject parent*/
foreach (Transform child in parent.transform) {
      Destroy(child.gameObject);
}
```

## 资源预设（Prefabs）与 对象克隆 (clone) 
- 预设（Prefabs）有什么好处？
	- 可提前制作或同步制作，提高效率，缩短制作周期
	- 避免重复造轮子
- 预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系？
	- 预设相当于模板，在预设做出更改时，其所链接的实例也会相应被更改
	- 对象克隆仅仅是克隆了一个实例，原对象与新对象无关联
- 制作 table 预制，写一段代码将 table 预制资源实例化成游戏对象
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefab1 : MonoBehaviour {
    public Transform prefab;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 5; ++i)
        {
            for (int e = 0; e < 5; ++e)
            {
                Instantiate(prefab, new Vector3(i * 4.0f, e * 4.0f, 0), Quaternion.identity);
            }
        }
    }
}

```
![screenshot3](https://github.com/zys980808/Unity3D/blob/master/Homework/Homework1/basic_concepts/screenshot3.jpg)

## 尝试解释组合模式（Composite Pattern / 一种设计模式）。使用 BroadcastMessage() 方法 向子对象发送消息

-	组合模式又叫**部分-整体模式**，是指在该模式下用户可以将对象用树形的形式（例如parent-child）来表示对象的层次结构,这样的话用户用一种方式就可以处理对象以及对象的集合

```c#
/* for Parent.cs */
public class Parent : MonoBehaviour {
    void ApplyDamage(float damage) {
        print("Parent got hurt:"+damage);
    }
    void Example() {
		this.BroadcastMessage("ApplyDamage", 5.0F);
    }
    void Start(){
		Example();
	}
}

/* for Child.cs */
public class Child : MonoBehaviour {
    void ApplyDamage(float damage) {
        print("Child got hurt:"+damage);
    }
}
```
![screenshot2](https://github.com/zys980808/Unity3D/blob/master/Homework/Homework1/basic_concepts/screenshot2.jpg)
