using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCrea : MonoBehaviour
{

    public string file = null;
    public int modele = 0;

    private List<string> res = new List<string>();
    private List<GameObject> spheres = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = (GameObject)Resources.Load("Graph");
        GameObject graph = GameObject.Instantiate(prefab,new Vector3(-15,-12,53),Quaternion.identity);

        GameObject sphere = (GameObject)Resources.Load("SpherePrefab");

        System.IO.StreamReader fileT = new System.IO.StreamReader(file);

        string line;

        while ((line = fileT.ReadLine()) != null)
        {
            System.Console.WriteLine(line);
            res.Add(line);
        }

        for(int i = 0;i<res.Count;i+=44)
        {
            convert(res[i])
            //spheres.Add(GameObject.Instantiate(sphere, new Vector3(-15+res[i], -12 + res[i+1], 53 + res[i+2]), Quaternion.identity));
            //Debug.Log(double.Parse(res[i].Split('\n')[0]));
        }

    }
    
    double convert(string temp)
    {
        double res;
        double before;
        double after;
        bool virgule=false;
        for(int i=0;i<temp.Count;i++)
        {
            double temp;
            if(virgule)
                temp = after;
            else
                temp = before;
            if(temp[i]=='0')
            {
                temp*=10;
            }
            else if(temp[i]=='1')
            {
                temp*=10;
                temp+=1;
            }
            else if(temp[i]=='2')
            {
            
            }
            else if(temp[i]=='3')
            {
            
            }
            else if(temp[i]=='4')
            {
            
            }
            else if(temp[i]=='5')
            {
            
            }
            else if(temp[i]=='6')
            {
            
            }
            else if(temp[i]=='7')
            {
            
            }
            else if(temp[i]=='8')
            {
            
            }
            else if(temp[i]=='9')
            {
            
            }
            else if(temp[i]==',')
            {
                virgule = true;
            }
            else if(temp[i]=='.')
            {
                virgule = true;
            }
        }
        return res;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
