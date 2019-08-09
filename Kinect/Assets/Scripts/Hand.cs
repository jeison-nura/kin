using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform _HandMesh;
    
    // Update is called once per frame
    void Update()
    {
        _HandMesh.position = Vector3.Lerp(_HandMesh.position, transform.position, Time.deltaTime * 15.0f);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Bubble"))
            return;

        
        collision.gameObject.SetActive(false);
    }
}
