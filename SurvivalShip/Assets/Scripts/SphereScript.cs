using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {

    public float floatSpeed = .5F;
    public float floatTime = 0F;
    public float floatDistance = .01F;
    public float rotateSpeed = 1F;
    public int spheresCollected = 0;
    public GameObject sphere;

    // Use this for initialization
    void Start()
    {
        Screen.showCursor = false;
    }

    // Update is called once per frame
    void Update()
    {
        floatTime += Time.deltaTime + floatSpeed;
        transform.Translate(0F, Mathf.Sin(Mathf.PI * 2 * floatTime) * floatDistance, 0F);

        transform.Rotate(0F, rotateSpeed, 0F);
    }
   
    void OnTriggerEnter(Collider other)
    {     
        Destroy(this.transform.gameObject);
        SpheresCollected();

    }
    void SpheresCollected() {
        spheresCollected++;
        //Debug.Log(spheresCollected);
    }

}
