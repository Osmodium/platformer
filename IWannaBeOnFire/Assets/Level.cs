using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
    private bool isEnabled = true;
    public void Start()
    {
        Disable();
    }

    public void Enable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        isEnabled = true;
    }

    public void Disable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        isEnabled = false;
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        //if (!isEnabled)
        //{
            if (collider.CompareTag("Player"))
            {
                Enable();
            }
        //}
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (isEnabled)
        {
            if (collider.CompareTag("Player"))
            {
                Disable();
            }
        }
    }
}
