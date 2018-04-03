using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solar : MonoBehaviour
{

    public Transform Sun;
    public Transform Mercury;
    public Transform Venus;
    public Transform Earth;
    public Transform Moon;
    public Transform Mars;
    public Transform Jupiter;
    public Transform Saturn;
    public Transform Uranus;
    public Transform Neptune;
    public Transform Pluto;
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
        Venus.Rotate(self_speed * Vector3.up * Time.deltaTime);
        Earth.Rotate(self_speed * Vector3.up * Time.deltaTime);
        Moon.Rotate(self_speed * Vector3.up * Time.deltaTime);
        Mars.Rotate(self_speed * Vector3.up * Time.deltaTime);
        Jupiter.Rotate(self_speed * Vector3.up * Time.deltaTime);
        Saturn.Rotate(self_speed * Vector3.up *  Time.deltaTime);
        Uranus.Rotate(self_speed * Vector3.up * Time.deltaTime);
        Neptune.Rotate(self_speed * Vector3.up * Time.deltaTime);
        Pluto.Rotate(self_speed * Vector3.up * Time.deltaTime);

        //公转
        Mercury.RotateAround(Sun.transform.position, Vector3.up + 0.1F * Vector3.left, speed  * Time.deltaTime / 87 * 500);
        Venus.RotateAround(Sun.transform.position, Vector3.up - 0.3F * Vector3.left, speed  * Time.deltaTime / 224 * 500);
        Earth.RotateAround(Sun.transform.position, Vector3.up - 0.2F * Vector3.left, speed * Time.deltaTime / 365 * 500);
        Moon.RotateAround(Earth.transform.position, Vector3.up + 0.1F * Vector3.left, speed  * Time.deltaTime / 30 * 500);
        Mars.RotateAround(Sun.transform.position, Vector3.up - 0.1F * Vector3.left, speed * Time.deltaTime / 687 * 500);
        Jupiter.RotateAround(Sun.transform.position, Vector3.up + 0.4F * Vector3.left, speed  * Time.deltaTime / 1000 * 500);
        Saturn.RotateAround(Sun.transform.position, Vector3.up - 0.6F * Vector3.left, speed  * Time.deltaTime / 1300 * 500);
        Uranus.RotateAround(Sun.transform.position, Vector3.up + 0.3F * Vector3.left, speed  * Time.deltaTime / 1500 * 500);
        Neptune.RotateAround(Sun.transform.position, Vector3.up + 0.8F * Vector3.left, speed  * Time.deltaTime / 1800 * 500);
        Pluto.RotateAround(Sun.transform.position, Vector3.up + 0.1F * Vector3.left, speed  * Time.deltaTime / 2000 * 500);

    }
}