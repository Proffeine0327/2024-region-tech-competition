using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    private DataManager dataManager => DataManager.Instance;

    public List<Transform> points;
    public RectTransform infoGroup;
    public TextMeshProUGUI infoPrefab;

    private void Start()
    {
        dataManager.ranking = dataManager.ranking.OrderBy(x => x.time).Take(Mathf.Min(dataManager.ranking.Count, 5)).ToList();
        points.For((p, index) =>
        {
            if (dataManager.ranking.Count <= index) return;
            var rankingData = dataManager.ranking[index];
            var model = Instantiate(rankingData.model);
            model.transform.position = p.position;
            model.transform.rotation = p.rotation;
            model.GetComponent<MeshRenderer>().sharedMaterial = rankingData.color;

            var text = Instantiate(infoPrefab, infoGroup);
            text.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(model.transform.position);
            text.text = $" \n{rankingData.time:#0.00}\n{rankingData.datetime:yy/MM/dd}\n{rankingData.datetime:HH:mm:ss}";
        });
    }
}
