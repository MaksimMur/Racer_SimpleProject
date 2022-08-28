using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] float distanceForSpawn, distanceForRespawn;
    [SerializeField]GameObject p1;
    [SerializeField]GameObject p2;
    [SerializeField] Sprite[] spritesOfView;
  

    // Update is called once per frame
    void Update()
    {
        float speed = PlayerCar.CURRENT_SPEED;
        MoveOfView(p1.transform,speed);
        MoveOfView(p2.transform,speed);
    }


    void MoveOfView(Transform tr, float speed) {
        tr.position -= new Vector3(0, speed*Time.deltaTime);
        if (tr.position.y <= distanceForRespawn) {
            tr.position = new Vector2(tr.position.x, distanceForSpawn);
            tr.GetComponent<SpriteRenderer>().sprite = spritesOfView[Random.Range(0, spritesOfView.Length)];
        }
      
    }

}
