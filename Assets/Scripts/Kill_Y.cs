using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Y : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //gameover
        }
        else
        {
            Destroy(collision.transform.gameObject);
        }
    }
}
