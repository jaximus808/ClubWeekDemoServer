using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float setSpawnCooldown;
    private float spawnCooldown;
    
    public GameObject cube; 

    void Start()
    {
        spawnCooldown = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && spawnCooldown <= 0)
        {
            Instantiate(cube, transform);
            spawnCooldown = setSpawnCooldown;
        }
        if(Input.GetKey(KeyCode.P))
        {
            foreach(Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        if(spawnCooldown > 0)
        {
            spawnCooldown -= Time.deltaTime; 
        }
    }
}
