using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    //public bool isActive = true;
    public Button buttonActivation;
    public Vector2[] travelPosition;
    public int nextStop = 1;
    public int lastStop = 0;
    public float timeBetweenStops = 500.0f;
    private float currentLerpTime = 0.0f;
    private PlayerController pc;
    private Vector2 previousLocation;

	public void Start ()
	{
	    transform.position = travelPosition[lastStop];
	}
	
	public void Update ()
    {
        if(buttonActivation != null && !buttonActivation.on)
            return;

	    if (currentLerpTime > timeBetweenStops)
	    {
	        lastStop = nextStop;
	        nextStop++;
	        currentLerpTime = 0;
	        if (nextStop > travelPosition.Length - 1)
	        {
	            nextStop = 0;
	        }
	    }

	    currentLerpTime += Time.deltaTime;
        float t = currentLerpTime / timeBetweenStops;
	    t = t*t*t*(t*(6f*t - 15f) + 10f);

	    previousLocation = transform.position;
	    transform.position = Vector2.Lerp(travelPosition[lastStop], travelPosition[nextStop], t);
	    
        if (pc && pc.isOnElevator)
	    {
	        pc.elevatorXSpeed = transform.position.x - previousLocation.x;
	        pc.elevatorYSpeed = transform.position.y - previousLocation.y;
	    }
	}

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            pc = collider.GetComponent<PlayerController>() as PlayerController;
            pc.isOnElevator = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            pc = collider.GetComponent<PlayerController>() as PlayerController;
            pc.isOnElevator = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (Vector2 pos in travelPosition)
        {
            Gizmos.DrawWireSphere(pos, 0.1f);
        }
    }
}
