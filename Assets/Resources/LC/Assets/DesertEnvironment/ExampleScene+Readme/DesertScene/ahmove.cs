using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ahmove : MonoBehaviour
{
    public int i;
    public Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
        i = 0;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (this.tag == "dongji")
        {
            if (i == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.position -= new Vector3(0, 0, 0.5f);
                if (transform.position.z < 200)
                {
                    i = 1;
                }
            }
            if (i == 1)
            {
                transform.Rotate(new Vector3(0, 1, 0));
                if (transform.eulerAngles.y >= 180)
                {
                    i = 2;
                }
            }
            if (i == 2)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.position += new Vector3(0, 0, 0.5f);
                if (transform.position.z > 550)
                {
                    i = 3;
                }

            }
            if (i == 3)
            {
                transform.Rotate(new Vector3(0, -1, 0));
                if (transform.eulerAngles.y <= 1)
                {
                    i = 0;
                }
            }
        }
        if (this.tag == "dongji2")
        {
            if (i == 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.position -= new Vector3(0.5f, 0, 0);
                if (transform.position.x < 200)
                {
                    i = 1;
                }
            }
            if (i == 1)
            {
                transform.Rotate(new Vector3(0, 1, 0));
                if (transform.eulerAngles.y >= 270)
                {
                    i = 2;
                }
            }
            if (i == 2)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                transform.position += new Vector3(0.5f, 0, 0);
                if (transform.position.x > 600)
                {
                    i = 3;
                }

            }
            if (i == 3)
            {
                transform.Rotate(new Vector3(0, -1, 0));
                if (transform.eulerAngles.y <= 90)
                {
                    i = 0;
                }
            }
        }
        if (this.tag == "dongji3")
        {
            transform.Translate(transform.forward * 1.5f);
            transform.Rotate(new Vector3(0, -0.3f, 0));

        }
        if (this.tag == "soldier")
        {
            animation.CrossFade("Take 001", 0);
            if (i == 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.position += new Vector3(0.05f, 0, 0);
                if (transform.position.x > 700)
                {
                    i = 1;
                }
            }
            if (i == 1)
            {
                transform.Rotate(new Vector3(0, -1, 0));
                if (transform.eulerAngles.y >= 200)
                {
                    i = 2;
                }
            }
            if (i == 2)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                transform.position -= new Vector3(0.05f, 0, 0);
                if (transform.position.x < 500)
                {
                    i = 3;
                }

            }
            if (i == 3)
            {
                transform.Rotate(new Vector3(0, 1, 0));
                if (transform.eulerAngles.y >= 90)
                {
                    i = 0;
                }
            }
        }
        if (this.tag == "gebisoldier")
        {
            animation.CrossFade("Take 001", 0);
            if (i == 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.position += new Vector3(0.05f, 0, 0);
                if (transform.position.x > -280)
                {
                    i = 1;
                }
            }
            if (i == 1)
            {
                transform.Rotate(new Vector3(0, -1, 0));
                if (transform.eulerAngles.y >= 200)
                {
                    i = 2;
                }
            }
            if (i == 2)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                transform.position -= new Vector3(0.05f, 0, 0);
                if (transform.position.x < -400)
                {
                    i = 3;
                }

            }
            if (i == 3)
            {
                transform.Rotate(new Vector3(0, 1, 0));
                if (transform.eulerAngles.y >= 90)
                {
                    i = 0;
                }
            }
        }
        if (this.tag == "tank")
        {
            if (i == 0)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 270, transform.eulerAngles.z);
                transform.position -= new Vector3(0.1f, 0, 0);
                if (transform.position.x < 500)
                {
                    i = 1;
                }
            }
            if (i == 1)
            {
                transform.Rotate(new Vector3(0, -1, 0));
                if (transform.eulerAngles.y <= 90)
                {
                    i = 2;
                }
            }
            if (i == 2)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
                transform.position += new Vector3(0.1f, 0, 0);
                if (transform.position.x > 620)
                {
                    i = 3;
                }

            }
            if (i == 3)
            {
                transform.Rotate(new Vector3(0, 1, 0));
                if (transform.eulerAngles.y > 269)
                {
                    i = 0;
                }
            }
        }
        if (this.tag == "gebitank")
        {
            if (i == 0)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 270, transform.eulerAngles.z);
                transform.position -= new Vector3(0.1f, 0, 0);
                if (transform.position.x < -450)
                {
                    i = 1;
                }
            }
            if (i == 1)
            {
                transform.Rotate(new Vector3(0, -1, 0));
                if (transform.eulerAngles.y <= 90)
                {
                    i = 2;
                }
            }
            if (i == 2)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
                transform.position += new Vector3(0.1f, 0, 0);
                if (transform.position.x > -300)
                {
                    i = 3;
                }

            }
            if (i == 3)
            {
                transform.Rotate(new Vector3(0, 1, 0));
                if (transform.eulerAngles.y > 269)
                {
                    i = 0;
                }
            }
        }
        if (this.tag == "shanqusoldier")
        {
            animation.CrossFade("Take 001", 0);
            if (i == 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.position += new Vector3(0.05f, 0, 0);
                if (transform.position.x > 900)
                {
                    i = 1;
                }
            }
            if (i == 1)
            {
                transform.Rotate(new Vector3(0, -1, 0));
                if (transform.eulerAngles.y >= 200)
                {
                    i = 2;
                }
            }
            if (i == 2)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                transform.position -= new Vector3(0.05f, 0, 0);
                if (transform.position.x < 850)
                {
                    i = 3;
                }

            }
            if (i == 3)
            {
                transform.Rotate(new Vector3(0, 1, 0));
                if (transform.eulerAngles.y >= 90)
                {
                    i = 0;
                }
            }
        }
        if (this.tag == "shanqutank")
        {
            if (i == 0)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 270, transform.eulerAngles.z);
                transform.position -= new Vector3(0.1f, 0, 0);
                if (transform.position.x < 830)
                {
                    i = 1;
                }
            }
            if (i == 1)
            {
                transform.Rotate(new Vector3(0, -1, 0));
                if (transform.eulerAngles.y <= 90)
                {
                    i = 2;
                }
            }
            if (i == 2)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
                transform.position += new Vector3(0.1f, 0, 0);
                if (transform.position.x > 860)
                {
                    i = 3;
                }

            }
            if (i == 3)
            {
                transform.Rotate(new Vector3(0, 1, 0));
                if (transform.eulerAngles.y > 269)
                {
                    i = 0;
                }
            }
        }
        if (this.tag == "shanqudiji")
        {
            if (i == 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.position += new Vector3(0, 0, 0.5f);
                if (transform.position.z > 750)
                {
                    i = 1;
                }
            }
            if (i == 1)
            {
                transform.Rotate(new Vector3(0, -1, 0));
                if (transform.eulerAngles.y <= 1)
                {
                    i = 2;
                }
            }
            if (i == 2)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.position -= new Vector3(0, 0, 0.5f);
                if (transform.position.z < 550)
                {
                    i = 3;
                }

            }
            if (i == 3)
            {
                transform.Rotate(new Vector3(0, 1, 0));
                if (transform.eulerAngles.y >= 180)
                {
                    i = 0;
                }
            }
        }

    }
}



