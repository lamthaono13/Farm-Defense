using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ChildImpactDetect : MonoBehaviour
{
    [SerializeField] private ObjectBase objectBase;

    public ObjectBase ObjectBase => objectBase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectBase.OnTriggerEnterChildDetect(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        objectBase.OnTriggerStayChildDetect(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectBase.OnTriggerExitChildDetect(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        objectBase.OnCollistionEnterChildDetect(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        objectBase.OnCollistionStayChildDetect(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        objectBase.OnCollistionExitChildDetect(collision);
    }
}
