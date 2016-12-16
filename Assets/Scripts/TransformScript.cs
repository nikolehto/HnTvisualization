using UnityEngine;
using System.Collections;

public class TransformScript : MonoBehaviour {

    syncValues data;

	// Use this for initialization
	void Start () {
        data = GetComponent<syncValues>();
	}
	
    public Vector3 getPosition(double nValue2, double eValue2)
    {
        double nDif = nValue2 - data.n1;
        double eDif = eValue2 - data.e1;

        double zDif = nDif * data.n2zFactor;
        double xDif = eDif * data.e2xFactor;

        float z3 = (float)(data.z1 + zDif);
        float x3 = (float)(data.x1 + xDif);

        return new Vector3(x3, 0.5f, z3);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
