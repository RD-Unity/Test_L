using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    Ray m_ray;
    RaycastHit m_rayCastHit;
    InteractableItem m_interactableItem;
    WaitForEndOfFrame m_waitForEndOfFrame = new WaitForEndOfFrame();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(m_ray, out m_rayCastHit))
            {
                CheckForClick(m_rayCastHit.transform);
            }
        }
    }

    void CheckForClick(Transform m_clickedObject)
    {
        if (m_clickedObject.TryGetComponent<InteractableItem>(out m_interactableItem))
        {
            StartCoroutine(ShowUIAfterFrame(m_interactableItem.Title, m_interactableItem.ItemData));
        }
    }
    IEnumerator ShowUIAfterFrame(string a_strTitle, List<InteractableItemData> a_itemData)
    {
        yield return m_waitForEndOfFrame;
        UIInfoPrompt.Instance.LoadNShow(a_strTitle, a_itemData);
    }
}

[Serializable]
public class InteractableItemData
{
    [SerializeField] public string m_strHeader = string.Empty;
    [SerializeField, TextArea] public string m_strInformation = string.Empty;
    [SerializeField] public Sprite m_spriteIcon = null;
}
