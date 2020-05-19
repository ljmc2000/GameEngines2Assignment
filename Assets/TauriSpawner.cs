﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauriSpawner : Spawner
{
	public int jetCount=0;
	public float minJetOffset=150;
	public float maxJetOffset=200;
	
	public Vector3 SG1_spawnpoint;
	public Vector3 Prometheus_spawnpoint;

	public GameObject camera;
	int cameraTarget=-1;

	public GameObject Prometheus;
	public GameObject Jet;
	public GameObject Hatak_SG1;

	public Dictionary<ShipClass,Vector3> cameraParams=
		new Dictionary<ShipClass,Vector3>
	{
		{ShipClass.prometheus,new Vector3(0,200,-750)},
		{ShipClass.jet,new Vector3(0,15,-40)},
		{ShipClass.hatak_sg1,new Vector3(0,25,-50)}
	};

	// Start is called before the first frame update
	void Start()
	{
		shipPrefabs=new Dictionary<ShipClass,GameObject>
		{
			{ShipClass.prometheus,Prometheus},
			{ShipClass.jet,Jet},
			{ShipClass.hatak_sg1,Hatak_SG1}
		};
		alligence_tag="tauri";
		enemy_tag="goauld";
		enemy_layer=1<<8;

		addShip(ShipClass.hatak_sg1,SG1_spawnpoint);
		changeSpectateTarget(true);
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown("left")) changeSpectateTarget(false);
		if(Input.GetKeyDown("right")) changeSpectateTarget(true);
	}

	void changeSpectateTarget(bool up)
	{
		if(up) cameraTarget++;
		else cameraTarget--;

		int s = ships.Count;
		if(cameraTarget<0) cameraTarget+=s;
		else cameraTarget %= s;

		Ship ship = ships[(int)cameraTarget];
		OffsetPursue camOffsetPursue=camera.GetComponent<OffsetPursue>();
		camOffsetPursue.target=ship.body;
		camOffsetPursue.offset=cameraParams[ship.type];
		camera.GetComponent<CamScript>().target=ship.body;
		camera.GetComponent<OffsetPursue>().target=ship.body;
	}

	public override void spawnArmy()
	{
		Vector3 offset;
		int ship_id;
		int prometheus_id=addShip(ShipClass.prometheus,Prometheus_spawnpoint);
		for(int i=0; i<jetCount; i++)
		{
			offset=getRandomOffset(minJetOffset,maxJetOffset);
			ship_id=addShip(ShipClass.jet,Prometheus_spawnpoint+offset);
			ships[ship_id].body.transform.position=ships[ship_id].body.transform.position;
		}
	}
	
	public void setAttackMode()
	{
		foreach(Ship s in ships)
		{
			if(s.type==ShipClass.jet)
			{
				s.body.GetComponentInChildren<OffsetPursue>().active=false;
			}
		}
	}
}
