using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class FinishManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHealth;

    [SerializeField] private Image imgCurrent;

    [SerializeField] private List<Sprite> listRenderMap;

    [SerializeField] private SpriteRenderer spriteFinish;

    [SerializeField] private ParticleSystem particleSystemRevive;

    [SerializeField] private float radius;

    private MaterialPropertyBlock _propBlock;

    // Start is called before the first frame update
    void Start()
    {
        Init();

        LevelManagerMainGame.Instance.ReviveEvent += OnRevive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        HealthGamePlay.Instance.OnSubHealthGamePlay += OnSubHealth;

        int health = GameManager.Instance.DataManager.DataManagerMainGame.DataGame.HealthInGame;

        textHealth.text = health.ToString();

        imgCurrent.fillAmount = 1;

        _propBlock = new MaterialPropertyBlock();

        int idCurrentMap = GameManager.Instance.DataManager.GetMap();

        spriteFinish.sprite = listRenderMap[idCurrentMap - 1];
    }

    public void OnSubHealth(int current, int max)
    {
        textHealth.text = current.ToString();

        imgCurrent.fillAmount = (float)current / (float)max;

        //

        DOTween.To((x) =>
        {

            spriteFinish.GetPropertyBlock(_propBlock);

            // Assign our new value.

            _propBlock.SetFloat("_GradBlend", x);

            // Apply the edited values to the renderer.

            spriteFinish.SetPropertyBlock(_propBlock);

        }, 0, 0.6f, 0.15f).SetEase(DG.Tweening.Ease.Linear).OnComplete(() =>
        {
            DOTween.To((x) =>
            {
                spriteFinish.GetPropertyBlock(_propBlock);

                // Assign our new value.

                _propBlock.SetFloat("_GradBlend", x);

                // Apply the edited values to the renderer.

                spriteFinish.SetPropertyBlock(_propBlock);

            }, 0.6f, 0, 0.15f).SetEase(DG.Tweening.Ease.Linear);
        });
    }

    public void OnRevive()
    {
        particleSystemRevive.Play();

        List<IContactObject> enermies = new List<IContactObject>(CharManager.Instance.Enermies);

        //float minDistance = 10000000;

        //List<IContactObject> contactObjectsEnermy = new List<IContactObject>();

        for (int i = 0; i < enermies.Count; i++)
        {
            if(enermies[i] == null)
            {
                continue;
            }

            IContactObject icontactEnermy = enermies[i];

            float distance = Vector3.Distance(transform.position, icontactEnermy.GetBody().position);

            if (distance <= radius)
            {
                if (icontactEnermy.GetHealth().GetHealth() > 0)
                {
                    //minDistance = distance;
                    icontactEnermy.Hited(TypeWeapon.NuclearBoom, 100000000);
                    //continue;
                }
            }
        }

        CharManager.Instance.CheckWin();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;

        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    }

#endif
}
