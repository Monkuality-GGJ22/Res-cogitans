using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Y : MonoBehaviour
{
    [SerializeField] private RespawnManager respawnManager;

    private void Start()
    {
        if (respawnManager == null) respawnManager = FindObjectOfType<RespawnManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            respawnManager.RespawnPlayer(true);
        }
        else if (collision.transform.CompareTag("Enemy"))
        {
            SearchAndDestroy var = collision.gameObject.GetComponent<SearchAndDestroy>();
            if (var != null) {
                var.dieAndIncreaseLight();
            }
        }
        else

        {
            var resettable = collision.gameObject.GetComponent<Resettable>();
            if (resettable != null)
            {
                resettable.Respawn();
            }
            else
            {
                Destroy(collision.transform.gameObject);
            }
        }
    }
}
