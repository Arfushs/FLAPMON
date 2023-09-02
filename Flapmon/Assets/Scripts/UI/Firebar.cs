using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Firebar : MonoBehaviour
{
    [SerializeField] private PlayerShooting player;
    [SerializeField] private Sprite chargedTexture;
    [SerializeField] private Sprite unChargedTexture;
    [SerializeField] private TextMeshProUGUI chargedText;
    [SerializeField] private GameObject textureContainer;


    public void OnPlayerShoot()
    {
        chargedText.gameObject.SetActive(false);
        Image[] bars = textureContainer.GetComponentsInChildren<Image>();
        foreach (Image i in bars)
        {
            i.sprite = unChargedTexture;
        }
    }

    private void Update()
    {
        BarFixed();
    }

    private void BarFixed()
    {
        Image[] bars = textureContainer.GetComponentsInChildren<Image>();
        float fire_cooldown = player.GetCooldown();
        float elapsed_time = fire_cooldown - player.GetTimer();

        if (elapsed_time >= fire_cooldown / 10)
        {
            bars[0].sprite = chargedTexture;
        }

        if (elapsed_time >= fire_cooldown * 2 / 10)
        {
            bars[1].sprite = chargedTexture;
        }

        if (elapsed_time >= fire_cooldown * 3 / 10)
        {
            bars[2].sprite = chargedTexture;
        }

        if (elapsed_time >= fire_cooldown * 4 / 10)
        {
            bars[3].sprite = chargedTexture;
        }

        if (elapsed_time >= fire_cooldown * 5 / 10)
        {
            bars[4].sprite = chargedTexture;
        }

        if (elapsed_time >= fire_cooldown * 6 / 10)
        {
            bars[5].sprite = chargedTexture;
        }

        if (elapsed_time >= fire_cooldown * 7 / 10)
        {
            bars[6].sprite = chargedTexture;
        }

        if (elapsed_time >= fire_cooldown * 8 / 10)
        {
            bars[7].sprite = chargedTexture;
        }

        if (elapsed_time >= fire_cooldown * 9 / 10)
        {
            bars[8].sprite = chargedTexture;
        }

        if (elapsed_time >= fire_cooldown)
        {
            bars[9].sprite = chargedTexture;
            chargedText.gameObject.SetActive(true);
        }
    }
    
}
