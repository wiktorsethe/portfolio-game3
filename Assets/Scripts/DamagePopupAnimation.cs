using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopupAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve opacityCurve;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private AnimationCurve heightCurve;
    private TMP_Text text;
    private float time = 0f;
    private Vector3 origin;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<TMP_Text>();
        origin = transform.position;
    }
    private void Update()
    {
        text.color = new Color(0.7175279f, 0, 1, opacityCurve.Evaluate(time));
        transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
        transform.position = origin + new Vector3(0, 1 + heightCurve.Evaluate(time), 0);
        time += Time.deltaTime;
    }
}
