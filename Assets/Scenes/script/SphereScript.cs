using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    public int modele = 0;
	public double coeffCara = 6;
	public double coeffSect = 6;
	public double coeffTaille = 9;
	
	public Vector3 centreGraphCara = new Vector3(-6.055751f, 6.14f, 13.75607f);
	
	public Vector3 centreGraphSect = new Vector3(-6.055751f, 6.14f, 13.75607f);
	
	private string file = "Python/InfoSecteur/";

    private List<string> resCara = new List<string>();
    private List<double> resCara2 = new List<double>();
	private GameObject graphCara;
	private List<string> namesCara = new List<string>();
	private List<string> namesCompleteCara = new List<string>();
    private List<GameObject> spheresCara = new List<GameObject>();
	
	private List<string> resSect = new List<string>();
	private List<double> resSect2 = new List<double>();
	private GameObject graphSect;
    private List<string> namesSect = new List<string>();
	private List<double> diSect = new List<double>();
    private List<GameObject> spheresSect = new List<GameObject>();
	
	private TestCrea papa = null;
	
	private GameObject moi = null;
	
	private bool selectionne = false;

    // Start is called before the first frame update
    void Start()
    {
		TextMesh temp = this.GetComponentInChildren<TextMesh>();
		
		file += temp.text + "/";
		
        creationCaracteristique();
		creationSecteur();
    }
	
	public void setCentre(Vector3 vecCara, Vector3 vecSect)
	{
		centreGraphCara = vecCara;
		
		centreGraphSect = vecSect;
		
		return;
	}
	
	public void setMoi(GameObject me)
	{
		moi = me;
	}
	
	public void setPapa(TestCrea parent)
	{
		papa = parent;
	}
	
	private void creationSecteur()
	{
		//GameObject prefab = (GameObject)Resources.Load("Graph");
        //graphSect = GameObject.Instantiate(prefab, centreGraphSect, Quaternion.identity);

        GameObject sphere = (GameObject)Resources.Load("SpherePrefab");

        System.IO.StreamReader fileT = new System.IO.StreamReader(file + "coord.txt");
        System.IO.StreamReader fileName = new System.IO.StreamReader(file + "secteurs.txt");
		System.IO.StreamReader fileDi = new System.IO.StreamReader(file + "di.txt");
		
		string line;
		
		while ((line = fileName.ReadLine()) != null)
        {
            //Debug.Log(line);
            namesSect.Add(line);
        }
		
		while ((line = fileDi.ReadLine()) != null)
        {
            //Debug.Log(line);
            diSect.Add(convert(line));
        }
		
		int trucTemp=0;
		
		while ((line = fileT.ReadLine()) != null)
        {
            //Debug.Log(trucTemp / 44);
            if (trucTemp / 44 <= namesSect.Count)
            {
                if (namesSect[trucTemp / 44] != "Inutilise")
                {
                    //System.Console.WriteLine(line);
                    resSect.Add(line);
                }
            }
            trucTemp++;
            //Debug.Log(trucTemp);
        }
		
		for(int i = 0; i < resSect.Count; i += 44)
		{
			resSect2.Add(convert(resSect[i]));
			resSect2.Add(convert(resSect[i+1]));
			resSect2.Add(convert(resSect[i+2]));
		}
		
		double max = maxList(resSect2);
		double maxTaille = maxList(diSect);
		
		for(int i = 0;i<resSect2.Count;i++)
		{
			resSect2[i]/=max;
			resSect2[i]*=coeffSect;
		}
		
		for (int i = 0; i < resSect2.Count; i += 3)
        {
            spheresSect.Add(GameObject.Instantiate(sphere, new Vector3(centreGraphSect.x + (float)resSect2[i], centreGraphSect.y + (float)resSect2[i+1], centreGraphSect.z + (float)resSect2[i+2]), Quaternion.identity));
			//Debug.Log(convert(resSect[i]));
        }

        for(int i = 0; i< spheresSect.Count;i++)
        {
			double temporaire = 1+coeffTaille*diSect[i]/maxTaille;
			
			Vector3 vec = spheresSect[i].transform.localScale;
			
			spheresSect[i].transform.localScale = new Vector3(vec.x*(float)temporaire,vec.y*(float)temporaire,vec.z*(float)temporaire);
			
            TextMesh temp = spheresSect[i].GetComponentInChildren<TextMesh>();

            temp.text = namesSect[i];
			
			Debug.Log(file + namesSect[i]);
			
			spheresSect[i].name = namesSect[i];
			
			spheresSect[i].SetActive(false);
        }
		
		Debug.Log(file + resSect2.Count);
		Debug.Log(file + namesSect.Count);
		Debug.Log(file + spheresSect.Count);
	}
	
	private void OnMouseOver()
	{
        Renderer renderer = (Renderer)moi.GetComponent("Renderer");
        renderer.material.color = Color.green;
		
		selectionne = true;
	}
	
	private void OnMouseExit()
	{
		Renderer renderer = (Renderer)moi.GetComponent("Renderer");
        renderer.material.color = Color.red;
		
		selectionne = false;
	}
	
	private void clique()
	{
		for(int i = 0; i< spheresSect.Count;i++)
        {
			spheresSect[i].SetActive(true);
        }
		
		for(int i = 0; i< spheresCara.Count;i++)
        {
			spheresCara[i].SetActive(true);
        }
		
		papa.setAllInvisible();
	}
	
	public void setAllInvisible()
	{
		for(int i = 0; i< spheresSect.Count;i++)
        {
			spheresSect[i].SetActive(false);
        }
		
		for(int i = 0; i< spheresCara.Count;i++)
        {
			spheresCara[i].SetActive(false);
        }
	}
	
	private void creationCaracteristique()
	{
		//GameObject prefab = (GameObject)Resources.Load("Graph");
        //graphCara = GameObject.Instantiate(prefab, centreGraphCara, Quaternion.identity);

        GameObject sphere = (GameObject)Resources.Load("SpherePrefab");

        System.IO.StreamReader fileT = new System.IO.StreamReader(file + "corvar.txt");
        System.IO.StreamReader fileName = new System.IO.StreamReader(file + "caracteristique.txt");
		//System.IO.StreamReader fileName = new System.IO.StreamReader(file + "caracteristique.txt");

        string line;

        while ((line = fileT.ReadLine()) != null)
        {
            //System.Console.WriteLine(line);
            resCara.Add(line);
        }

        while ((line = fileName.ReadLine()) != null)
        {
            //System.Console.WriteLine(line);
            namesCara.Add(line);
        }
		
		for(int i = 0; i < resCara.Count; i += 44)
		{
			resCara2.Add(convert(resCara[i]));
			resCara2.Add(convert(resCara[i+1]));
			resCara2.Add(convert(resCara[i+2]));
		}
		
		for(int i = 0;i<resCara2.Count;i++)
		{
			resCara2[i]*=coeffCara;
		}

        for (int i = 0; i < resCara2.Count; i += 3)
        {

            spheresCara.Add(GameObject.Instantiate(sphere, new Vector3(centreGraphCara.x + (float)resCara2[i], centreGraphCara.y + (float)resCara2[i+1], centreGraphCara.z + (float)resCara2[i+2]), Quaternion.identity));
            //Debug.Log(resCara2[i]);
        }

        for(int i = 0; i< spheresCara.Count;i++)
        {
            TextMesh temp = spheresCara[i].GetComponentInChildren<TextMesh>();

            temp.text = namesCara[i];
			
			spheresCara[i].SetActive(false);
        }
	}
    
    private double convert(string str)
    {
        double before=0;
        double after=0;
        int nbZero = 0;
        bool virgule=false;
		bool negatif=false;
        bool zero=false;
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
				zero = false;
            }
            else if(str[i]=='2')
            {
                temp*=10;
                temp+=2;
				zero = false;
            }
            else if(str[i]=='3')
            {
                temp*=10;
                temp+=3;
				zero = false;
            }
            else if(str[i]=='4')
            {
                temp*=10;
                temp+=4;
				zero = false;
            }
            else if(str[i]=='5')
            {
                temp*=10;
                temp+=5;
				zero = false;
            }
            else if(str[i]=='6')
            {
                temp*=10;
                temp+=6;
				zero = false;
            }
            else if(str[i]=='7')
            {
                temp*=10;
                temp+=7;
				zero = false;
            }
            else if(str[i]=='8')
            {
                temp*=10;
                temp+=8;
				zero = false;
            }
            else if(str[i]=='9')
            {
                temp*=10;
                temp+=9;
				zero = false;
            }
            else if(str[i]==',')
            {
                virgule = true;
				zero = true;
            }
            else if(str[i]=='.')
            {
                zero = true;
				virgule = true;
            }
			else if(str[i]=='-')
			{
				//Debug.Log("Negatif");
				negatif=true;
			}
            //Debug.Log(temp);
            if(str[i]!='-'&&str[i]!='.'&&str[i]!=',')
			{
				if(virgule)
					after = temp;
				else
					before = temp;
			}
        }
        while(after>1)
        {
            after/=10;
        }
		//Debug.Log(nbZero);
        while(nbZero>0)
        {
            nbZero--;
            after/=10;
        }
        //Debug.Log(before);
        //Debug.Log(after);
		if(negatif)
			return - before - after;
		return before + after;
    }
	
	private double maxList(List<double> list)
	{
		double res=double.MinValue;
		foreach(double i in list)
		{
			if(i>res)
				res = i;
		}
		return res;
	}
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			if(selectionne)
			{
				Renderer renderer = (Renderer)moi.GetComponent("Renderer");
				renderer.material.color = Color.red;
				
				Debug.Log("Pressed primary button.");
				clique();
			}
		}
	}
}
