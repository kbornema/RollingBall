using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBall : MonoBehaviour 
{
    [SerializeField]
    private float _jumpPower = 5.0f;
    [SerializeField]
    private float _rollingSpeed = 30.0f;

    private Rigidbody _myBody;
    public Rigidbody PlayerBody { get { return _myBody; } }

    private void Awake()
    {
        _myBody = GetComponent<Rigidbody>();
        
    }

	// Use this for initialization
	void Start () 
    {
       
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        _myBody.position += GameManager.Instance.MoveVec * _rollingSpeed * Time.fixedDeltaTime;
    }

    
}
