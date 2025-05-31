using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    private GameObject database;

    [SerializeField] private Button button;
    [SerializeField] private ProductAd productAd;

    private void Start()
    {
        database = GameObject.Find("projectData");

        button.onClick.AddListener(buttonClick);
    }

    private void buttonClick()
    {
        database.GetComponent<PlayerSaveData>().AddProductToCart(productAd);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(buttonClick);
    }
}
