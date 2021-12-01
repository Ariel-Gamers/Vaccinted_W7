using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject button_prefab;
    Button button;
    [SerializeField] int type;
    GameSystem gs;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
        gs = FindObjectOfType<GameSystem>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        gs.createCell(type);
    }
    // Update is called once per frame
}
