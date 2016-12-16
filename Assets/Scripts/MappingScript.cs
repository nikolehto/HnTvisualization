using UnityEngine;
using System.Collections;

public class MappingScript : MonoBehaviour {

    public GameObject pos1;
    public double nValue1;
    public double eValue1;

    public GameObject pos2;
    public double nValue2;
    public double eValue2;

    public double nValue3;
    public double eValue3;


    // Use this for initialization
    void Start () {
        double z1 = pos1.transform.position.z;
        double x1 = pos1.transform.position.x;

        double z2 = pos2.transform.position.z;
        double x2 = pos2.transform.position.x;


        double distanceN = nValue2 - nValue1;
        double distanceZ = z2 - z1;
        double n2zFactor = distanceZ / distanceN;

        Debug.Log( "N distance: " + distanceN.ToString() + "Z distance: " + distanceZ.ToString() );
        Debug.Log(n2zFactor);

        double distanceE = eValue2 - eValue1;
        double distanceX = x2 - x1;
        double e2xFactor = distanceX / distanceE;

        Debug.Log("E distance: " + distanceE.ToString() + "X distance: " + distanceX.ToString());
        Debug.Log(e2xFactor);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "predict";

        double nDif = nValue3 - nValue1;
        double eDif = eValue3 - eValue1;

        double zDif = nDif * n2zFactor;
        double xDif = eDif * e2xFactor;

        float z3 = (float)(z1 + zDif);
        float x3 = (float)(x1 + xDif);

        cube.transform.position = new Vector3(x3, 0, z3);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
