using UnityEngine;
using System.Collections;

public class Kill : MonoBehaviour 
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.tag);
        if (collider.tag.Equals("Player"))
        {
            PlayerController playerController = collider.GetComponentInChildren<PlayerController>() as PlayerController;
            playerController.Die();

            //PlayerControlManual playerControlManual =
            //    collider.GetComponentInChildren<PlayerControlManual>() as PlayerControlManual;
            //playerControlManual.Die();

            //PlayerControl playerControl = collider.GetComponentInChildren<PlayerControl>() as PlayerControl;
            //playerControl.Die();
        }
    }
}
