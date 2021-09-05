using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour
{
    private int objects;
    // Start is called before the first frame update
    void Start()
    {
        objects = 0;
    }

    bool allowableJump(string s)
    {
        return s == "ground" || s == "block" || s == "Box" || s == "orb" || s == "MovingBlock";
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "MovingBlock")
        {
            this.gameObject.transform.parent.transform.SetParent(col.gameObject.transform);
        }

        if(allowableJump(col.collider.tag))
        {
            objects++;
            this.gameObject.transform.parent.gameObject.GetComponent<Movement>().resetHasFired();
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(allowableJump(col.collider.tag))
        {
            this.gameObject.transform.parent.gameObject.GetComponent<Movement>().resetHasFired();
        }
    }

    public bool GetJump()
    {
        return objects > 0;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.collider.tag == "MovingBlock")
        {
            this.gameObject.transform.parent.transform.SetParent(null);
        }

        if(allowableJump(col.collider.tag))
        {
            objects--;
        }
    }
}
