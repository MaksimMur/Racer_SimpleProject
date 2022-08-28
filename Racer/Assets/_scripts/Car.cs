using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Set in Inpsector: cars options")]
    public float speed;
    public bool up=true;
    [SerializeField]private Vector2 kofSpeedValue = new Vector2(0.1f, 1f);
    [SerializeField] private float kofReduceSpeedNearlyCars = 2f;
    [SerializeField] private float boarderYForDestroy = -25f;
    private float kofSpeed;
    public SpriteRenderer sp;
    private Rigidbody2D rigid;
    private void Awake()
    {
        kofSpeed = Random.Range(kofSpeedValue.x, kofSpeedValue.y);
        sp = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CarMove();
    }
    private void CarMove()
    {
        if (!up)
        {
            rigid.velocity = Vector2.down * kofSpeed * speed;
            return;
        }
        rigid.velocity = Vector2.down * kofSpeed * speed/kofReduceSpeedNearlyCars;
        
    }
    private void LateUpdate()
    {
        if (transform.position.y < boarderYForDestroy) Destroy(this.gameObject);
    }
}
