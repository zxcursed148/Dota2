using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    public static WindowsManager Layout;
    [SerializeField] private GameObject[] windows;
    // Start is called before the first frame update

    private void Start()
    {
        OpenLayout("Loading");
    }

    private void Awake()
    {
        Layout = this;
        foreach (GameObject window in windows)
        {
            window.SetActive(false);
        }
    }
    public void OpenLayout(string name)
    {
        foreach (GameObject window in windows)
        {
            {
                if (window.name == name) window.SetActive(true);
                else window.SetActive(false);
            }
        }
    }
}
