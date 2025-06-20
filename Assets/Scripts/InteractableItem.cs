using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [SerializeField]
    string m_strTitle = string.Empty;
    public string Title => m_strTitle;

    [SerializeField]
    string m_strInfo = string.Empty;
    public string Info => m_strInfo;

    [SerializeField]
    List<InteractableItemData> m_itemData = null;
    public List<InteractableItemData> ItemData => m_itemData;

    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(m_strTitle))
        {
            m_strTitle = transform.name;
        }
    }
}
