using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCrea : MonoBehaviour
{

    public string file = null;
    public int modele = 0;

    private List<int> res = new List<int>();
    private List<GameObject> spheres = List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = (GameObject)Resources.Load("Graph");
        GameObject graph = GameObject.Instantiate(prefab,new Vector3(-15,-12,53),Quaternion.identity);

        GameObject sphere = (GameObject)Resources.Load("SpherePrefab");

        System.IO.StreamReader fileT = new System.IO.StreamReader(file);

        while ((line = fileT.ReadLine()) != null)
        {
            System.Console.WriteLine(line);
            res.Add(Int32.Parse(line));
        }

        for(int i = 0;i<res.Count;i+=44)
        {
            spheres.Add(GameObject.Instantiate(sphere, new Vector3(-15+res[i], -12 + res[i+1], 53 + res[i+2]), Quaternion.identity));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
