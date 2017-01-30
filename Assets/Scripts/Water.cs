﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Water : MonoBehaviour {

	void reflection()
	{
		// Create an empty gameobject
		GameObject emptyGameObject = new GameObject ();
		GameObject reflectionObject = Instantiate(emptyGameObject,Vector3.zero,Quaternion.identity) as GameObject;

		// Mirror the new object on the y axis and move in to position
		SpriteRenderer originalRenderer = GetComponent<SpriteRenderer> ();
		float ySize = originalRenderer.sprite.bounds.size.y;
		reflectionObject.transform.parent = transform;
		reflectionObject.transform.localScale = new Vector3(1 * Mathf.Sign(transform.localScale.x),-1,1);
		reflectionObject.transform.localPosition = new Vector3(0,-ySize,0);

		// Copy the sprite renderer
		reflectionObject.AddComponent<SpriteRenderer>();
		SpriteRenderer renderer = reflectionObject.GetComponent<SpriteRenderer> ();
		renderer.sprite = originalRenderer.sprite;
		renderer.sortingLayerID = originalRenderer.sortingLayerID;

		// Put our reflection material on the new object
		Material newMat = Resources.Load("Shaders/WaterReflection", typeof(Material)) as Material;
		renderer.material = newMat;
	}
}
