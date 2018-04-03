# 简答题
<img src="http://chart.googleapis.com/chart?cht=tx&chl=\Large x=\frac{-b\pm\sqrt{b^2-4ac}}{2a}" style="border:none;">
## 游戏对象运动的本质是什么？

- 游戏对象运动的本质是通过在每个极短时间的Frame内改变游戏对象的transform属性实现运动效果的。
- 从原理上来说，这个过程是离散的
 
## 请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…）
 
### 方法一【直接改变transform.position的值】

![formula_1](https://github.com/zys980808/Unity3D/blob/master/Homework/Homework2/basic_concepts/formula_1.jpg)
    
 ```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hi : MonoBehaviour {

    float v_x = -5;
    float v_y = 10;
    float g = 9.8f;
    void Update()
    {
        float delta_x = v_x * Time.deltaTime;
        float delta_y = Time.deltaTime * (v_y - 1 / 2 * g * Time.deltaTime);
        v_y -= g * Time.deltaTime;
        this.transform.position += new Vector3(delta_x, delta_y);

    }
}
```


### 方法二【用Transform.Translate()方法】
    ```
    public class parabola_2 : MonoBehaviour {

    	public float speed;
    	// Use this for initialization
    	void Start () {
    		this.transform.position = Vector3.zero;
    	}
    	
    	// Update is called once per frame
    	void Update () {
    		float rightOffset = speed * Time.deltaTime;
    		float previousX = this.transform.position.x;
    		float currentX = previousX + rightOffset;
    		float upOffset = speed * (FunctionValue (currentX) - FunctionValue (previousX));
    
    		this.transform.Translate (rightOffset, upOffset, 0);
    	}
    
    	//return the y-position of the function y = x(5-x) when x is given 
    	float FunctionValue(float x) {
    		return x * (5 - x);
    	}
    }
### 方法三【调用Vector3.MoveTowards()方法】
    
    ```
    public class parabola_3 : MonoBehaviour {

    	public float speed;
    	// Use this for initialization
    	void Start () {
    		this.transform.position = Vector3.zero;
    	}
    
    	// Update is called once per frame
    	void Update () {
    		float step = speed * Time.deltaTime;
    
    		float rightOffset = step;
    		float previousX = this.transform.position.x;
    		float currentX = previousX + rightOffset;
    		float upOffset = FunctionValue (currentX) - FunctionValue (previousX);
    		float previousY = this.transform.position.y;
    		float currentY = previousY + upOffset;
    		Vector3 target = new Vector3 (currentX, currentY, this.transform.position.z);
    
    		this.transform.position = Vector3.MoveTowards (this.transform.position, target, step);
    	}
    
    	//return the y-position of the function y = x(5-x) when x is given 
    	float FunctionValue(float x) {
    		return x * (5 - x);
    	}
    }

## 写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上。
 

- 第一步：创建Sphere作为各个星球，贴图Sphere
- 第二步：添加背景图（使用Plate）
- 第三步：初始化行星 并在Update内设置以不同法平面环绕太阳旋转
 ```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solar : MonoBehaviour
{

    public Transform Sun;
    public Transform Mercury;
    //...略
    float speed = 5;
    float self_speed = 20;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //自转
        Sun.Rotate(self_speed * Vector3.up * Time.deltaTime);
        Mercury.Rotate(self_speed * Vector3.up * Time.deltaTime);
        //....略
 
        //公转
        Mercury.RotateAround(Sun.transform.position, Vector3.up + 0.1F * Vector3.left, speed  * Time.deltaTime / 87 * 500);
        //...略
    }
}
    ```

- 第三步：Update函数中每个极短时间deltaTime内用两个旋转函数更新自转和公转的状态。 

  ```csharp
  void Update () {
  	//RotateAround第三个参数都是不同的
  	Mercury.RotateAround (sun.position, axisMercury, 20*Time.deltaTime);
  	Mercury.Rotate (Vector3.up*50*Time.deltaTime);
  	...
  	Earth.RotateAround (sun.position, axisEarth, 10*Time.deltaTime);
  	Earth.Rotate (Vector3.up*30*Time.deltaTime);
  	moon.transform.RotateAround (Earth.position, Vector3.up, 359 * Time.deltaTime);
  	...
  	Neptune.RotateAround (sun.position, axisNeptune, 4*Time.deltaTime);
  	Neptune.Rotate (Vector3.up*30*Time.deltaTime);
  }
  ```

- 第四步，把这个脚本挂在MainCamera上：

![效果图](https://github.com/zys980808/Unity3D/blob/master/Homework/Homework2/SolarSystem/Soloar_screenshot.jpg)
