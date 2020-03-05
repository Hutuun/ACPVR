using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TestCrea : MonoBehaviour
{
	public SteamVR_Action_Boolean touchPadTouch;
	public SteamVR_Action_Boolean zoomer;
	public SteamVR_Action_Boolean boomer;
	public SteamVR_Action_Boolean switchA;
	public SteamVR_Action_Boolean switchB;
	public SteamVR_Action_Boolean resetter;
	
    public int modele = 0;
	public double coeffCara = 6;
	
	public double coeffSectinit = 6;
	private double coeffSect = 6;
	
	public double coeffTaille = 9;
	
	public Vector3 centreGraphCara = new Vector3(-6.055751f, 6.14f, 13.75607f);
	
	public Vector3 centreGraphSect = new Vector3(-6.055751f, 6.14f, 13.75607f);

	private string file = "Python/Secteur/";

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
	
	private bool first = false;
	
	private int courant = 0;

    // Start is called before the first frame update
    void Start()
    {
		coeffSect = coeffSectinit;
        creationCaracteristique();
		creationSecteur();
		setAllInvisible();
    }
	
	public void setCoeffSectCara(double coeffCara1, double coeffSect1)
	{
		coeffCara = coeffCara1;
		coeffSectinit = coeffSect1;
	}
	
	public void setSteamVR(SteamVR_Action_Boolean touchPadTouc, SteamVR_Action_Boolean zoome, SteamVR_Action_Boolean boome,SteamVR_Action_Boolean switch1,SteamVR_Action_Boolean switch2,SteamVR_Action_Boolean resette)
	{
		touchPadTouch = touchPadTouc;
		zoomer = zoome;
		boomer = boome;
		switchA = switch1;
		switchB = switch2;
		resetter = resette;
	}
	
	private void creationSecteur()
	{
		GameObject prefab = (GameObject)Resources.Load("Graph");
        graphSect = GameObject.Instantiate(prefab, centreGraphSect, Quaternion.identity);

        GameObject sphere = (GameObject)Resources.Load("SpherePrefabSecteur");

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
            if (trucTemp / 44 < namesSect.Count)
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
            //spheresSect.Add(GameObject.Instantiate(sphere, new Vector3(centreGraphSect.x + (float)resSect2[i], centreGraphSect.y + (float)resSect2[i+1], centreGraphSect.z + (float)resSect2[i+2]), Quaternion.identity));
			//Debug.Log(convert(resSect[i]));
        }

		//Debug.Log("Sphere" + spheresSect.Count);
		//Debug.Log("Sect" + diSect.Count);

        for(int i = 0; i< spheresSect.Count;i++)
        {	
			double temporaire = 1+coeffTaille*diSect[i]/maxTaille;
			
			Vector3 vec = spheresSect[i].transform.localScale;
			
			spheresSect[i].transform.localScale = new Vector3(vec.x*(float)temporaire,vec.y*(float)temporaire,vec.z*(float)temporaire);
			
            TextMesh temp = spheresSect[i].GetComponentInChildren<TextMesh>();

            temp.text = namesSect[i];
			
			spheresSect[i].name = namesSect[i];

            SphereScript sphereScript = (SphereScript)spheresSect[i].GetComponent("SphereScript");

            sphereScript.setCentre(centreGraphCara,centreGraphSect);
			
			sphereScript.setPapa(this);
			
			sphereScript.setSteamVR(touchPadTouch,zoomer,boomer);
			
			sphereScript.setMoi(spheresSect[i]);
			
			sphereScript.setCoeffSectCara(coeffCara,coeffSect);
			
			//spheresSect[i].SetActive(false);
        }
	}
	
	public void reset()
	{
		for(int i = 0; i< spheresSect.Count;i++)
        {
			SphereScript sphereScript = (SphereScript)spheresSect[i].GetComponent("SphereScript");
			
			sphereScript.setAllInvisible();
        }
		
		setAllVisible();
		
		coeffSect = coeffSectinit;
		
		double max = maxList(resSect2);
		
		for(int i = 0;i<resSect2.Count;i++)
		{
			resSect2[i]/=max;
			resSect2[i]*=coeffSect;
		}
		
		for (int i = 0; i < spheresSect.Count; i ++)
        {
			spheresSect[i].transform.SetPositionAndRotation(new Vector3(centreGraphSect.x + (float)resSect2[i*3], centreGraphSect.y + (float)resSect2[i*3+1], centreGraphSect.z + (float)resSect2[i*3+2]),Quaternion.identity);
			
			SphereScript sphereScript = (SphereScript)spheresSect[i].GetComponent("SphereScript");
			
			sphereScript.rezoom(coeffSect);
        }
	}
	
	public void zoom()
	{
		double max = maxList(resSect2);
		
		coeffSect += 6;
		
		for(int i = 0;i<resSect2.Count;i++)
		{
			resSect2[i]/=max;
			resSect2[i]*=coeffSect;
		}
		
		for (int i = 0; i < spheresSect.Count; i ++)
        {
			spheresSect[i].transform.SetPositionAndRotation(new Vector3(centreGraphSect.x + (float)resSect2[i*3], centreGraphSect.y + (float)resSect2[i*3+1], centreGraphSect.z + (float)resSect2[i*3+2]),Quaternion.identity);
			
			SphereScript sphereScript = (SphereScript)spheresSect[i].GetComponent("SphereScript");
			
			sphereScript.rezoom(coeffSect);
        }
	}
	
	public void dezoom()
	{
		if(!(coeffSect <= 6))
		{
			double max = maxList(resSect2);
			
			coeffSect -= 6;
		
			for(int i = 0;i<resSect2.Count;i++)
			{
				resSect2[i]/=max;
				resSect2[i]*=coeffSect;
			}
		
			for (int i = 0; i < spheresSect.Count; i ++)
			{
				spheresSect[i].transform.SetPositionAndRotation(new Vector3(centreGraphSect.x + (float)resSect2[i*3], centreGraphSect.y + (float)resSect2[i*3+1], centreGraphSect.z + (float)resSect2[i*3+2]),Quaternion.identity);
				
				SphereScript sphereScript = (SphereScript)spheresSect[i].GetComponent("SphereScript");
			
				sphereScript.rezoom(coeffSect);
			}
		}
	}
	
	public void switchAvant()
	{
		SphereScript sphereScript = (SphereScript)spheresSect[courant].GetComponent("SphereScript");
		
		sphereScript.OnPointerOut();
		
		if(courant < spheresSect.Count-1)
		{
			courant++;
		}
		else
		{
			courant = 0;
		}
		
		sphereScript = (SphereScript)spheresSect[courant].GetComponent("SphereScript");
		
		sphereScript.OnPointerIn();
	}
	
	public void switchArriere()
	{
		SphereScript sphereScript = (SphereScript)spheresSect[courant].GetComponent("SphereScript");
		
		sphereScript.OnPointerOut();
		
		if(courant > 0)
		{
			courant--;
		}
		else
		{
			courant = spheresSect.Count-1;
		}
		
		sphereScript = (SphereScript)spheresSect[courant].GetComponent("SphereScript");
		
		sphereScript.OnPointerIn();
	}
	
	public void setAllVisible()
	{
		for(int i = 0; i< spheresSect.Count;i++)
        {
			spheresSect[i].SetActive(true);
        }
		
		for(int i = 0; i< spheresCara.Count;i++)
        {
			spheresCara[i].SetActive(true);
        }
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
		GameObject prefab = (GameObject)Resources.Load("Graph");
        graphCara = GameObject.Instantiate(prefab, centreGraphCara, Quaternion.identity);

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
	
	
	private void clique()
	{
		SphereScript sphere = (SphereScript)spheresSect[courant].GetComponent("SphereScript");
        sphere.clique();
	}


    public SteamVR_Input_Sources thisHand;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
		{
			zoom();
		}
		if (Input.GetKey(KeyCode.O))
		{
			dezoom();
		}
		if(Input.anyKey)
		{
			if(!first)
			{
				setAllVisible();
				first = true;
			}
		}
		if(touchPadTouch.GetStateDown(SteamVR_Input_Sources.LeftHand))
		{
			clique();
		}
		if(zoomer.GetStateDown(SteamVR_Input_Sources.RightHand))
		{
			zoom();
		}
		if(boomer.GetStateDown(SteamVR_Input_Sources.RightHand))
		{
			dezoom();
		}
		if(switchA.GetStateDown(SteamVR_Input_Sources.RightHand))
		{
			switchAvant();
		}
		if(switchB.GetStateDown(SteamVR_Input_Sources.LeftHand))
		{
			switchArriere();
		}
		if(resetter.GetStateDown(SteamVR_Input_Sources.RightHand))
		{
			reset();
		}
    }
}
