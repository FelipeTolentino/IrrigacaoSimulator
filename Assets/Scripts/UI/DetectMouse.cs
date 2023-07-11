using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectMouse : MonoBehaviour
{
    public bool IsMouseOver() {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
