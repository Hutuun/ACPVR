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
            
            //spheres.Add(GameObject.Instantiate(sphere, new Vector3(-15+res[i], -12 + res[i+1], 53 + res[i+2]), Quaternion.identity));
            Debug.Log(convert(res[i]));
        }

    }
    
    double convert(string str)
    {
        double res=0;
        double before=0;
        double after=0;
        bool virgule=false;
        for(int i=0;i<str.Length;i++)
        {
            double temp;
            if(virgule)
                temp = after;
            else
                temp = before;
            if(str[i]=='0')
            {
                temp*=10;
            }
            else if(str[i]=='1')
            {
                temp*=10;
                temp+=1;
            }
            else if(str[i]=='2')
            {
                temp*=10;
                temp+=2;
            }
            else if(str[i]=='3')
            {
                temp*=10;
                temp+=3;
            }
            else if(str[i]=='4')
            {
                temp*=10;
                temp+=4;
            }
            else if(str[i]=='5')
            {
                temp*=10;
                temp+=5;
            }
            else if(str[i]=='6')
            {
                temp*=10;
                temp+=6;
            }
            else if(str[i]=='7')
            {
                temp*=10;
                temp+=7;
            }
            else if(str[i]=='8')
            {
                temp*=10;
                temp+=8;
            }
            else if(str[i]=='9')
            {
                temp*=10;
                temp+=9;
            }
            else if(str[i]==',')
            {
                virgule = true;
            }
            else if(str[i]=='.')
            {
                virgule = true;
            }
            Debug.Log(temp);
            if(virgule)
                after = temp;
            else
                before = temp;
        }
        while(after>0)
        {
            after/=10;
        }
        //Debug.Log(before);
        res = before + after;
        return res;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
