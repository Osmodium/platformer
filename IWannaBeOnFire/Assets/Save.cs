using UnityEngine;
using System.Collections;

public class Save : MonoBehaviour
{
    public Sprite[] randomFaces;
    private PlayerController pc = null;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            if(pc == null)
                pc = FindObjectOfType<PlayerController>();
            if (pc != null)
            {
                pc.Save();
                SpriteRenderer spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
                int randomFaceIndex = Random.Range(0, randomFaces.Length - 1);
                spriteRenderer.sprite = randomFaces[randomFaceIndex];
            }
        }
    }
}
