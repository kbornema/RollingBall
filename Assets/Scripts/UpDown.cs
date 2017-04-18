using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour 
{
    [SerializeField]
    private Transform min;
    [SerializeField]
    private Transform max;
    [SerializeField]
    private float speed = 1.0f;

    private float time = 0.0f;

	// Use this for initialization
	void Start () 
    {
        time = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (!LevelManager.Instance.LevelIsRunning)
            return;

        time += Time.fixedDeltaTime;

        float t = Mathf.Sin(time * speed) * 0.5f + 0.5f; 
	    	
       
        gameObject.transform.position = Vector3.Lerp(min.transform.position, max.transform.position, t);

        
	}
}
