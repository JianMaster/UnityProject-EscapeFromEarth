using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvGenerate : MonoBehaviour {
    public Forest forest1;
    public Forest forest2;

    public GameObject[] forests;
    public int forestCount = 2;

    void Update () {
		
	}

    public void Generate()
    {
        ++forestCount;
        int type = Random.Range(0, 3);
        GameObject newGameobject=Instantiate(forests[type], new Vector3(0f, 0f, forestCount * 3000f),Quaternion .identity )as GameObject ;
        forest1 = forest2;
        forest2 = newGameobject.GetComponent<Forest>();
    }
}
