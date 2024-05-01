using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharManager : MonoBehaviour, ISingleton
{
    public static CharManager Instance;

    [SerializeField] private SqawnSystem _sqawnSystem;
    
    private List<IContactObject> characterBases;

    private List<IContactObject> allies;

    private List<IContactObject> enermies;


    public SqawnSystem SqawnSystem => _sqawnSystem;
    
    public List<IContactObject> CharacterBases => characterBases;

    public List<IContactObject> Allies => allies;

    public List<IContactObject> Enermies => enermies;

    private int numberEnermyDieNeedToWin;

    private int numberEnermyDie;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void InitSingleton()
    {
        Instance = this;
        
        characterBases = new List<IContactObject>();
        allies = new List<IContactObject>();
        enermies = new List<IContactObject>();
        
        
    }

    public void AddCharBase(IContactObject iContactObject)
    {
        characterBases.Add(iContactObject);
    }

    public void RemoveCharBase(IContactObject iContactObject)
    {
        if (characterBases.Contains(iContactObject))
        {
            characterBases.Remove(iContactObject);
        }
    }

    public void AddAlly(IContactObject iContactObject)
    {
        allies.Add(iContactObject);
    }

    public void RemoveAlly(IContactObject iContactObject)
    {
        if (allies.Contains(iContactObject))
        {
            allies.Remove(iContactObject);
        }
    }

    public void AddEnermy(IContactObject iContactObject)
    {
        if(enermies == null)
        {
            enermies = new List<IContactObject>();
        }

        if (enermies.Contains(iContactObject))
        {
            return;
        }

        enermies.Add(iContactObject);
    }

    public void RemoveEnermy(IContactObject iContactObject)
    {
        if (enermies.Contains(iContactObject))
        {
            numberEnermyDie++;

            CheckWin();

            enermies.Remove(iContactObject);
        }
    }

    public void CheckWin()
    {
        if (GameManager.Instance.IsGameDesign)
        {
            return;
        }

        if(numberEnermyDie >= numberEnermyDieNeedToWin)
        {
            // Win

            LevelManagerMainGame.Instance.OnEndGame(GameResult.Win);
        }
    }

    public void AddEnermyNeedToWin()
    {
        numberEnermyDieNeedToWin++;
    }

    public void AddNewSpecialEnermy()
    {
        numberEnermyDieNeedToWin++;
    }
}
