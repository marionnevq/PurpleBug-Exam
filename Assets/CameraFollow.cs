using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    [SerializeField] private Vector2 followOffset;
    private Vector2 threshold;

    private Rigidbody2D theRB;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        threshold = CalculateThreshold();
        theRB = followObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 follow = followObject.transform.position;
        float xDistance = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x );
        float yDistance = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position; 
        if(Mathf.Abs(xDistance) >= threshold.x)
        {
            newPosition.x = follow.x;
        }

        if(Mathf.Abs(yDistance) >= threshold.y)
        {
            newPosition.y = follow.y;
        }

        float moveSpeed = theRB.velocity.magnitude > speed ? theRB.velocity.magnitude : speed;

        Vector3 finalPos = Vector3.MoveTowards(transform.position,newPosition, moveSpeed * Time.deltaTime);

        transform.position = new Vector3(finalPos.x,finalPos.y,finalPos.z);
        
    }

    private Vector3 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y-= followOffset.y;
        return t;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));    
    }
}
