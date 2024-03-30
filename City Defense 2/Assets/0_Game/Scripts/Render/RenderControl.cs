using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

[RequireComponent(typeof(AnimationBase))]
public class RenderControl : MonoBehaviour
{
    [SerializeField] private bool lockFlip;

    [SerializeField] private bool dontScaleGetHit;

    [SerializeField] private bool isEffectSqawn;

    [SerializeField] private ParticleSystem particleSqawn;

    [SerializeField] protected TypeRender typeRender;

    //[SerializeField] protected List<Material> materials;

    [SerializeField] protected AnimationBase animation;

    public AnimationBase Animation => animation;

    [ShowIf("typeRender", TypeRender.Mesh)] [SerializeField] protected MeshRenderer mesh;

    [ShowIf("typeRender", TypeRender.Sprite)] [SerializeField] protected SpriteRenderer spriteRenderer;

    protected MaterialPropertyBlock _propBlock;

    private Tween tweenScale;

    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if(particleSqawn != null)
        {
            particleSqawn.Stop();

            particleSqawn.Play();
        }

        if (typeRender == TypeRender.Mesh)
        {
            initialScale = mesh.transform.localScale;
        }


    }

    public virtual void Init()
    {
        _propBlock = new MaterialPropertyBlock();

        if (isEffectSqawn)
        {
            transform.localScale = Vector3.zero;

            transform.DOScale(Vector3.one, 0.1f).SetUpdate(true);
        }

        //switch (typeRender)
        //{
        //    case TypeRender.Mesh:

        //        mesh = gameObject.GetComponent<MeshRenderer>();

        //        break;
        //    case TypeRender.Sprite:

        //        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //        break;
        //}

        //ChangeMaterial();
    }

    public virtual void SetFlip(bool isTrue)
    {
        if (lockFlip)
        {
            return;
        }

        animation.SetFlip(isTrue);
    }

    public void RotateGunTo(Vector3 target)
    {

    }

    public virtual void OnBlink()
    {

    }

    public virtual void OnGetHit()
    {
        try
        {
            DOTween.To((x) =>
            {

                switch (typeRender)
                {
                    case TypeRender.Mesh:

                        mesh.GetPropertyBlock(_propBlock);

                        // Assign our new value.

                        _propBlock.SetFloat("_HitEffectGlow", x);

                        // Apply the edited values to the renderer.

                        mesh.SetPropertyBlock(_propBlock);

                        break;
                    case TypeRender.Sprite:

                        spriteRenderer.GetPropertyBlock(_propBlock);

                        // Assign our new value.

                        _propBlock.SetFloat("_HitEffectGlow", x);

                        // Apply the edited values to the renderer.

                        spriteRenderer.SetPropertyBlock(_propBlock);

                        break;
                }

            }, 0, 15f, 0.15f).OnComplete(() =>
            {
                DOTween.To((x) =>
                {

                    switch (typeRender)
                    {
                        case TypeRender.Mesh:

                            mesh.GetPropertyBlock(_propBlock);

                            // Assign our new value.

                            _propBlock.SetFloat("_HitEffectGlow", x);

                            // Apply the edited values to the renderer.

                            mesh.SetPropertyBlock(_propBlock);

                            break;
                        case TypeRender.Sprite:

                            spriteRenderer.GetPropertyBlock(_propBlock);

                            // Assign our new value.

                            _propBlock.SetFloat("_HitEffectGlow", x);

                            // Apply the edited values to the renderer.

                            spriteRenderer.SetPropertyBlock(_propBlock);

                            break;
                    }

                }, 15, 0, 0.15f);
            });

            if (!dontScaleGetHit)
            {
                if (typeRender == TypeRender.Mesh)
                {
                    if (tweenScale != null)
                    {
                        return;
                        //tweenScale.Kill();
                    }

                    tweenScale = mesh.transform.DOScale(new Vector3(0.8f, 1.2f, 0) * initialScale.x, 0.1f).OnComplete(() =>
                    {
                        tweenScale = mesh.transform.DOScale(new Vector3(1.2f, 0.8f, 0) * initialScale.x, 0.1f).OnComplete(() =>
                        {
                            tweenScale = mesh.transform.DOScale(initialScale, 0.1f).OnComplete(() => { tweenScale = null; });
                        });
                    });
                }
            }
        }
        catch
        {

        }




    }

    public virtual void OnDie()
    {
        try
        {
            if (typeRender == TypeRender.Mesh)
            {
                DOTween.To((x) =>
                {

                    switch (typeRender)
                    {
                        case TypeRender.Mesh:

                            mesh.GetPropertyBlock(_propBlock);

                            // Assign our new value.

                            _propBlock.SetFloat("_HitEffectGlow", x);

                            // Apply the edited values to the renderer.

                            mesh.SetPropertyBlock(_propBlock);

                            break;
                        case TypeRender.Sprite:

                            spriteRenderer.GetPropertyBlock(_propBlock);

                            // Assign our new value.

                            _propBlock.SetFloat("_HitEffectGlow", x);

                            // Apply the edited values to the renderer.

                            spriteRenderer.SetPropertyBlock(_propBlock);

                            break;
                    }

                }, 0, 25f, 0.15f).OnComplete(() =>
                {
                    DOTween.To((x) =>
                    {

                        switch (typeRender)
                        {
                            case TypeRender.Mesh:

                                mesh.GetPropertyBlock(_propBlock);

                                // Assign our new value.

                                _propBlock.SetFloat("_HitEffectGlow", x);

                                // Apply the edited values to the renderer.

                                mesh.SetPropertyBlock(_propBlock);

                                break;
                            case TypeRender.Sprite:

                                spriteRenderer.GetPropertyBlock(_propBlock);

                                // Assign our new value.

                                _propBlock.SetFloat("_HitEffectGlow", x);

                                // Apply the edited values to the renderer.

                                spriteRenderer.SetPropertyBlock(_propBlock);

                                break;
                        }

                    }, 25, 0, 0.15f);
                });
            }






            DOTween.To((x) =>
            {

                switch (typeRender)
                {
                    case TypeRender.Mesh:

                        mesh.GetPropertyBlock(_propBlock);

                        // Assign our new value.

                        _propBlock.SetFloat("_FadeAmount", x);

                        // Apply the edited values to the renderer.

                        mesh.SetPropertyBlock(_propBlock);

                        break;
                    case TypeRender.Sprite:

                        spriteRenderer.GetPropertyBlock(_propBlock);

                        // Assign our new value.

                        _propBlock.SetFloat("_FadeAmount", x);

                        // Apply the edited values to the renderer.

                        spriteRenderer.SetPropertyBlock(_propBlock);

                        break;
                }

            }, 0f, 2f, 1f).SetDelay(animation.GetTimeAnimation(TypeAnimation.Die, true));
        }
        catch
        {

        }



    }

    [Button]
    public virtual void ChangeMaterial()
    {
        //for (int i = 0; i < mesh.materials.Length; i++)
        //{
        //    mesh.materials[i] = materials[i];
        //}

        //switch (typeRender)
        //{
        //    case TypeRender.Mesh:

        //        animation.OnChangeMaterial(mesh.materials, materials);

        //        break;
        //    case TypeRender.Sprite:

        //        //animation.OnChangeMaterial(spriteRenderer.materials, materials);

        //        break;
        //}


    }
}