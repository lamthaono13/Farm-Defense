using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioWin;

    [SerializeField] private AudioClip audioLose;

    [SerializeField] private AudioSource audioSourceBgLobby;

    [SerializeField] private AudioSource audioSourceInGame;

    [SerializeField] private AudioSource audioSourceEndGame;

    [SerializeField] private AudioSource audioSourceButton;

    [SerializeField] private AudioSource audioSourceSqawn;

    [SerializeField] private AudioSource audioSourceChooseTroop;

    [SerializeField] private AudioSource audioSourceZombie;

    [SerializeField] private AudioSource audioSourceGun;

    [SerializeField] private AudioSource audioSourceGun2;

    [SerializeField] private AudioSource audioSourcePunch;

    [SerializeField] private AudioSource audioSourceDie;

    [SerializeField] private AudioSource audioSourceDestroy;

    [SerializeField] private AudioSource audioSourceExplosition;

    [SerializeField] private AudioSource audioSourceBoom;

    [SerializeField] private AudioSource audioSourceEnermyBoom;

    [SerializeField] private AudioSource audioSourceThrow;

    [SerializeField] private PoolSoundManager poolSoundManager;

    public PoolSoundManager PoolSoundManager => poolSoundManager;

    // use to manager all sound in game

    private void Start()
    {

    }

    public void Init()
    {
        GameManager.Instance.DataManager.OnChangeMusic += OnChangeMusic;

        GameManager.Instance.DataManager.OnChangeSound += OnChangeSound;

        poolSoundManager.Init();
    }

    private void OnChangeMusic(bool isActive)
    {
        if (isActive)
        {
            audioSourceInGame.volume = 1;
            audioSourceBgLobby.volume = 1;
            audioSourceEndGame.volume = 1;
        }
        else
        {
            audioSourceInGame.volume = 0;
            audioSourceBgLobby.volume = 0;
            audioSourceEndGame.volume = 0;
        }
    }

    private void OnChangeSound(bool isActive)
    {

    }

    public void PlaySoundBgLobby(bool isTrue)
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Music))
        {
            if (isTrue)
            {
                audioSourceBgLobby.Play();
                PlaySoundInGame(false);
                PlaySoundEndGame(false, GameResult.NoDeciced);
            }
            else
            {
                audioSourceBgLobby.Stop();
            }
        }
    }

    public void PlaySoundInGame(bool isTrue)
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Music))
        {
            if (isTrue)
            {
                audioSourceInGame.Play();
                PlaySoundBgLobby(false);
                PlaySoundEndGame(false, GameResult.NoDeciced);
            }
            else
            {
                audioSourceInGame.Stop();
            }
        }
    }

    public void PlaySoundEndGame(bool isTrue, GameResult gameResult)
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Music))
        {
            if (isTrue)
            {
                switch (gameResult)
                {
                    case GameResult.Lose:

                        audioSourceEndGame.clip = audioLose;

                        break;
                    case GameResult.Win:

                        audioSourceEndGame.clip = audioWin;

                        break;
                }

                audioSourceEndGame.Play();
                PlaySoundInGame(false);
                PlaySoundBgLobby(false);
            }
            else
            {
                audioSourceEndGame.Stop();
            }
        }
    }

    public void PlaySoundButton()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceButton.Play();
        }
    }

    public void PlaySoundSqawn()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            if (audioSourceSqawn.isPlaying)
            {
                return;
            }

            audioSourceSqawn.Play();
        }
    }

    public void PlaySoundChooseTroop()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceChooseTroop.Play();
        }
    }

    public void PlaySoundZombie()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceZombie.Play();
        }
    }

    public void PlaySoundPunch()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourcePunch.Play();
        }
    }

    public void PlaySoundGun()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceGun.Play();
        }
    }

    public void PlaySoundGun2()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceGun2.Play();
        }
    }

    public void PlaySoundDie()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceDie.Play();
        }
    }

    public void PlaySoundDestroy()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceDestroy.Play();
        }
    }

    public void PlaySoundExplositon()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceExplosition.Play();
        }
    }

    public void PlaySoundBoom()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceBoom.Play();
        }
    }

    public void PlaySoundEnermyBoom()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceEnermyBoom.Play();
        }
    }

    public void PlaySoundThrow()
    {
        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSourceThrow.Play();
        }
    }
}
