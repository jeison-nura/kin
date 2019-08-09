using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Vector3 _MovementDirection = Vector3.zero;
    private Coroutine _CurrentChanger = null;

    [HideInInspector]
    public BubbleManager _BubbleManager = null;

    private void OnEnable()
    {
        _CurrentChanger = StartCoroutine( DirectionChanger() );
    }

    private void OnDisable()
    {
        StopCoroutine( _CurrentChanger );
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive( false );
    }
  
    // Update is called once per frame
    void Update()
    {
        transform.position += _MovementDirection * Time.deltaTime * 0.5f;
    }

    private IEnumerator DirectionChanger() {
        while (gameObject.activeSelf) {
            _MovementDirection = new Vector2(Random.Range(0,100) * 0.01f, Random.Range(0, 100) * 0.01f);

            yield return new WaitForSeconds(3.0f);
        }
    }
}
