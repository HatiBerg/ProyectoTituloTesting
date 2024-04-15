using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public UnityEngine.UI.Image healthBar;
    public float actualHealth;
    public float maxHealth;

    private void Start()
    {
        
    }

    void Update()
    {
        healthBar.fillAmount = actualHealth/maxHealth;
    }
}
