using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadOnTimer : MonoBehaviour {

    float delay = 1;
    float timer;

	// Use this for initialization
	void Start () {
        timer = Time.time + delay;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer < Time.time)
            Application.LoadLevel(0);
	}
}
