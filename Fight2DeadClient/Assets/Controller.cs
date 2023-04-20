using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // TODO: create prefabs to automatically spawn 
    // TODO: find out a way to layer sprite together

    // Start is called beforeishida the first frame update
    private Vector3 ishidaDestination = new Vector3(-5.15f, 2.41f, 0f);
    private Vector3 venomDestination = new Vector3(4.98f, 2.34f, 0);

    private Vector3 movementVectorIshida = new Vector3(-5.15f - 11.79f, 2.41f - -4.96f);
    private Vector3 movementVectorVenom = new Vector3(4.98f - -11.5f, 2.34f - -3.61f);
    public GameObject ishida;
    public GameObject venom;

    void Start()
    {
        venom.transform.position = new Vector3(-11.5f, -3.61f, 0);
        ishida.transform.position = new Vector3(11.79f, -4.96f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        bool ishidaFinish = V3Equal(ishida.transform.position, ishidaDestination);
        bool venomFinish = V3Equal(venom.transform.position, venomDestination);
        if(!ishidaFinish)
		{
			ishida.transform.position += movementVectorIshida*Time.deltaTime;
		}
        if(!venomFinish)
		{
			venom.transform.position += movementVectorVenom*Time.deltaTime;
		}
    }
    public bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.0001;
    }
}
