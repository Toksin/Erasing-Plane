using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameInterfaceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinAmount;

    private void Update()
    {      
        coinAmount.text = GameStates.Instance.GetTemporaryCoins().ToString();
    }
}
