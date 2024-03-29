using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ResourceContainer : MonoBehaviour
{
    public static ResourceContainer Instance { get; private set; }

    public List<PlayerModel> playerModels;
    public List<Sprite> enemyModelIcons;
    public List<Material> playerColors;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
