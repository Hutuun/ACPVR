using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : MonoBehaviour
{
	
	private bool selectionne = false;
	
	TestCrea testCrea = null;
	
	GameObject moi = null;
	
    // Start is called before the first frame update
    void Start()
    {
        moi = transform.parent.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			if(selectionne)
			{
				Debug.Log("Pressed primary button.");
				testCrea.reset();
			}
		}
    }
	
	public void setTestCrea(TestCrea testCrea1)
	{
		this.testCrea = testCrea1;
	}
	
	public void setMoi(GameObject me)
	{
		moi = me;
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
	
}
