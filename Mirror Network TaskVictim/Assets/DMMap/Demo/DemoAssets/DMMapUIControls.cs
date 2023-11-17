using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DMM;
using Unity.VisualScripting;

public class DMMapUIControls : MonoBehaviour {

    public Transform followTarget;
    public GameObject mapCamera;
    public float zoomValue;
    public float xValue;
    public float zValue;
    public GameObject mapImage;
    public Button xBUTTON;

    public bool dragging = false;
    public float xDragStart;
    public float yDragStart;
    public float xDrag;
    public float yDrag;

    public float scrollWheel;
    public float scrollSensitivity;

    public bool isFullscreen;
    public GameObject mapFrame;

    public void Awake() {
        // EventSystem.current.sendNavigationEvents = false;
        mapFrame.SetActive(false);
    }



    public void SetZoom(float value) {
        //DMMap.instance.configs[DMMap.instance.loadedConfig].zoom = value;
        //Vector3 campos = mapCamera.transform.position;
        //ampos.y = value;
        //zoomValue = campos.y;
        zoomValue = value;
        mapCamera.transform.position = new Vector3(mapCamera.transform.position.x, zoomValue, mapCamera.transform.position.z);
    }

    public void SetX(float value)
    {
        //DMMap.instance.configs[DMMap.instance.loadedConfig].zoom = value;
        //Vector3 campos = mapCamera.transform.position;
        //ampos.y = value;
        //zoomValue = campos.y;
        xValue = value;
        //mapImage.GetComponent<RawImage>().uvRect.Equals(xValue);
        mapCamera.transform.position = new Vector3(xValue, mapCamera.transform.position.y, mapCamera.transform.position.z);
    }

    public void SetXbutton()
    {
        //DMMap.instance.configs[DMMap.instance.loadedConfig].zoom = value;
        //Vector3 campos = mapCamera.transform.position;
        //ampos.y = value;
        //zoomValue = campos.y;
        //xValue = value;
        //mapImage.GetComponent<RawImage>().uvRect.Equals(xValue);
        //mapCamera.transform.position = new Vector3(3, mapCamera.transform.position.y, mapCamera.transform.position.z) * Time.deltaTime;
        //mapCamera.transform.position += transform.right * 3 * Time.deltaTime;
    }

    public void SetZ(float value)
    {
        //DMMap.instance.configs[DMMap.instance.loadedConfig].zoom = value;
        //Vector3 campos = mapCamera.transform.position;
        //ampos.y = value;
        //zoomValue = campos.y;
        zValue = value;
        mapCamera.transform.position = new Vector3(mapCamera.transform.position.x, mapCamera.transform.position.y, zValue);
    }

    public void ToggleRotate(bool value) {
        DMMap.instance.configs[DMMap.instance.loadedConfig].rotate = value;
        if(followTarget != null) DMMap.instance.configs[DMMap.instance.loadedConfig].objectToFocusOn = followTarget;
    }

    public void ToggleFollow(bool value) {
        if (value) {
            if (followTarget != null) DMMap.instance.configs[DMMap.instance.loadedConfig].objectToFocusOn = followTarget;
        } else {
            DMMap.instance.configs[DMMap.instance.loadedConfig].objectToFocusOn = null;
        }
    }

    public void SetOpacity(float value) {
        DMMap.instance.configs[DMMap.instance.loadedConfig].opacity = value;
    }

    public void SetBackgroundOpacity(float value) {
        Color c = DMMap.instance.configs[DMMap.instance.loadedConfig].mapBackgroundColor;
        c.a = value;
        DMMap.instance.configs[DMMap.instance.loadedConfig].mapBackgroundColor = c;
    }

    public void SetMinimap() {
        DMMap.instance.LoadConfig(0);
    }

    public void SetFullscreen() {
        DMMap.instance.LoadConfig(1);
    }

    public void NextDemo() {
        int loadedLevel = Application.loadedLevel;
        loadedLevel++;
        if (loadedLevel >= 4) loadedLevel = 0;
        Application.LoadLevel(loadedLevel);
    }
    public bool toggle = false;

    public void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    toggle = !toggle;
        //    DMMap.instance.gameObject.SetActive(toggle);
        //}

        //xBUTTON.onClick.AddListener(SetXbutton);
        if (Input.GetKey(KeyCode.J))
        {
            mapCamera.transform.Translate(mapCamera.transform.right * -20f * Time.deltaTime);
        }

        if (Input.GetMouseButton(0))
        {

            if (!dragging)
            {
                dragging = true;

                xDragStart = Input.GetAxis("Mouse X");
                yDragStart = Input.GetAxis("Mouse Y");
            }


            xDrag = Input.GetAxis("Mouse X") * 200f;
            yDrag = Input.GetAxis("Mouse Y") * 200f;

            //DragValuesText.text = "x = " + xDrag + ", y = " + yDrag;
            //Debug.Log("x = " + xDrag + ", y = " + yDrag);
            mapCamera.transform.Translate(mapCamera.transform.right * -(xDrag) * Time.deltaTime);
            mapCamera.transform.Translate(mapCamera.transform.forward * yDrag * Time.deltaTime);
        }
        else
        {
            if (dragging)
            {
                dragging = false;
            }
        }

        scrollWheel = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        //Debug.Log("scroll = " + scrollWheel);
        mapCamera.transform.Translate(mapCamera.transform.up * scrollWheel * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.M))
        {
            if (isFullscreen == false)
            {
                
                isFullscreen = true;
                mapFrame.SetActive(true);

            }
            else if (isFullscreen == true)
            {
                
                isFullscreen = false;
                mapFrame.SetActive(false);

            }

        }
    }
}
