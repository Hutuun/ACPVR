using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hello : MonoBehaviour
{
	private bool first = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(!first)
		{
			first = true;
			if(Input.anyKey)
			{
				transform.gameObject.SetActive(false);
			}
		}
    }
}
