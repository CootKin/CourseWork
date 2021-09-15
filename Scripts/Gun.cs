using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float timerFirerate; // Отсчет времени между выстрелами
    public float offset; // Величина для корректировки разницы в положении курсора и пушки
    public GameObject bullet; // Пуля
    public Transform shotPoint; // Точка спавна пуль
    public float firerate; // Время между выстрелами

    void Update()
    {
        // Поворот пушки за курсором
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset); 

        // Выстрел и скорострельность
        if (timerFirerate <= 0) 
        {
            if (Input.GetMouseButton(0)) 
            {
                Instantiate(bullet, shotPoint.position, transform.rotation); 
                timerFirerate = firerate; 
            }
        }
        else timerFirerate -= Time.deltaTime; 
    }
}
