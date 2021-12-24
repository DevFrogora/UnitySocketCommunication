using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    // Start is called before the first frame update
    CubeClient cube;
    void Start()
    {
        cube  = GameObject.Find("Cube").GetComponent<CubeClient>();

    }

    // Update is called once per frame
    void Update()
    {
        //1. Test for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // get mouse position in world space
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));

            //vector substration to get the direction vector 
            // 3. get direction vector from camera postion to mouse position in world space
            Vector3 direction = worldMousePosition - Camera.main.transform.position;

            RaycastHit hit;
            //4. cast a ray from the camera in the given direction
            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
            {
                Debug.Log("hitted" + hit.point);
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.green, 0.5f);
                if(hit.collider.gameObject.name == "CubeClient")
                {
                    //cube.changeOnClick();
                }
             }
            else
            {
                Debug.Log("not hitted" + hit.point);
                Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.red, 0.5f);
            }

        }


    }


}
