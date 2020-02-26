using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperScript : MonoBehaviour
{
    public Vector3 origine = new Vector3(-1.57f, 0.7045f, 6.94f);

    private GameObject gameObject = null;

    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = (GameObject)Resources.Load("ObjetPrincipal");
        gameObject = GameObject.Instantiate(prefab, origine, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
