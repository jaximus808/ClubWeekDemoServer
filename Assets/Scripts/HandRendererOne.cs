using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRendererOne : MonoBehaviour
{
    
    public float translateMove; 
    public float scale;
    public float connectorWidth;

    public GameObject LandMark; 
    public GameObject Connector;
    private Transform[] connectorList = new Transform[23];
    private Transform[] landmarkPos = new Transform[21];
    public Vector3 Offset = new Vector3(0f, 0f, 0f); //test for now

    private int[] previousAngle = new int[4] { 0, 0, 0, 0 };
    private int[] currentDifference = new int[4] { 0, 0, 0, 0 };
    private bool updated = false; 
    private Vector3[] buffer;
    private int currentConnector = 0;


    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < 23; i++)
        {
            connectorList[i] = Instantiate(Connector, transform.position,Quaternion.identity, transform).transform;
            connectorList[i].gameObject.name = $"Connector: {i}";
        }
        for(int i = 0; i < 21; i++)
        {
            landmarkPos[i] = Instantiate(LandMark, transform.position,Quaternion.identity,transform).transform;
            landmarkPos[i].gameObject.name = $"LandMark: {i}";
            //landmarkPos[i].position = 50f;
        }
    }


    // Update is called once per frame
    public void UpdateHands(Vector3[] newPos)
    {
        updated = true; 
        
        buffer = newPos;
        
    }

    public void PrintDifference()
    {
    }

    public int[] CalculateRotationJoints()
    {
        Debug.Log("Start");
        for(int i = 0; i < 4; i++)
        {
            float c = Vector3.Distance(landmarkPos[5+i*4].position, landmarkPos[6 + i * 4].position);
            float b = Vector3.Distance(landmarkPos[5+i*4].position, landmarkPos[0].position);
            float a = Vector3.Distance(landmarkPos[6 + i * 4].position, landmarkPos[0].position);

            float ang = Mathf.Acos((b*b + c*c -a*a )/(2*b*c)); 

            previousAngle[i] = (int)(((Mathf.Rad2Deg*(ang)-80f)/90 *360 ));
            Debug.Log(previousAngle[i]);
        }

        Debug.Log("End");
        return previousAngle; 
    }

    private void connectJoints(int pos, int start, int target)
    {
        connectorList[pos].localScale = new Vector3(connectorWidth,connectorWidth,Vector3.Distance(landmarkPos[start].position,landmarkPos[target].position)/2);

        connectorList[pos].position = landmarkPos[start].position;
        connectorList[pos].LookAt(landmarkPos[target]);
        currentConnector++;
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward*-1*translateMove*Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward*translateMove*Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left*-1*translateMove*Time.fixedDeltaTime);
        
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.left*translateMove*Time.fixedDeltaTime);
        }
    }

    private void RenderConnector()
    {
        currentConnector = 0; 
        for(int i = 0; i < 4; i++)
        {
            connectJoints(currentConnector, i,i+1);
        }

        connectJoints(currentConnector, 1, 5);
        for(int i = 0; i < 3; i++)
        {

            connectJoints(currentConnector, 5+i, 6+i);
        }

        connectJoints(currentConnector, 5, 9);
        for(int i = 0; i < 3; i++)
        {

            connectJoints(currentConnector, 9+i, 10+i);
        }

        connectJoints(currentConnector, 9, 13);
        for(int i = 0; i < 3; i++)
        {

            connectJoints(currentConnector, 13+i, 14+i);
        }
        
        connectJoints(currentConnector, 13, 17);
        for(int i = 0; i < 3; i++)
        {

            connectJoints(currentConnector, 17+i, 18+i);
        }

        connectJoints(currentConnector, 0, 17);

        connectJoints(currentConnector, 2, 5);

        connectJoints(currentConnector, 0, 9);
    }

    private void FixedUpdate()
    {
        if(!updated) return;
        updated = false; 
        for(int i = 0; i < 21; i++)
        {
            //Debug.Log("balls");
            //Debug.Log(buffer[i]);
            //Debug.Log(transform.position + scale * new Vector3(buffer[i].x, buffer[i].y * -1, buffer[i].z));
            landmarkPos[i].position = transform.position+scale* new Vector3(buffer[i].x, buffer[i].y*-1, buffer[i].z);
            //Debug.Log(landmarkPos[i].position);
        }
        CalculateRotationJoints();
        RenderConnector();
        Movement();
        
        
    }
}
