using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleObjectScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float x = 0, y = 0;
    public GameObject player1, player2;
    public Camera cam;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: need a bit more work on the camera sizing 
        float width = player1.transform.position.x - player2.transform.position.x;
        x = player1.transform.position.x - (width)/2;
        y = Mathf.Max(player1.transform.position.y,player2.transform.position.y);
        cam.orthographicSize = Mathf.Abs(width) * (14.0f/18.0f);
        transform.position = new Vector3(x, y, 0);
        //Debug.Log($"middle object: x:{x},y:{y}");
    }
}
