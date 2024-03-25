using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject waveText;
    private void Start()
    {
        Color waveTextColor = waveText.GetComponent<TMP_Text>().color;
        waveTextColor.a = 0f;
        waveText.GetComponent<TMP_Text>().color = waveTextColor;
    }
    public void ShowWaveText(int waveValue)
    {
        TMP_Text text = waveText.GetComponent<TMP_Text>();
        text.text = "Wave " + waveValue;
        Sequence textSequence = DOTween.Sequence();

        textSequence.Append(text.DOFade(1f, 1f));

        textSequence.AppendInterval(3f);

        textSequence.Append(text.DOFade(0f, 1f));
    }
}
