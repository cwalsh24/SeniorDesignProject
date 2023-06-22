using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RupeeWallet : MonoBehaviour
{
    public int currentRupees;
    
    [SerializeField] private TMP_Text rupeeText;

    //Adding this to attempt to put in a sound effect for attacking
    [SerializeField] private AudioSource RupeeSound;

    private void Update() {
        UpdateRupeeText();
    }

    public void UpdateRupeeText() {
        rupeeText.text = currentRupees.ToString("D3");
    }

    public void IncreaseRupeeCount(int amount) {
        currentRupees += amount;
        RupeeSound.Play();
    }
}
