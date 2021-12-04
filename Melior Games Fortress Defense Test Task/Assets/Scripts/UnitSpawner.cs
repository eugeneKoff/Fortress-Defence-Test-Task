using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : MonoBehaviour, IBeginDragHandler, IDragHandler
{

    public GameObject unitToInstantiate;


    private void Awake()
    {
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(eventData.position).x, Camera.main.ScreenToWorldPoint(eventData.position).y, 0);

        GameObject go = Instantiate(unitToInstantiate, Vector3.zero, Quaternion.identity);

        go.GetComponent<UnitController>().ToggleGameUnit(false);

        eventData.pointerDrag = go.GetComponentInChildren<BoxCollider2D>().gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }


    public void SpawnUnit()
    {
        //Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Instantiate(unitToInstantiate, position, Quaternion.identity);
    }
}
