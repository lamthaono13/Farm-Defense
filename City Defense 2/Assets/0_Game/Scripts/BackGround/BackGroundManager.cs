using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabPieceBackGround;

    [SerializeField] private float widthPieceBackGround;

    [SerializeField] private float heightPieceBackGround;

    [SerializeField] private int numberPieceHorizontal;

    [SerializeField] private int numberPieceVertical;

    private Dictionary<(int, int) ,GameObject> listPieces;

    private bool canFollow;

    private float3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        FirstSqawnBG();
    }

    private void FirstSqawnBG()
    {
        listPieces = new Dictionary<(int, int), GameObject>();

        //GameObject objOrigin = Instantiate(prefabPieceBackGround, transform);

        //listPieces.Add((0, 0), objOrigin);

        Vector3 originPosition = Vector3.zero;

        for (int i = - numberPieceHorizontal; i < numberPieceHorizontal + 1; i++)
        {

            for(int j = - numberPieceVertical; j < numberPieceVertical + 1; j++)
            {
                //if (i == 0 && j == 0)
                //{
                //    continue;
                //}

                GameObject objBorder = Instantiate(prefabPieceBackGround, transform);

                objBorder.transform.position = new Vector3(i * (widthPieceBackGround), j * (heightPieceBackGround), 0) + originPosition;

                listPieces.Add((i, j), objBorder);
            }
        }
    }

    public void SetPlayer()
    {
        //player = entity;
        //canFollow = true;
    }

    public void SetFollow(bool isTrue)
    {
        canFollow = isTrue;
    }

    // Update is called once per frame
    void Update()
    {
        //FollowPlayer();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    public void UpdatePlayerPosition(float3 _positionPlayer)
    {
        playerPosition = _positionPlayer;
    }

    private void FollowPlayer()
    {
        if (canFollow)
        {
            float3 playerPosition = this.playerPosition;

            Vector2 u = new Vector2(playerPosition.x, playerPosition.y);

            bool a = listPieces.TryGetValue((0, 0), out GameObject objOrigin);

            Vector2 originPosition = objOrigin.transform.position;

            int xSaiSo = 0;

            int ySaiSo = 0;

            xSaiSo = ((int)((u.x - originPosition.x) / (widthPieceBackGround / 2)));

            ySaiSo = ((int)((u.y - originPosition.y) / (heightPieceBackGround / 2)));

            Vector3 newOriginPositon = originPosition + new Vector2(widthPieceBackGround * xSaiSo, heightPieceBackGround * ySaiSo);

            ChangeOrigin(xSaiSo, ySaiSo, newOriginPositon);
        }
    }

    public void ChangeOrigin(int xSaiSo, int ySaiSo, Vector3 originPosition)
    {
        if(xSaiSo == 0 && ySaiSo == 0)
        {
            return;
        }

        Dictionary<(int, int), GameObject> listCoppy = new Dictionary<(int, int), GameObject>();

        for (int i = -numberPieceHorizontal; i < numberPieceHorizontal + 1; i++)
        {

            for (int j = -numberPieceVertical; j < numberPieceVertical + 1; j++)
            {
                //if (i == 0 && j == 0)
                //{
                //    continue;
                //}

                GameObject objBorder = listPieces[(i, j)];

                objBorder.transform.position = new Vector3(i * (widthPieceBackGround), j * (heightPieceBackGround), 0) + originPosition;

                //listCoppy.Add((i, j), objBorder);
            }
        }

        //listPieces = listCoppy;
    }
}

