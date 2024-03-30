using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFollowScreen : MonoBehaviour
{
    [SerializeField] private float widthConstant;

    [SerializeField] private float heightConstant;

    // Start is called before the first frame update
    void Start()
    {
        float x = Screen.width / widthConstant;

        float y = Screen.height / heightConstant;

        transform.localScale = new Vector3(x, y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
