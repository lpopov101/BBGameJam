using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float torque;
    public float secondsDelay;
    private float time = 0;
    private float mvmtSpd = 20f;
    protected Rigidbody2D r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > secondsDelay * Random.Range(0.5f,3f)) {
            time = Time.time;
                    if (Random.Range(1, 100) <= 2) {
                        Split();
                        Split();
                        Destroy(r.gameObject);
                    }
        }
    }

    private void Split() {
            // new child pos/direction
            Vector2 childPos = r.position + Random.insideUnitCircle * 0.5f;
            // split into 2 smaller roids (make alterable # of splits)
            Debug.Log(string.Format("transforming local scale by 0.5 * {0} (x) and 0.5 * {1} (y)", r.transform.localScale.x, r.transform.localScale.y));
            var child = Instantiate<Rigidbody2D>(r, childPos, r.transform.rotation);
        
            float halfX = r.transform.localScale.x * 0.5f;
            float halfY = r.transform.localScale.y * 0.5f;
            child.transform.localScale = new Vector2(halfX, halfY);
            
            // set random new trajectory
            child.AddForce(Random.insideUnitCircle.normalized * mvmtSpd);
        }
}
