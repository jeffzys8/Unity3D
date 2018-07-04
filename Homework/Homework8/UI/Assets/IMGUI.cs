using UnityEngine;
using UnityEngine.UI;

public class IMGUI : MonoBehaviour
{
    public Slider healthSlider;
    public float health = 0.0f;
    private float resulthealth = 0.0f;

    private Rect HealthBar;
    private Rect HealthUp;
    private Rect HealthDown;


    void OnGUI()
    {   
       
        HealthBar = new Rect(50, 50, 200, 20);
        HealthUp = new Rect(105, 80, 40, 20);
        HealthDown = new Rect(155, 80, 40, 20);


        if (GUI.Button(HealthUp, "+"))
        {
            resulthealth = resulthealth + 0.1f > 1.0f ? 1.0f : resulthealth + 0.1f;
        }
        if (GUI.Button(HealthDown, "-"))
        {
            resulthealth = resulthealth - 0.1f < 0.0f ? 0.0f : resulthealth - 0.1f;
        }
        
        health = Mathf.Lerp(health, resulthealth, 0.01f);
        GUI.HorizontalScrollbar(HealthBar, 0.0f, health, 0.0f, 1.0f);
        healthSlider.value = health;
    }
}