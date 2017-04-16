using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBall : MonoBehaviour 
{
    //[SerializeField]
    //private float _jumpPower = 5.0f;
    [SerializeField]
    private float _rollingSpeed = 30.0f;

    private Rigidbody _myBody;
    public Rigidbody PlayerBody { get { return _myBody; } }

    private void Awake()
    {
        _myBody = GetComponent<Rigidbody>();

        Vector3 startPos = transform.position;
        startPos.y = 1.0f;
        transform.position = startPos;

        LevelManager.Instance.onLevelEnd.AddListener(OnLevelEnd);
        
    }

    private void OnLevelEnd(LevelManager arg0)
    {
        _rollingSpeed = 0.0f;
        _myBody.velocity = Vector3.zero;
        _myBody.useGravity = false;
    }

	// Use this for initialization
	void Start () 
    {
       
	}

    void Update()
    {
        if(transform.position.y < -2.0)
        {
            LevelManager.Instance.RestartLevel();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        Vector3 moveVec = GameManager.Instance.MoveVec * _rollingSpeed;
        _myBody.AddForce(moveVec * Time.deltaTime, ForceMode.Acceleration);
        //float length = moveVec.magnitude;

        //Vector3 curVelo = _myBody.velocity;
        //float veloMag = curVelo.magnitude;

        //_myBody.velocity = veloMag > length ? curVelo : moveVec;

        //_myBody.position += GameManager.Instance.MoveVec * _rollingSpeed * Time.fixedDeltaTime;
    }

    
}
