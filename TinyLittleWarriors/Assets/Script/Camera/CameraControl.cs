using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed = 2.0f;
    public float zoomSpeed = 20.0f;
    public float rotationSpeed = 30.0f;

    float angle;
    private GameObject cam;
    
    // Start is called before the first frame update
    void Start()
    {
        angle = Mathf.Deg2Rad * this.gameObject.transform.rotation.x;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(this.GameObject().transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(this.GameObject().transform.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.R))
        {
            transform.RotateAround(this.GameObject().transform.position, transform.right, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.F))
        {
            transform.RotateAround(this.GameObject().transform.position, transform.right, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            this.gameObject.GetComponentInChildren<Camera>().transform.Translate(new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel")*zoomSpeed));
        }
    }
}
