using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager instance;
    public static ProjectManager Instance => instance ??= FindObjectOfType<ProjectManager>();

    protected virtual void Awake()
    {
        if(!instance)
        {
            Destroy(gameObject);
            return; 
        }
        DontDestroyOnLoad(instance);
    }
}
