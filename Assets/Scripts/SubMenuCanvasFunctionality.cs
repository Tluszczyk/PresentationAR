using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuCanvasFunctionality : MonoBehaviour
{
    [SerializeField]
    GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(0,0,0);
    }

    public void activate()
    {
        gameObject.transform.localScale = new Vector3(1,1,1);
    }

    public void desactivate()
    {
        gameObject.transform.localScale = new Vector3(0,0,0);
    }
}
