using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deathRoutine());
    }
    IEnumerator deathRoutine() {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
