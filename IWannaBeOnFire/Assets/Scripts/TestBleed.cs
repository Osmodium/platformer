using UnityEngine;
using System.Collections;

public class TestBleed : MonoBehaviour 
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            //collision.
        }
    }

}
