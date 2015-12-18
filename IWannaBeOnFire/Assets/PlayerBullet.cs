using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public LayerMask ignoreMask;
    public bool isActive = false;
    private float distance;
    private Vector2 startPoint;

    public void Shoot(Vector2 position)
    {
        //startPoint = position;
        isActive = true;
    }

    public void Start()
    {
        distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, 0)));
        startPoint = transform.position;
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
    }
	
	public void Update () 
    {
	    //transform.position += transform.up * Speed * Time.deltaTime;
        //Debug.Log(distance + " : " + Vector2.Distance(startPoint, transform.position));
	    //if (isActive)
	    //{
	        if (Vector2.Distance(startPoint, transform.position) > distance)
	            //isActive = false;
	            Destroy(gameObject);
	    //}
	    
        //gameObject.SetActive(isActive);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((ignoreMask.value & 1 << other.gameObject.layer) == 0)
        {
            Destroy(gameObject);
            //isActive = false;
        }
    }
}
