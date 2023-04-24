using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Image[] GetAllChildImages(){
        int childCount = gameObject.transform.childCount;
        List<Image> childImagesList = new List<Image>();

        for (int i = 0; i < childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            Image imageComponent = child.GetComponent<Image>();
            if (imageComponent != null){
                childImagesList.Add(imageComponent);
            }
        }

        return childImagesList.ToArray();
    }
}
