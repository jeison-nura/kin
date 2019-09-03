using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{

    public GameObject _BubblePrefab;

    private List<Bubble> _AllBubble = new List<Bubble>();
    private Vector2 _BottomLeft = Vector2.zero;
    private Vector2 _TopRight = Vector2.zero;

    private void Awake()
    {

        _BottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,0, Camera.main.farClipPlane));
        _TopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 
                                                    Camera.main.pixelHeight/2, Camera.main.farClipPlane));
        Debug.Log(_BottomLeft);
    }

    void Start()
    {
        StartCoroutine(CreateBubbles());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(0, 0, Camera.main.farClipPlane), 0.5f);
        Gizmos.DrawSphere(new Vector3(Camera.main.pixelWidth,
                                      Camera.main.pixelHeight / 2, Camera.main.farClipPlane), 0.5f);
    }

    public Vector3 GetPlanePosition() {
        float targetX = Random.Range(_BottomLeft.x, _TopRight.x);
        float targetY = Random.Range(_BottomLeft.y, _TopRight.y);

        return new Vector3(targetX, targetY, 0)/80;
    }

    private IEnumerator CreateBubbles() {

        while ( _AllBubble.Count < 20 ) {

            GameObject newBubbleObject = Instantiate(_BubblePrefab, GetPlanePosition(), Quaternion.identity, transform);
            Bubble newBubble = newBubbleObject.GetComponent<Bubble>();

            newBubble._BubbleManager = this;
            _AllBubble.Add(newBubble);

            yield return new WaitForSeconds(0.5f);

        }

    }
}
