using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : MonoBehaviour
{
	
	private bool selectionne = false;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
