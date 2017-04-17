using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour 
{

    private float down;
    private float up;
    [SerializeField]
    private float offset = 0.5f;
    [SerializeField]
    private float speed = 1.0f;

    private float time = 0.0f;

	// Use this for initialization
	void Start () 
    {
        time = 0.0f;
        down = transform.position.y;
        up = down + offset;
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        float t = Mathf.Sin(time * speed) * 0.5f + 0.5f; 
	    	
        Vector3 pos = gameObject.transform.position;

        pos.y = Mathf.Lerp(up, down, t);

        gameObject.transform.position = pos;
	}
}
