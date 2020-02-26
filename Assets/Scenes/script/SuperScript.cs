using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SuperScript : MonoBehaviour
{
	public SteamVR_Action_Boolean touchPadTouch;
	public SteamVR_Action_Boolean zoomer;
	public SteamVR_Action_Boolean boomer;
	public SteamVR_Action_Boolean switchA;
	public SteamVR_Action_Boolean switchB;
	
	public double coeffCara = 6;
	
	public double coeffSect = 6;
	
    public Vector3 origine = new Vector3(-1.57f, 0.7045f, 6.94f);

    private GameObject gameObject = null;

    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = (GameObject)Resources.Load("ObjetPrincipal");
        gameObject = GameObject.Instantiate(prefab, origine, Quaternion.identity);
		
		TestCrea testCrea = (TestCrea)gameObject.GetComponent("TestCrea");
		
		testCrea.setCoeffSectCara(coeffCara,coeffSect);
		
		testCrea.setSteamVR(touchPadTouch,zoomer,boomer,switchA,switchB);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
