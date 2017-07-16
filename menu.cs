using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;


public class menu : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
    public RectTransform tx;
    public bool isopen;

	// Use this for initialization
	void Start () {
        tx = transform.FindChild("tx").GetComponent<RectTransform>();
        isopen = false;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 scole = tx.lossyScale;
        scole.y = Mathf.Lerp(scole.y, isopen ? 1 : 0, Time.deltaTime *3000);
        scole.x = 1;
        scole.z = 1;
        tx.localScale = scole;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        isopen = true;
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isopen = false;
    }
}
