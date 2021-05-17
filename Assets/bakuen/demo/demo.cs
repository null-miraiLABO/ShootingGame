using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demo : MonoBehaviour {

	public GameObject bakuPrefab;
	// Use this for initialization
	void Start () {
		GameObject bakupre = GameObject.Instantiate(bakuPrefab);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

