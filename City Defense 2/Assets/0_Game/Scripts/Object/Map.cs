using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform finish;

    [SerializeField] private List<GameObject> objsBake;

    [SerializeField] private List<GameObject> objsRender;

    [SerializeField] private List<Warning> warnings;

    [SerializeField] private bool hasFinishHealth;

    [ShowIf("hasFinishHealth")] [SerializeField] private HealthBase healthFinish;

    public HealthBase HealthFinish => healthFinish;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveObjBake(bool isTrue)
    {
        for(int i = 0; i < objsBake.Count; i++)
        {
            objsBake[i].gameObject.SetActive(isTrue);
        }
    }

    public Transform GetFinish()
    {
        return finish;
    }

    public void ActiveWarning(DirectionSqawn directionSqawn)
    {
        switch (directionSqawn)
        {
            case DirectionSqawn.Staight:
                break;
            case DirectionSqawn.Left:
                break;
            case DirectionSqawn.Right:
                break;
        }

        warnings[(int)directionSqawn].StartWarning();
    }
}
