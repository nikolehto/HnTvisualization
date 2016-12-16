using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class TheScript : MonoBehaviour {

    
    public JSONNode testData;
    private TextAsset file;
    public string fileName = "";
    private string filePath = "";

    [Tooltip("Set colors of the players - if not set random colors will be used")]
    public Color[] playerColors;

    public GameObject positionTransformContainer;

    public GameObject playerMark;
    public GameObject fakePositionMark;
    TransformScript ts;

    public bool showNoPlayers = false;
    [Tooltip("indices of players which are visualized. Indices start from 0. If empty list all players are visualized")]
    public List<int> showPlayers;

    public bool greyHiddenPlayers = false;

    public bool showNoMarkers = false;
    [Tooltip("indices of players which markers are visualized. Indices start from 0. If empty list all markers are visualized")]
    public List<int> showMarkers;


    void Start()
    {
        drawScene();
    }

    bool drawScene()
    {
        bool playerListSet = showPlayers.Count != 0;
        bool markerListSet = showMarkers.Count != 0;


        ts = positionTransformContainer.GetComponent<TransformScript>();
        if (ts == null)
        {
            Debug.Log("ts is null");
            Application.Quit();
        }


        // get data from file
        file = (TextAsset)Resources.Load(filePath + fileName);

        if (file == null)
        {
            Debug.Log("Json-file not exist");
            Application.Quit();
        }

        else
        {
            Debug.Log("File Exists");
            //Debug.Log(file.text);
            testData = JSON.Parse(file.text);
            //Debug.Log(testData);
        }

        //Debug.Log(testData["id"]);

        int playerCount;
        int.TryParse(testData["playerCount"], out playerCount);
        int pCount = testData["players"].Count;

        for (int playerIndex = 0; playerIndex != pCount; playerIndex++)
        {
            Debug.Log("Player index: " + playerIndex);

            bool pIsChosen = showPlayers.IndexOf(playerIndex) != -1;
            bool mIsChosen = showMarkers.IndexOf(playerIndex) != -1;

            bool showPlayerPosition = (!showNoPlayers) && (!playerListSet || pIsChosen);
            bool showMarkerPosition = (!showNoMarkers) && (!markerListSet || mIsChosen);


            Color playerColor;
            Color markerColor;
            if (playerColors.Length > playerIndex)
            {
                playerColor = playerColors[playerIndex];
            }
            else
            {
                playerColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }

            markerColor = playerColor * 0.8f;


            int movCount = testData["players"][playerIndex]["playerMovement"].Count;
            Debug.Log("Move count: " + movCount);
            for (int movIndex = 0; movIndex != movCount; movIndex++)
            {

                JSONNode movement = testData["players"][playerIndex]["playerMovement"][movIndex];

                double n = movement["latitude"].AsDouble;
                double e = movement["longitude"].AsDouble;
                bool isFake = movement["isFakeLocation"].AsBool;
                bool isHidden = movement["isMoving"].AsBool;

                Debug.Log(ts.getPosition(n, e));
                Vector3 pos = ts.getPosition(n, e);

                if (showPlayerPosition == true && isFake == false)
                {
                    GameObject cube = Instantiate(playerMark) as GameObject;
                    cube.name = "pos";
                    pos.y += (float)playerIndex * 0.01f;
                    cube.transform.position = pos;


                    if (isHidden && greyHiddenPlayers)
                    {
                        cube.GetComponent<Renderer>().material.color = Color.grey;
                    }
                    else
                    {
                        cube.GetComponent<Renderer>().material.color = playerColor;
                    }



                }

                else if (showMarkerPosition == true && isFake == true)
                {
                    GameObject cube = Instantiate(fakePositionMark) as GameObject;
                    cube.name = "pos";
                    pos.y += (float)playerIndex * 0.01f;
                    cube.transform.position = pos;
                    Renderer[] renderers = cube.GetComponentsInChildren<Renderer>();

                    foreach (Renderer rend in renderers)
                    {
                        if (rend.name != "top_text")
                        {
                            rend.material.color = markerColor;
                        }
                    }
                }
            }


        }
        return true;
    }



    // Update is called once per frame
    void Update () {
	
	}


}
