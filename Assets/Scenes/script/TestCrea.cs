﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCrea : MonoBehaviour
{

    public string file = null;
    public int modele = 0;
	public Vector3 centreGraphCara = new Vector3(-6.055751f, 6.14f, 13.75607f);
	
	public Vector3 centreGraphSect = new Vector3(-6.055751f, 6.14f, 13.75607f);

    private List<string> resCara = new List<string>();
    private GameObject graphCara;
	private List<string> namesCara = new List<string>();
    private List<GameObject> spheresCara = new List<GameObject>();
	
	private List<string> resSect = new List<string>();
	private GameObject graphSect;
    private List<string> namesSect = new List<string>();
    private List<GameObject> spheresSect = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        creationCaracteristique();
		creationSecteur();
    }
	
	void creationSecteur()
	{
		GameObject prefab = (GameObject)Resources.Load("Graph");
        graphSect = GameObject.Instantiate(prefab, centreGraphSect, Quaternion.identity);

        GameObject sphere = (GameObject)Resources.Load("SpherePrefab");

        System.IO.StreamReader fileT = new System.IO.StreamReader(file + "corvar.txt");
        System.IO.StreamReader fileName = new System.IO.StreamReader(file + "secteur.txt");
	}
	
	void creationCaracteristique()
	{
		GameObject prefab = (GameObject)Resources.Load("Graph");
        graphCara = GameObject.Instantiate(prefab, centreGraph, Quaternion.identity);

        GameObject sphere = (GameObject)Resources.Load("SpherePrefab");

        System.IO.StreamReader fileT = new System.IO.StreamReader(file + "corvar.txt");
        System.IO.StreamReader fileName = new System.IO.StreamReader(file + "caracteristique.txt");

        string line;

        while ((line = fileT.ReadLine()) != null)
        {
            System.Console.WriteLine(line);
            res.Add(line);
        }

        while ((line = fileName.ReadLine()) != null)
        {
            System.Console.WriteLine(line);
            names.Add(line);
        }

        for (int i = 0; i < res.Count; i += 44)
        {

            spheres.Add(GameObject.Instantiate(sphere, new Vector3(centreGraph.x + (float)convert(res[i]) * 7.5f, centreGraph.y + (float)convert(res[i + 1]) * 7.5f, centreGraph.z + (float)convert(res[i + 2]) * 7.5f), Quaternion.identity));
            Debug.Log(convert(res[i]));
        }

        for(int i = 0; i<spheres.Count;i++)
        {
            TextMesh temp = spheres[i].GetComponentInChildren<TextMesh>();

            temp.text = names[i];
        }
	}
    
    double convert(string str)
    {
        double before=0;
        double after=0;
        int nbZero = -1;
        bool virgule=false;
        bool zero=true;
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
                if(zero)
                    nbZero++;
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
            //Debug.Log(temp);
            if(temp!=0)
                if(virgule && zero)
                    zero=false;
            if(virgule)
                after = temp;
            else
                before = temp;
        }
        while(after>1)
        {
            after/=10;
        }
        while(nbZero!=0)
        {
            nbZero--;
            after/=10;
        }
        //Debug.Log(before);
        //Debug.Log(after);
        return before + after;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
