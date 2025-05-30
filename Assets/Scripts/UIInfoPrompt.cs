using System.Collections;
using TMPro;
using UnityEngine;

public class UIInfoPrompt : MonoBehaviour
{
    public static UIInfoPrompt Instance { get; private set; }

    [SerializeField]
    Canvas m_canvas = null;

    [SerializeField]
    TextMeshProUGUI m_textTitle = null, m_textInfo = null;

    WaitForSeconds m_hideTime = new WaitForSeconds(2);
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
    public void LoadNShow(string a_strTitle, string a_strInfo)
    {
        m_canvas.enabled = true;
        m_textInfo.text = a_strInfo;
        m_textTitle.text = a_strTitle;
        // if (m_coroutine != null)
        // {
        //     StopCoroutine(m_coroutine);
        // }
        // m_coroutine = StartCoroutine(HideAfterTimeDelay());
    }
    IEnumerator HideAfterTimeDelay()
    {
        yield return m_hideTime;
        Hide();
    }
    public void Hide()
    {
        m_canvas.enabled = false;
    }
}
