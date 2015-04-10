﻿using UnityEngine;
using System.Collections;
using RTS;
using System.Collections.Generic;

public class Worker : Unit {

	public int buildSpeed;

	private Building currentProject;
	private bool building = false;
	private float amountBuilding = 0.0f;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		actions = new string[] {"WarFactory", "QGrobot", "tour_de_guet"};
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update ();

		if ((transform.position.x - destination.x < 0.5 && transform.position.x - destination.x > -0.5) && 
			(transform.position.y - destination.y < 1 && transform.position.y - destination.y > -1) && 
			(transform.position.z - destination.z < 0.5 && transform.position.z - destination.z > -0.5)) 
		{

			if (building)
			{
				currentProject.Construct(buildSpeed * Time.deltaTime);
			}
		}
	}

	public override void SetBuilding (Building project)
	{
		base.SetBuilding (project);
		currentProject = project;
		StartMove (currentProject.transform.position, currentProject.gameObject);
		building = true;
	}

	public void StopBuilding()
	{
		currentProject = null;
		building = false;
	}

	public override void PerformAction (string actionToPerform)
	{
		base.PerformAction (actionToPerform);
		/*int costCrystalite = RessourceManager.GetBuilding (actionToPerform).GetComponent<Building> ().cost[ResourceType.Crystalite];
		Debug.Log (costCrystalite);
		if (RessourceManager.GetBuilding (actionToPerform).GetComponent<Building> ().cost [ResourceType.Crystalite] > player.GetResource (ResourceType.Crystalite) 
			|| RessourceManager.GetBuilding (actionToPerform).GetComponent<Building> ().cost [ResourceType.Dilithium] > player.GetResource (ResourceType.Dilithium)) 
		{
			Debug.Log ("Not enough resources!");
		} 
		else 
		{*/
			/*layer.AddResource (ResourceType.Crystalite, - RessourceManager.GetBuilding (actionToPerform).GetComponent<Building> ().cost [ResourceType.Crystalite]);
			player.AddResource (ResourceType.Dilithium, - RessourceManager.GetBuilding (actionToPerform).GetComponent<Building> ().cost [ResourceType.Dilithium]);*/
			CreateBuilding (actionToPerform);
		//}
	}

	private void CreateBuilding(string buildingName)
	{
		Vector3 buildPoint = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 10);
		if (player) 
		{
			player.CreatBuilding(buildingName, buildPoint, this, playingArea);
		}

	}

	public override void MouseClick (GameObject hitObject, Vector3 hitPoint, Player controller)
	{
		base.MouseClick(hitObject, hitPoint, controller);

		if (player && player.human) 
		{
			if (hitObject.name != "Ground")
			{
				Building building = hitObject.transform.parent.GetComponent< Building >();
				if (building && !building.isBuilt())
				{
					SetBuilding(building);
				}
			}
			else
			{
				StopBuilding();
			}
		}
	}
}
