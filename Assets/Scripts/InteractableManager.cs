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
            StartCoroutine(ShowUIAfterFrame(m_interactableItem.Title, m_interactableItem.Info));
        }
    }
    IEnumerator ShowUIAfterFrame(string a_strTitle, string a_strInfo)
    {
        yield return m_waitForEndOfFrame;
        UIInfoPrompt.Instance.LoadNShow(a_strTitle, a_strInfo);
    }
}
