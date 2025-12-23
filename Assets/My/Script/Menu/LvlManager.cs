using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class LvlManager : MonoBehaviour
{
    [SerializeField] private int lvlNow;
    [SerializeField] private LVLHolder lvlHolderPrefab;
    [SerializeField] private GameObject[] lvlList;
    [SerializeField] private int BeatedLvl;
    void Start()
    {
        BeatedLvl = PlayerPrefs.GetInt("Level", 0);
        SpawnLvl();
    }

    private void SpawnLvl()
    {
        int j = 0;
        for(int i = 1; i < lvlNow + 1; i++)
        {
            if (i == 11) j++;
            LVLHolder lvlHolder = Instantiate(lvlHolderPrefab, lvlList[j].transform);
            lvlHolder.Init(i, i > BeatedLvl);
        }
    }
}
