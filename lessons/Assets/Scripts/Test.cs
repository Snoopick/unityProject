using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject m_redCube;
    private GameObject redCube;
    
    void Start()
    {       
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        redCube = Instantiate(m_redCube);
        redCube.transform.position = new Vector3(1.0f, 1.0f, 1.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = redCube.transform.position;
        position.y += 0.1f;
        redCube.transform.position = position;
    }
}
