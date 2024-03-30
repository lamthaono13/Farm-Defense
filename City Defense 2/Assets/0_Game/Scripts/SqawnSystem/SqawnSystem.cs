using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class SqawnSystem : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    
    
    [SerializeField] private List<int> numberTutorial;

    private List<int> numberHasSqawn;

    private bool canSqawn;

    private TypeSqawn typeSqawn;

    private TypeGroup typeGroup;

    private TypeSlotEquip typeSlotEquip;

    private DataLevel dataLevel;

    private int energySub;

    private bool isTouching;

    [SerializeField] private List<Vector2> sizeSqawnAllys;

    //private List<Vector3> listPositionMove;

    Vector3 passPosition;

    Vector3 currentPosition;

    private List<Vector3> listPosition;

    //[SerializeField] private GameObject objCoroutine;

    [SerializeField] private GameObject objNewSqawn;

    private float timeDelay;

    private int currentLevel;

    private List<GameObject> allEnermy;

    private List<Coroutine> coroutines;

    // Start is called before the first frame update
    void Start()
    {
        //listPositionMove = new List<Vector3>();


    }

    public void Init(DataLevel _dataLevel, float _timeDelay)
    {
        dataLevel = _dataLevel;

        timeDelay = _timeDelay;

        allEnermy = new List<GameObject>();

        coroutines = new List<Coroutine>();

        currentLevel = GameManager.Instance.DataManager.GetLevel();

        numberHasSqawn = new List<int>();

        for(int i = 0; i < 3; i++)
        {
            numberHasSqawn.Add(0);
        }

        listPosition = new List<Vector3>();
        
        StartCoroutine(WaitSqawnEnermyInitialGame());
    }

    [Button]
    public void SqawnIdEnermy(TypeGroup typeGroup, TypeTier typeTier, Vector3 positionSqawn)
    {
        Debug.Log("Enermy/" + typeGroup.ToString() + "/" + typeGroup.ToString() + " Enermy " + typeTier.ToString());

        GameObject objLoad = ResourceManager.Instance.Load("Enermy/" + typeGroup.ToString() + "/" + typeGroup.ToString() + " Enermy " + typeTier.ToString()); //Resources.Load<GameObject>("Enermy/" + typeGroup.ToString() + "/" + typeGroup.ToString() + " Enermy " + typeTier.ToString());

        GameObject objEnermy = Instantiate(objLoad, positionSqawn, Quaternion.Euler(-90, 0, 0));

        CharacterBase characterBase = objEnermy.GetComponent<CharacterBase>();

        characterBase.InitIndexConfig(new DataSqawn() { level = 1 });

        CharManager.Instance.AddEnermyNeedToWin();

        objEnermy.gameObject.SetActive(true);
    }

    public void SqawnIdEnermy(int id, int _level, Vector3 positionSqawn)
    {
        GameObject objLoad = ResourceManager.Instance.Load("Enermy/Enermy " + id); //Resources.Load<GameObject>("Enermy/Enermy " + id);

        GameObject objEnermy = Instantiate(objLoad, positionSqawn, Quaternion.Euler(-90, 0, 0));

        CharacterBase characterBase = objEnermy.GetComponent<CharacterBase>();

        characterBase.InitIndexConfig(new DataSqawn() { level = _level });

        CharManager.Instance.AddEnermyNeedToWin();

        objEnermy.gameObject.SetActive(true);
    }

    public void SqawnIdEnermy(int id, int _level, Vector3 positionSqawn, int number)
    {
        GameObject objLoad = ResourceManager.Instance.Load("Enermy/Enermy " + id); //Resources.Load<GameObject>("Enermy/Enermy " + id);

        for(int i = 0; i < number; i++)
        {
            GameObject objEnermy = Instantiate(objLoad, positionSqawn, Quaternion.Euler(-90, 0, 0));

            CharacterBase characterBase = objEnermy.GetComponent<CharacterBase>();

            characterBase.InitIndexConfig(new DataSqawn() { level = _level });

            CharManager.Instance.AddEnermyNeedToWin();

            Vector2 a = UnityEngine.Random.insideUnitCircle * 2;

            objEnermy.transform.position = new Vector3(a.x, a.y, 0) + positionSqawn;

            objEnermy.gameObject.SetActive(true);
        }
    }

    IEnumerator WaitSqawnEnermyInitialGame()
    {
        yield return new WaitForSeconds(timeDelay);

        for (int i = 0; i < dataLevel.DataTurnEnermy.Count; i++)
        {
            List<DataSqawn> dataSqawns = new List<DataSqawn>(dataLevel.DataTurnEnermy[i].DataSqawns);

            List<GameObject> listContain = new List<GameObject>();



            for (int j = 0; j < dataSqawns.Count; j++)
            {
                try
                {
                    int a = (int)Char.GetNumericValue(dataSqawns[0].IdEnermy.ToString()[0]) - 1;

                    TypeGroup typeGroupEnermySqawn = (TypeGroup)a;

                    GameObject objLoad = ResourceManager.Instance.Load("Enermy/" + typeGroupEnermySqawn.ToString() + "/" + dataSqawns[0].IdEnermy.ToString()); //Resources.Load<GameObject>("Enermy/" + typeGroupEnermySqawn.ToString() + "/" + dataSqawns[0].IdEnermy.ToString());

                    //GameObject objLoad = Resources.Load<GameObject>("Enermy/Enermy " + dataSqawns[0].IdEnermy.ToString());

                    GameObject objEnermy = Instantiate(objLoad, dataSqawns[j].PostionSqawn, Quaternion.Euler(-90, 0, 0));

                    CharacterBase characterBase = objEnermy.GetComponent<CharacterBase>();



                    characterBase.InitIndexConfig(dataSqawns[j]);

                    listContain.Add(objEnermy);

                    CharManager.Instance.AddEnermyNeedToWin();
                }
                catch
                {
                    continue;
                }
            }

            //GameObject objNew = Instantiate(objCoroutine, transform);

            //CoroutineSqawn coroutineSqawn = objNew.GetComponent<CoroutineSqawn>();

            //coroutineSqawn.Init(() => {
            //    LevelManagerMainGame.Instance.ObjectManager.ActiveWarning(dataLevel.DataTurnEnermy[i].DirectionSqawn);

            //    for (int i = 0; i < listContain.Count; i++)
            //    {
            //        listContain[i].gameObject.SetActive(true);
            //    }
            //}, dataLevel.DataTurnEnermy[i].timeToSqawn);

            allEnermy.AddRange(listContain);

            coroutines.Add(StartCoroutine(WaitForSqawnTurnEnermy(dataLevel.DataTurnEnermy[i].timeToSqawn, listContain, dataLevel.DataTurnEnermy[i].DirectionSqawn)));
        }

        //GC.Collect();
    }

    private IEnumerator WaitForSqawnTurnEnermy(float timeWait, List<GameObject> objSqawn, DirectionSqawn directionSqawn)
    {
        yield return new WaitForSeconds(timeWait);

        GameManager.Instance.SoundManager.PlaySoundZombie();

        ObjectManager.Instance.ActiveWarning(directionSqawn);

        for(int i = 0; i < objSqawn.Count; i++)
        {
            objSqawn[i].gameObject.SetActive(true);
        }

        //GC.Collect();
    }

    public void SqawnHero()
    {
        GameObject objLoad = Resources.Load<GameObject>("Ally/Hero/HeroAlly");

        GameObject objInstan = Instantiate(objLoad, Vector3.zero, Quaternion.Euler(-90, 0, 0));

        CharacterBase characterBase = objInstan.GetComponent<CharacterBase>();

        characterBase.InitIndexConfig(new DataSqawn());

        characterBase.Init();

        characterBase.gameObject.SetActive(true);
    }

    public void SqawnAll()
    {
        for(int i = 0; i < coroutines.Count; i++)
        {
            if(coroutines[i] != null)
            {
                StopCoroutine(coroutines[i]);
            }
        }

        for(int i = 0; i < allEnermy.Count; i++)
        {
            if(allEnermy[i] != null)
            {
                allEnermy[i].gameObject.SetActive(true);
            }
        }
    }

    public void AddSpecialEnermySqawn(DataSqawn dataSqawn)
    {
        GameObject objLoad = Resources.Load<GameObject>("Enermy/Enermy " + dataSqawn.IdEnermy.ToString());

        GameObject objEnermy = Instantiate(objLoad, dataSqawn.PostionSqawn, Quaternion.Euler(-90, 0, 0));

        CharacterBase characterBase = objEnermy.GetComponent<CharacterBase>();

        characterBase.InitIndexConfig(dataSqawn);

        characterBase.gameObject.SetActive(true);
    }

    public void SqawnNewEnermy(Vector3 positionSqawn)
    {
        CharManager.Instance.AddEnermyNeedToWin();

        StartCoroutine(WaitSqawn(positionSqawn));
    }

    IEnumerator WaitSqawn(Vector3 positionSqawn)
    {
        yield return new WaitForSeconds(0.5f);

        Instantiate(objNewSqawn, positionSqawn, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        AddSpecialEnermySqawn(new DataSqawn()
        {
            IdEnermy = 0,
            CoinEarn = 5,
            level = 1,
            PostionSqawn = positionSqawn
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (canSqawn && isTouching)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 u = Camera.main.ScreenToWorldPoint(Input.mousePosition) / 5 * 15;

                //RaycastHit2D raycastHit = Physics2D.Raycast(u, Input.mousePosition);

                currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) / 5 * 15;

                SqawnFollowLine();

                passPosition = currentPosition;

                //if (raycastHit)
                //{
                //    if (raycastHit.collider.tag == "Ground")
                //    {


                //        //Sqawn(new Vector3(u.x, u.y, 0));
                //    }
                //}

                //bool a = true;

                //Collider2D[] collider2D = Physics2D.OverlapBoxAll(new Vector2(u.x, u.y), new Vector2(0.6f, 0.45f), 0);

                //foreach (var item in collider2D)
                //{
                //    if (!item.CompareTag("Ground"))
                //    {
                //        a = false;
                //    }
                //}

                //if (a)
                //{
                //    Sqawn(new Vector3(u.x, u.y, 0));
                //}
            }
        }
    }

    private void LateUpdate()
    {
        //List<Vector3> listCoppy = new List<Vector3>();

        //for(int i = 0; i < listPosition.Count; i++)
        //{
        //    Vector3 u = listPosition[i];

        //    Collider2D[] collider2D = Physics2D.OverlapBoxAll(new Vector2(u.x, u.y), new Vector2(0.3f, 0.215f), 0);

        //    bool a = true;

        //    foreach (var item in collider2D)
        //    {
        //        if (!item.CompareTag("Ground"))
        //        {
        //            if (!item.CompareTag("MoveStraight"))
        //            {
        //                a = false;
        //            }
        //        }
        //    }

        //    if (a)
        //    {
        //        listCoppy.Add(u);

        //        Sqawn(new Vector3(u.x, u.y, 0));
        //    }
        //}

        //for(int i = 0; i < listCoppy.Count; i++)
        //{
        //    if (listPosition.Contains(listCoppy[i]))
        //    {
        //        listPosition.Remove(listCoppy[i]);
        //    }
        //}
    }

    private void SqawnFollowLine()
    {
        float angleBox = DirectionToAngle2D.GetAngleFromDirection2D(new Vector3(sizeSqawnAllys[((int)typeGroup)].x, sizeSqawnAllys[((int)typeGroup)].y, 0));

        Vector3 dir = currentPosition - passPosition;

        dir = dir.normalized;

        dir.x = Mathf.Abs(dir.x);

        dir.y = Mathf.Abs(dir.y);

        float angleDir = DirectionToAngle2D.GetAngleFromDirection2D(dir.normalized);

        //Vector3 positionNext = passPosition;

        //while (Vector3.Distance(positionNext, passPosition) < Vector3.Distance(passPosition, currentPosition))
        //{
        //    Collider2D[] collider2D = Physics2D.OverlapBoxAll(new Vector2(positionNext.x, positionNext.y), sizeSqawnAllys[((int)typeAlly)], 0);

        //    bool a = true;

        //    foreach (var item in collider2D)
        //    {
        //        if (!item.CompareTag("Ground"))
        //        {
        //            a = false;
        //        }
        //    }

        //    if (a)
        //    {
        //        Sqawn(new Vector3(positionNext.x, positionNext.y, 0));
        //    }

        //    positionNext += new Vector3(sizeSqawnAllys[((int)typeAlly)].x, sizeSqawnAllys[((int)typeAlly)].y, 0);
        //}

        //passPosition = currentPosition;

        //return;

        if (angleBox == angleDir)
        {
            Vector3 positionNext = passPosition;

            while (Vector3.Distance(positionNext, passPosition) < Vector3.Distance(passPosition, currentPosition) && isTouching)
            {
                Collider2D[] collider2D = Physics2D.OverlapBoxAll(new Vector2(positionNext.x, positionNext.y), sizeSqawnAllys[((int)typeGroup)], 0);

                bool a = true;

                foreach (var item in collider2D)
                {
                    if (!item.CompareTag("Ground"))
                    {
                        if (!item.CompareTag("MoveStraight") && !item.CompareTag("Enermy"))
                        {
                            a = false;
                        }
                    }
                }

                if (a)
                {
                    Sqawn(new Vector3(positionNext.x, positionNext.y, 0));
                }

                Vector3 dir1 = currentPosition - passPosition;

                if (dir1.x >= 0)
                {
                    if (dir1.y >= 0)
                    {
                        positionNext += new Vector3(sizeSqawnAllys[((int)typeGroup)].x, sizeSqawnAllys[((int)typeGroup)].y, 0);
                    }
                    else
                    {
                        positionNext += new Vector3(sizeSqawnAllys[((int)typeGroup)].x, - sizeSqawnAllys[((int)typeGroup)].y, 0);
                    }
                }
                else
                {
                    if (dir1.y >= 0)
                    {
                        positionNext += new Vector3( - sizeSqawnAllys[((int)typeGroup)].x, sizeSqawnAllys[((int)typeGroup)].y, 0);
                    }
                    else
                    {
                        positionNext += new Vector3( - sizeSqawnAllys[((int)typeGroup)].x, - sizeSqawnAllys[((int)typeGroup)].y, 0);
                    }
                }


            }

            return;
        }

        if (angleBox < angleDir)
        {
            Vector3 positionNext = passPosition;

            while (Vector3.Distance(positionNext, passPosition) < Vector3.Distance(passPosition, currentPosition) && isTouching)
            {
                Collider2D[] collider2D = Physics2D.OverlapBoxAll(new Vector2(positionNext.x, positionNext.y), sizeSqawnAllys[((int)typeGroup)], 0);

                bool a = true;

                foreach (var item in collider2D)
                {
                    if (!item.CompareTag("Ground"))
                    {
                        if (!item.CompareTag("MoveStraight") && !item.CompareTag("Enermy"))
                        {
                            a = false;
                        }
                    }
                }

                if (a)
                {
                    Sqawn(new Vector3(positionNext.x, positionNext.y, 0));
                }

                Vector3 dir1 = currentPosition - passPosition;

                if (dir1.x >= 0)
                {
                    if (dir1.y >= 0)
                    {
                        positionNext += new Vector3(sizeSqawnAllys[((int)typeGroup)].y / Mathf.Tan(Mathf.Deg2Rad * angleDir), sizeSqawnAllys[((int)typeGroup)].y, 0);
                    }
                    else
                    {
                        positionNext += new Vector3(sizeSqawnAllys[((int)typeGroup)].y / Mathf.Tan(Mathf.Deg2Rad * angleDir), - sizeSqawnAllys[((int)typeGroup)].y, 0);
                    }
                }
                else
                {
                    if (dir1.y >= 0)
                    {
                        positionNext += new Vector3( - sizeSqawnAllys[((int)typeGroup)].y / Mathf.Tan(Mathf.Deg2Rad * angleDir), sizeSqawnAllys[((int)typeGroup)].y, 0);
                    }
                    else
                    {
                        positionNext += new Vector3( - sizeSqawnAllys[((int)typeGroup)].y / Mathf.Tan(Mathf.Deg2Rad * angleDir), - sizeSqawnAllys[((int)typeGroup)].y, 0);
                    }
                }


            }

            return;
        }

        if (angleBox > angleDir)
        {
            Vector3 positionNext = passPosition;

            while (Vector3.Distance(positionNext, passPosition) < Vector3.Distance(passPosition, currentPosition) && isTouching)
            {
                Collider2D[] collider2D = Physics2D.OverlapBoxAll(new Vector2(positionNext.x, positionNext.y), sizeSqawnAllys[((int)typeGroup)], 0);

                bool a = true;

                foreach (var item in collider2D)
                {
                    if (!item.CompareTag("Ground"))
                    {
                        if (!item.CompareTag("MoveStraight") && !item.CompareTag("Enermy"))
                        {
                            a = false;
                        }
                    }
                }

                if (a)
                {
                    Sqawn(new Vector3(positionNext.x, positionNext.y, 0));
                }

                Vector3 dir1 = currentPosition - passPosition;

                if(dir1.x >= 0)
                {
                    if(dir1.y >= 0)
                    {
                        positionNext += new Vector3(sizeSqawnAllys[((int)typeGroup)].x, sizeSqawnAllys[((int)typeGroup)].x * Mathf.Tan(Mathf.Deg2Rad * angleDir), 0);
                    }
                    else
                    {
                        positionNext += new Vector3(sizeSqawnAllys[((int)typeGroup)].x, - sizeSqawnAllys[((int)typeGroup)].x * Mathf.Tan(Mathf.Deg2Rad * angleDir), 0);
                    }
                }
                else
                {
                    if (dir1.y >= 0)
                    {
                        positionNext += new Vector3( - sizeSqawnAllys[((int)typeGroup)].x, sizeSqawnAllys[((int)typeGroup)].x * Mathf.Tan(Mathf.Deg2Rad * angleDir), 0);
                    }
                    else
                    {
                        positionNext += new Vector3( - sizeSqawnAllys[((int)typeGroup)].x, - sizeSqawnAllys[((int)typeGroup)].x * Mathf.Tan(Mathf.Deg2Rad * angleDir), 0);
                    }
                }


            }

            return;
        }
    }

    public void Sqawn(Vector3 positionSqawn)
    {
        GameManager.Instance.SoundManager.PlaySoundSqawn();

        //GameManager.Instance.SoundManager.PoolSoundManager.PlaySound(TypeSound.Sqawn);

        Pooling.Instance.PoolAlly.Sqawn(typeSlotEquip, positionSqawn);



        EnergyManager.Instance.SubEnergy(energySub);

        switch (typeGroup)
        {
            case TypeGroup.Barrier:
                break;
            case TypeGroup.Vanguard:

                LevelManagerMainGame.Instance.QuestManager.SetQuest(TypeQuestGame.Use_Vanguard);

                break;
            case TypeGroup.Sniper:

                LevelManagerMainGame.Instance.QuestManager.SetQuest(TypeQuestGame.Use_Sniper);

                break;
            case TypeGroup.Gunner:

                LevelManagerMainGame.Instance.QuestManager.SetQuest(TypeQuestGame.Use_Gunner);

                break;
            case TypeGroup.Oppressor:

                LevelManagerMainGame.Instance.QuestManager.SetQuest(TypeQuestGame.Use_Oppressor);

                break;
        }


        if (currentLevel == 1 && !GameManager.Instance.NoTutorial && !GameManager.Instance.DataManager.GetHasTutorialChooseChar())
        {
            numberHasSqawn[(int)typeSlotEquip] += 1;

            if (numberHasSqawn[(int)typeSlotEquip] == numberTutorial[(int)typeSlotEquip])
            {
                isTouching = false;

                //canSqawn = false;

                LevelManagerMainGame.Instance.UiManagerMainGame.UiTutorial.ChangeActionChoose();

                //StartCoroutine(WaitBugSqawn());
            }
        }

        TypeEquip _typeEquip = GameManager.Instance.DataManager.GetEquipAlly(typeSlotEquip);

        HandleFireBase.Instance.LogEventWithParameter("Spawn_Ally", new FirebaseParam[] { new FirebaseParam("Level", currentLevel), new FirebaseParam("Id", _typeEquip.TypeGroup.ToString() + _typeEquip.TypeTier.ToString() + _typeEquip.TypeId.ToString()) });

        //switch (typeAlly)
        //{
        //    case TypeSqawn.None:
        //        break;
        //    case TypeSqawn.Barel:

        //        break;
        //    case TypeSqawn.MeleeChar:
        //        PoolManager.Spawn<GameObject>("PoliceMeleeAlly", positionSqawn);
        //        break;
        //    case TypeSqawn.RangeChar:
        //        PoolManager.Spawn<GameObject>("PoliceRangeAlly", positionSqawn);
        //        break;
        //    default:
        //        PoolManager.Spawn<GameObject>("PoliceMeleeAlly", positionSqawn);
        //        break;
        //}
    }

    IEnumerator WaitBugSqawn()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        canSqawn = true;
    }

    [Button]
    public void ChangeTypeSqawn(int id, int idSlot, int _energySub)
    {
        typeGroup = (TypeGroup)id;

        typeSlotEquip = (TypeSlotEquip)idSlot;

        SetEnergySub(_energySub);

        SetCanSqawn(true);
    }

    public void SetEnergySub(int _energySub)
    {
        energySub = _energySub;
    }



    public void SetCanSqawn(bool _canSqawn)
    {
        canSqawn = _canSqawn;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canSqawn)
        {
            isTouching = true;
            passPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) / 5 * 15;

        }

        if (canSqawn && isTouching)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 u = Camera.main.ScreenToWorldPoint(Input.mousePosition) / 5 * 15;

                //RaycastHit2D raycastHit = Physics2D.Raycast(u, Input.mousePosition);

                //if (raycastHit)
                //{
                //    if (raycastHit.collider.tag == "Ground")
                //    {


                //        Sqawn(new Vector3(u.x, u.y, 0));
                //    }
                //}

                //Debug.Log(1);

                bool a = true;

                Collider2D[] collider2D = Physics2D.OverlapBoxAll(new Vector2(u.x, u.y), sizeSqawnAllys[((int)typeGroup)], 0);



                foreach (var item in collider2D)
                {
                    if (!item.CompareTag("Ground"))
                    {
                        if (!item.CompareTag("MoveStraight") && !item.CompareTag("Enermy"))
                        {
                            a = false;
                        }
                    }
                }

                if (a)
                {
                    Sqawn(new Vector3(u.x, u.y, 0));
                }
            }
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(canSqawn && isTouching)
        listPosition.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition) / 5 * 15);

        if (canSqawn && isTouching)
        {
            if (Input.GetMouseButton(0))
            {
                //Vector3 u = Camera.main.ScreenToWorldPoint(Input.mousePosition) / 5 * 15;

                //RaycastHit2D raycastHit = Physics2D.Raycast(u, Input.mousePosition);

                //if (raycastHit)
                //{
                //    if (raycastHit.collider.tag == "Ground")
                //    {


                //        Sqawn(new Vector3(u.x, u.y, 0));
                //    }
                //}

                //Debug.Log(1);

                //bool a = true;

                //Collider2D[] collider2D = Physics2D.OverlapBoxAll(new Vector2(u.x, u.y), new Vector2(0.3f, 0.215f), 0);



                //foreach (var item in collider2D)
                //{
                //    if (!item.CompareTag("Ground"))
                //    {
                //        if (!item.CompareTag("MoveStraight"))
                //        {
                //            a = false;
                //        }
                //    }
                //}

                //if (a)
                //{
                //    Sqawn(new Vector3(u.x, u.y, 0));
                //}
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouching = false;
    }
}

public enum TypeSqawn
{
    None,
    Barel,
    MeleeChar,
    RangeChar
}
