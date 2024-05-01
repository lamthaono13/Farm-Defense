using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour, ISingleton
{
    public static EnergyManager Instance;
    
    [SerializeField] private DronEnergy dron;

    [SerializeField] private bool noDron;

    [SerializeField] private bool hasEditEnergy;

    [SerializeField] private UiEnergy uiEnergy;

    private int energyMax;

    private int currentEnergy;

    public delegate void EventChangeEnergy(float a, float b, bool c);

    public EventChangeEnergy OnChangeEnergy;

    private bool canSqawnDron;

    private bool hasClickDron;

    private bool hasFisrtEarnEnergy;

    [SerializeField] private int energyAddPerSecond;

    private bool canAddEnergy;

    private float countSecond;

    // Start is called before the first frame update
    void Start()
    {
        //canSqawnDron = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAddEnergy)
        {
            if(countSecond >= 1)
            {
                AddEnergy(energyAddPerSecond);

                countSecond = 0;
            }
            else
            {
                countSecond += Time.deltaTime;
            }
        }
    }

    public void InitSingleton()
    {
        Instance = this;
    }
    
    public void OnClickDron()
    {
        hasClickDron = true;
    }

    public void SqawnDron()
    {
        if (noDron)
        {
            return;
        }

        if (!canSqawnDron)
        {
            return;
        }

        canSqawnDron = false;

        StartCoroutine(WaitReturnDron());

        dron.StartMove();
    }

    IEnumerator WaitReturnDron()
    {
        yield return new WaitForSeconds(15);

        if (!hasClickDron)
        {
            canSqawnDron = true;
        }
    }

    public void Init(int _energyMax, bool isDron)
    {
        //LevelManagerMainGame.Instance.EnergyManager.OnChangeEnergy += LevelManagerMainGame.Instance.UiManagerMainGame.UiEnergy.FillBar;

        if (!hasEditEnergy)
        {
            energyMax = _energyMax;
        }
        else
        {
            energyMax = 100000;
        }

        currentEnergy = energyMax;

        canSqawnDron = isDron;

        uiEnergy.Init();

        OnChangeEnergy?.Invoke(currentEnergy, energyMax, false);

        canAddEnergy = true;

        countSecond = 0;
    }

    public void AddEnergy(int numberAdd)
    {
        int u = currentEnergy + numberAdd;

        if(u > energyMax)
        {
            currentEnergy = energyMax;
        }
        else
        {
            currentEnergy = u;
        }

        int currentLevel = GameManager.Instance.DataManager.GetLevel();

        if(currentLevel == 1 & !hasFisrtEarnEnergy)
        {
            hasFisrtEarnEnergy = true;

            OnChangeEnergy?.Invoke(currentEnergy, energyMax, true);
        }
        else
        {
            OnChangeEnergy?.Invoke(currentEnergy, energyMax, false);
        }
    }

    public void AddEnergy(TypeGroup typeGroup)
    {
        int u = currentEnergy + 1;

        if(u > energyMax)
        {
            currentEnergy = energyMax;
        }
        else
        {
            currentEnergy = u;
        }

        int currentLevel = GameManager.Instance.DataManager.GetLevel();

        if(currentLevel == 1 & !hasFisrtEarnEnergy)
        {
            hasFisrtEarnEnergy = true;

            OnChangeEnergy?.Invoke(currentEnergy, energyMax, true);
        }
        else
        {
            OnChangeEnergy?.Invoke(currentEnergy, energyMax, false);
        }


    }
    
    public void SetEnergy(int number)
    {
        currentEnergy = energyMax;

        int currentLevel = GameManager.Instance.DataManager.GetLevel();

        if (currentLevel == 2 & !hasFisrtEarnEnergy)
        {
            hasFisrtEarnEnergy = true;

            OnChangeEnergy?.Invoke(currentEnergy, energyMax, true);
        }
        else
        {
            OnChangeEnergy?.Invoke(currentEnergy, energyMax, false);
        }
    }

    public void SubEnergy(int numberSub)
    {
        //if (GameManager.Instance.IsGameDesign)
        //{
        //    return;
        //}

        int u = currentEnergy - numberSub;

        if (u < 0)
        {
            currentEnergy = 0;
        }
        else
        {
            currentEnergy = u;
        }

        if(currentEnergy < 32)
        {
            SqawnDron();
        }

        OnChangeEnergy?.Invoke(currentEnergy, energyMax, false);
    }

    public bool canSqawn(int numberSub)
    {
        return currentEnergy >= numberSub ? true : false;
    }

    public void OnRevive()
    {
        currentEnergy += energyMax / 2;

        OnChangeEnergy?.Invoke(currentEnergy, energyMax, false);
    }
}
