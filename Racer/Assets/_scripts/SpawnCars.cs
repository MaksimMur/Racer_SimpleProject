using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite[] cars_sprites;
    [SerializeField] private Vector2[] pointToSpawnHeadCar;
    [SerializeField] private Vector2[] pointAheadSpawnHeadCar;
    [SerializeField] private float kofReduceTimeSpawn=2f;
    private float timeBetweenSpawn = 4;
    private IEnumerator coroutine;
    private void Awake()
    {
        coroutine = SpawnInvoker();
        StartCoroutine(coroutine);
    }
    private IEnumerator SpawnInvoker(){
        while (true)
        {
            float c_r = PlayerCar.CURRENT_SPEED;
            
            if (c_r < PlayerCar.MIN_SPEED_TO_GAME_ACTIONS) yield return null;
            else
            {
                Spawn();
                yield return new WaitForSeconds(timeBetweenSpawn- Math.Min(timeBetweenSpawn-0.5f,c_r / PlayerCar.Max_Speed*kofReduceTimeSpawn));
            }
            
        }
    }
    private void Spawn() {
        Car r = Instantiate<GameObject>(prefab).GetComponent<Car>();
        if (r == null) throw new Exception("Prefab was trust");
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            Vector2 p=pointToSpawnHeadCar[UnityEngine.Random.Range(0, pointToSpawnHeadCar.Length)];
            r.transform.position = p;
            r.sp.flipY = true;
            r.up = false;
        }
        else r.transform.position = pointAheadSpawnHeadCar[UnityEngine.Random.Range(0, pointAheadSpawnHeadCar.Length)];
        r.speed = PlayerCar.CURRENT_SPEED;
        r.sp.sprite = cars_sprites[UnityEngine.Random.Range(0, cars_sprites.Length)];
    }
   

}
