﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vehicle_Movement : MonoBehaviour
{
	public float speed;
	public int lane;

	Vector3 vehiclePosition;
	Vector3 vehicleDirection;

	bool laneChange;
	float laneChangeTimer;

	private List<GameObject> Wheels = new List<GameObject>();

	float circumference;

	void Start()
    {
		speed = 2.0f;
		lane = 3;

		vehiclePosition = new Vector3(-48.0f, -1.0f, -2.25f + lane*1.1f);
		vehicleDirection = new Vector3(speed, 0, 0);
		laneChange = false;

		foreach (Transform child in GameObject.Find("Wheels").transform)
		{
			if (child.tag == "Wheel")
				Wheels.Add(child.gameObject);
		}

		float wheelSize = Wheels[0].GetComponent<Renderer>().bounds.size.x;
		circumference = wheelSize * Mathf.PI;

		transform.position = vehiclePosition;
	}

    void Update()
    {
		//	Lane Change
		float laneChangeDuration = 1/speed;
		if (laneChange == true)
		{
			laneChangeTimer += Time.deltaTime;
			if(laneChangeTimer > laneChangeDuration)
			{
				laneChange = false;
				vehicleDirection = new Vector3(speed, 0, 0);
			}
		}

		//	Wheel Rotation
		float distance = speed * Time.deltaTime;
		float wheelRotationAngle = distance * 360 / circumference;
		foreach(GameObject wheel in Wheels)
		{
			wheel.transform.Rotate(new Vector3(0, 1, 0), wheelRotationAngle);
		}

		//	Vehicle Movement
		vehiclePosition += vehicleDirection * Time.deltaTime;
		transform.position = vehiclePosition;
	}


	public void changeLane(char dir)
	{
		if (laneChange == false)
		{
			if ((dir == 'R' || dir == 'r') && lane > 1)
			{
				lane--;
				laneChange = true;
				laneChangeTimer = 0.0f;
				vehicleDirection = new Vector3(speed, 0, -1.1f * speed);
			}
			if ((dir == 'L' || dir == 'l') && lane < 3)
			{
				lane++;
				laneChange = true;
				laneChangeTimer = 0.0f;
				vehicleDirection = new Vector3(speed, 0, 1.1f * speed);
			}
		}
	}
}
