using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] float distanceForSpawn, distanceForRespawn;
    [SerializeField] Transform p1;
    [SerializeField] Transform p2;
    [SerializeField] Sprite[] spritesOfView;
    void Update()
    {
        float speed = PlayerCar.CURRENT_SPEED;
        p1.position -= new Vector3(0, speed * Time.deltaTime);
        p2.position -= new Vector3(0, speed * Time.deltaTime);

        if (p1.position.y <= distanceForRespawn)
        {
            p1.position = new Vector2(p1.position.x, distanceForSpawn);
            p1.GetComponent<SpriteRenderer>().sprite = spritesOfView[Random.Range(0, spritesOfView.Length)];
        }
        if (p2.position.y <= distanceForRespawn)
        {
            p2.position = new Vector2(p1.position.x, distanceForSpawn);
            p2.GetComponent<SpriteRenderer>().sprite = spritesOfView[Random.Range(0, spritesOfView.Length)];
        }
    }
}
