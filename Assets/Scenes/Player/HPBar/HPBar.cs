using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HPBar : MonoBehaviour
{
    private UnityEngine.UI.Slider m_pSlider;
    private TextMeshProUGUI m_pText;


    private void Awake()
    {
        m_pSlider = GetComponentInChildren<UnityEngine.UI.Slider>();
        m_pText = GetComponentInChildren<TextMeshProUGUI>();
        
    }
    public void Init(int _iPlayerMaxHP)
    {
        m_pSlider.maxValue = _iPlayerMaxHP;//먼저 설정
        m_pSlider.value = _iPlayerMaxHP;
      
    }

    public void SetHP(int _iCur, int _iMax)
    {
        m_pText.text = _iCur + " / 100";
        StartCoroutine(StartLerp(_iCur, _iMax));
    }

    private IEnumerator StartLerp(int _iCur, int _iMax)
    {
        m_pSlider.maxValue = _iMax;

        float fStart = m_pSlider.value;
        float fEnd = _iCur;
        float fDuration = 0.2f; 
        float fTime = 0f;

        while (fTime < fDuration)
        {
            fTime += Time.deltaTime;
            m_pSlider.value = Mathf.Lerp(fStart, fEnd, fTime / fDuration);
            yield return null;
        }

        m_pSlider.value = fEnd;
    }
}
