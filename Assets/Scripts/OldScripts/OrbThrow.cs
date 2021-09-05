using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbThrow : MonoBehaviour
{

    public GameObject orbPrefab;
    public float throwSpeed = 5;
    public float slingshotSpeed = 12;
    public float speed;
    Vector3 mousePos;
    private bool hasSlingshot = false;
    public bool orbOnScreen = false;
    public GameObject player;
    OrbScript oScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = hasSlingshot ? slingshotSpeed : throwSpeed;
        if (Input.GetMouseButtonDown(0) && !orbOnScreen/*&& GameObject.FindGameObjectsWithTag("orb").Length == 0*/)
        {
            orbOnScreen = true;

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (Vector2)(mousePos - transform.position);
            direction.Normalize();

            GameObject orb = Instantiate(orbPrefab, transform.position + (Vector3)(direction * 1f), Quaternion.identity);

            orb.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x * speed, direction.y * speed), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.S) && orbOnScreen)
        {
            oScript = GameObject.FindGameObjectWithTag("orb").GetComponent<OrbScript>();

            player.transform.position = new Vector3(oScript.dir.x, oScript.dir.y, 0);
            oScript.destroy();

            orbOnScreen = false;

        }
    }

    private void FixedUpdate()
    {

        
    }
}
