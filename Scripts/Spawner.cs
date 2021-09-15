using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float timer; // Отсчет времени
    private float oldTimer; // Время, когда в последний раз были заспавнены враги
    private bool spawned = false; // Устанавливается true после спавна врагов
    public GameObject[] enemyTypes; // Массив из типов врагов
    public Transform spawner; // Точка спавна
    public int spawnRate; // Периодичность спавна
    
    private void Update()
    {
        // Спавн врагов
        timer += Time.deltaTime; 
        if (System.Convert.ToInt32(timer) % spawnRate == 0 && !spawned) 
        {
            int rand = Random.Range(1, 21); 
            if (rand == 1 || rand == 2)
            {
                Instantiate(enemyTypes[2], spawner.position, transform.rotation); 
                spawned = true; 
                oldTimer = timer;  
            }
            if (rand >= 3 && rand <= 16) 
            {
                Instantiate(enemyTypes[0], spawner.position, transform.rotation); 
                spawned = true; 
                oldTimer = timer; 
            }
            if (rand >= 17 && rand <= 20) 
            {
                Instantiate(enemyTypes[1], spawner.position, transform.rotation); 
                spawned = true; 
                oldTimer = timer; 
            }
        }
        if (System.Convert.ToInt32(timer) > System.Convert.ToInt32(oldTimer)) 
        {
            spawned = false; 
        }
    }
}
