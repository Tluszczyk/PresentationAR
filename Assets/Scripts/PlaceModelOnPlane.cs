using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceModelOnPlane : MonoBehaviour {

    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    private Vector3 localScale = new Vector3(.01f,.01f,.01f);

    private float rotation = .0f;

    // [SerializeField]
    // string projectPath;

    private int m_modelPtr = 0;

    private GameObject m_ModelToPlace;
    private GameObject placedObject = null;

    [SerializeField]
    ARRaycastManager m_ARRaycastManager;

    private bool isModelPlaced = false;
    
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    [SerializeField]
    List<GameObject> m_modelsToPlace = new List<GameObject>();

    void loadModelsGeneric()
    {
        // var dirPath = projectPath + "/the-utah-teapot/source/all";
        // var info = new DirectoryInfo(dirPath);

        // foreach(var modelPath in info.GetFiles())
        // {
        //     if (modelPath.Extension != ".obj") continue;
        //     m_modelsToPlace.Add(Resources.Load<GameObject>(modelPath.ToString()));
        // }

        // Debug.Log("Loaded " + m_modelsToPlace.Count + " models");
    }

    void Start()
    {
        loadModelsGeneric();

        m_ModelToPlace = m_modelsToPlace[m_modelPtr];
    }
    
    public void SetModelToPlace(int modelPtr)
    {
        if (placedObject != null) Destroy(placedObject);

        m_modelPtr = modelPtr;
        m_ModelToPlace = m_modelsToPlace[m_modelPtr];
        isModelPlaced = false;
    }

    void Update () {

        // float rotation = .0f;

        if( Input.touchCount == 1 ) {
            Touch touch = Input.GetTouch(0);

            // Placing a model
            if( touch.phase == TouchPhase.Began ) {
                if( isModelPlaced ) return;

                if( m_ARRaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon) ) {
                    Pose hitPose = s_Hits[0].pose;

                    placedObject = Instantiate(m_ModelToPlace, hitPose.position, hitPose.rotation);
                    placedObject.transform.localScale = localScale;

                    isModelPlaced = true;
                }
            }

            // Rotating with one finger
            if( touch.phase == TouchPhase.Moved ) {
                rotation = touch.deltaPosition.x * 0.1f;
            }
        } 
        
        // Pitch zoom
        else if( isModelPlaced && Input.touchCount == 2 ) {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            
            // rotation = ((touchZero.deltaPosition + touchOne.deltaPosition) / 2).x * 0.05f;

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = (currMagnitude - prevMagnitude) * 0.0005f;

            localScale = placedObject.transform.localScale + new Vector3(difference, difference, difference);
            placedObject.transform.localScale = localScale;
        }

        if( isModelPlaced ) {
            placedObject.transform.Rotate(0, -rotation, 0, Space.Self);
        }
    }
}