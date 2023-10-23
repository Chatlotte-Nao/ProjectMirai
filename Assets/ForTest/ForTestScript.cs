using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ForTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var h= Input.GetAxis("Horizontal");
        Log.Debug(h);
    }
}
