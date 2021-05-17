using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class par_autoend : MonoBehaviour {
	ParticleSystem parsys;

	// Use this for initialization
	void Start () {
		parsys = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!parsys.IsAlive()){
			GameObject.DestroyObject(this.gameObject);
		}
		Debug.Log(parsys);
		Debug.Log(parsys.IsAlive());		
	}
}

