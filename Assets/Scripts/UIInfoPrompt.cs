using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoPrompt : MonoBehaviour
{
    public static UIInfoPrompt Instance { get; private set; }

    [SerializeField]
    Canvas m_canvas = null;

    [SerializeField]
    GraphicRaycaster m_graphicRayCaster = null;

    [SerializeField]
    TextMeshProUGUI m_textTitle = null, m_textInfo = null, m_textHeader = null;

    [SerializeField]
    Image m_imageIcon = null;

    [SerializeField]
    Button m_buttonPrevious = null, m_buttonNext = null;

    WaitForSeconds m_hideTime = new WaitForSeconds(2);
    int m_iCurrentInfoIndex = 0;

    List<InteractableItemData> m_itemData = null;

    // Coroutine m_coroutine = null;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void LoadNShow(string a_strTitle, List<InteractableItemData> a_itemData)
    {
        m_itemData = a_itemData;
        m_iCurrentInfoIndex = 0;
        m_canvas.enabled = true;
        m_graphicRayCaster.enabled = true;
        m_textTitle.text = a_strTitle;
        UpdateData();
    }
    void UpdateData()
    {
        m_textHeader.text = m_itemData[m_iCurrentInfoIndex].m_strHeader;
        m_textInfo.text = m_itemData[m_iCurrentInfoIndex].m_strInformation;
        m_imageIcon.sprite = m_itemData[m_iCurrentInfoIndex].m_spriteIcon;
        m_buttonPrevious.interactable = !(m_iCurrentInfoIndex == 0);
        m_buttonNext.interactable = !(m_iCurrentInfoIndex == (m_itemData.Count - 1));
    }
    IEnumerator HideAfterTimeDelay()
    {
        yield return m_hideTime;
        Hide();
    }
    public void Hide()
    {
        m_canvas.enabled = false;
        m_graphicRayCaster.enabled = false;
    }

    public void OnClick_Next()
    {
        m_iCurrentInfoIndex++;
        // if (m_iCurrentInfoIndex == m_itemData.Count)
        // {
        //     m_iCurrentInfoIndex = 0;
        // }
        UpdateData();

    }
    public void OnClick_Previous()
    {
        m_iCurrentInfoIndex--;
        // if (m_iCurrentInfoIndex == -1)
        // {
        //     m_iCurrentInfoIndex = m_itemData.Count - 1;
        // }
        UpdateData();
    }
}
