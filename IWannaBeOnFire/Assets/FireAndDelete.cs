using UnityEngine;
using System.Collections;

public class FireAndDelete : MonoBehaviour 
{

	public void Start()
    {
	    GetComponent<ParticleSystem>().Play();
	}
	
	public void Update()
    {
	    if(GetComponent<ParticleSystem>().isStopped)
            DestroyImmediate(gameObject);
    }
}
