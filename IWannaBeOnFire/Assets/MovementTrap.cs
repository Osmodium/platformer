using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MovementTrap : MonoBehaviour
{
    public Transform[] transforms;
    public Vector2 target;
    public float travelTime = 0.5f;
    public bool reset = true;
    public float resetTime = 3.0f;

    private Vector2[] startPositions;
    private float currentLerpTime = 0.0f;

    private bool exceute = false;
    private bool doReset = false;

    public void Start()
    {
        startPositions = new Vector2[transforms.Length];
        for (int i = 0; i < transforms.Length; ++i)
        {
            startPositions[i] = new Vector2(transforms[i].position.x, transforms[i].position.y);
            //Debug.Log(startPositions[i]);
        }
    }

    public void Update()
    {
        if (exceute)
        {
            if ((currentLerpTime > travelTime && exceute && !doReset) || (currentLerpTime > resetTime && exceute && doReset))
            {
                currentLerpTime = 0f;

                if (exceute && doReset)
                {
                    doReset = false;
                    exceute = false;
                    for (int i = 0; i < transforms.Length; ++i)
                    {
                        transforms[i].position = startPositions[i];
                    }
                }
                else if (exceute)
                    doReset = true;
            }

            currentLerpTime += Time.deltaTime;

            if (exceute && doReset)
            {
                float t = currentLerpTime / resetTime;
                for (int i = 0; i < transforms.Length; ++i)
                {
                    transforms[i].position = Vector2.Lerp(target, startPositions[i], t);
                }
            }
            else if(exceute && !doReset)
            {

                float t = currentLerpTime / travelTime;
                for (int i = 0; i < transforms.Length; ++i)
                {
                    transforms[i].position = Vector2.Lerp(startPositions[i], target, t);
                }
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && exceute == false)
        {
            if (!exceute && !doReset)
                exceute = true;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target, 0.1f);
        Gizmos.color = Color.green;
        if (startPositions != null)
        {
            foreach (Vector2 startPosition in startPositions)
            {
                Gizmos.DrawWireSphere(startPosition, 0.1f);
            }
        }
        Gizmos.color = Color.magenta;
        BoxCollider2D boxCollider = GetComponentInChildren<BoxCollider2D>() as BoxCollider2D;
        if(boxCollider != null)
            Gizmos.DrawWireCube(boxCollider.offset + (Vector2)transform.position, boxCollider.size);
    }

}
