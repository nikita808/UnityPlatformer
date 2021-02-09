#region

using UnityEngine;

#endregion

public class Collectables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.IncreaseCherries();
            Destroy(gameObject);
        }
    }
}