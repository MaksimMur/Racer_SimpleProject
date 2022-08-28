using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    [Header("Set in Inspetot")]
    [SerializeField] private float maxSpeed = 80;
    [SerializeField] float startKofToSpeed = 2;
    [SerializeField] float kofToInertialDeceleration = 3;
    [SerializeField] float kofToStopMovement = 6;
    [SerializeField] float kofRotMoment=4;
    [SerializeField] float visiblePartCarY;
    [SerializeField] float minVelocityYForAlign = 3;
    [SerializeField] float minDistanceYForAlign = 3;
    private static PlayerCar S;
    private float _currentSpeed = 0;
    public static float MIN_SPEED_TO_GAME_ACTIONS = 30F;
    private Rigidbody2D rigid;
    private Transform playerTr;
    private void Awake()
    {
        _currentSpeed = 0;
        S = this;
        rigid = GetComponent<Rigidbody2D>();
        playerTr = GetComponent<Transform>();
    }
    private KeyCode[] _keysRots = new KeyCode[] { KeyCode.A, KeyCode.D };
    private Vector2[] _rots = new Vector2[] { Vector2.left, Vector2.right };
    private void Update()
    {
        if (ExecuteCodeIfCarStayInDisplay()) {
            this.gameObject.SetActive(false);
            return; 
        }
        for(int i =0;i<2;i++)if(Input.GetKey(_keysRots[i])) rigid.velocity += _rots[i] * Time.deltaTime * kofRotMoment * _currentSpeed / maxSpeed;

        if (Input.acceleration.x < -.2f) rigid.velocity += _rots[0] * Time.deltaTime * kofRotMoment * _currentSpeed / maxSpeed;
        if (Input.acceleration.x > .2f) rigid.velocity += _rots[1] * Time.deltaTime * kofRotMoment * _currentSpeed / maxSpeed;

        if (Input.GetKey(KeyCode.W) ||_gas)
        {
            Car_Leading();
            return;
        }
        if (Input.GetKey(KeyCode.S) ||_break)
        {
            Car_Stoping();
            return;
        }

        CarInertialDeceleration();
        
    }
    private void LateUpdate()
    {
        if (align) {
            AlignCar();
            return;
        }
        AlignCarCheck();
    }
    public static bool ExecuteCodeIfCarStayInDisplay() {
        if (S.playerTr.position.y < S.minDistanceYForAlign)
        {
            return true;
        }
        return false;
    }

    //buttons events

    private bool _break=false;
    public void BreakIn() => _break = true;
    public void BreakOut() => _break = false;

    private bool _gas = false;
    public void GasIn() => _gas = true;
    public void GasOut() => _gas = false;

    //car's movement

    public void Car_Stoping() {
        _currentSpeed = Mathf.Max(0, _currentSpeed - Mathf.Log( startKofToSpeed, _currentSpeed +2) *Time.deltaTime*kofToStopMovement);
       
    }
    public void Car_Leading()
    {
        _currentSpeed = Mathf.Min(maxSpeed, _currentSpeed + Mathf.Log(startKofToSpeed, _currentSpeed + 2) * Time.deltaTime * startKofToSpeed);
    }
    public void CarInertialDeceleration() {
        if(_currentSpeed!=0)_currentSpeed = Mathf.Max(0, _currentSpeed - Mathf.Log(kofToInertialDeceleration, _currentSpeed)*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Car>()) collisionB = true;
    }
    public void AlignCarCheck() {
        if (Mathf.Abs(rigid.velocity.y) < minVelocityYForAlign && playerTr.position.y>minDistanceYForAlign &&collisionB) {
            _timeAlignStart = Time.time;
            collisionB = false;
            align = true;
        }
    }
    [SerializeField] float timeToAlign;
    private float _timeAlignStart;
    private bool align = false;
    private bool collisionB = false;
    public void AlignCar() {
        float u = (Time.time - _timeAlignStart) / timeToAlign;
        if (u >= 1) {
            align = false;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.angularVelocity = 0;
            playerTr.rotation = Quaternion.identity;
            return;
        }
        if (u >= 0.3f) { playerTr.rotation = Quaternion.Lerp(playerTr.rotation, Quaternion.identity, u-.3f);  }
        if (u >= 0.3f) playerTr.position = new Vector2(playerTr.position.x, Mathf.Lerp(playerTr.position.y, visiblePartCarY, u-.3f));
    }
    public static float CURRENT_SPEED => S._currentSpeed;
    public static float Max_Speed => S.maxSpeed;
    public static bool PosOnTheAheadPartOfRoad => S.transform.position.x <= -2;
}
