using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour 
{
    [SerializeField]
    private Vector3 axis;
    [SerializeField]
    private float degreePerSecond;
    [SerializeField]
    public Space space;
    
	
	// Update is called once per frame
	private void FixedUpdate () 
    {
        if(LevelManager.Instance.LevelIsRunning)
            transform.Rotate(axis, degreePerSecond * Time.fixedDeltaTime, space);
	}
}
