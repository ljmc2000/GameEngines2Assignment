﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
	public float speed=100;
	[Range(0,1)]
	public int accelwithtrigger;

	// Update is called once per frame
	void Update()
	{
		transform.Translate(new Vector3(0,0,Input.GetAxis("RT")+accelwithtrigger)*Time.deltaTime*speed);
		transform.Rotate(new Vector3(Input.GetAxis("Vertical"),Input.GetAxis("Horizontal"),-Input.GetAxis("HorizontalRS"))*Time.deltaTime*360);
	}
}
