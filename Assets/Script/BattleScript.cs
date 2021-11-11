using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClanBoatSpawner {
    public ClanEnum clanEnum;
    public Vector3 spawnLocation;
    public Quaternion spawnRotation;
    public List<BoatInfo> listBoatPfab;
    public List<BoatInfo> listBoatIns;
    public ClanBoatSpawner(ClanEnum clanEnum, Vector3 spawnDirection, List<BoatInfo> listBoatPfab) {
        this.clanEnum = clanEnum;
        this.spawnLocation = spawnDirection.normalized * 800;
        this.spawnRotation = Quaternion.LookRotation(-spawnDirection);
        this.listBoatPfab = listBoatPfab;
    }
}

public class BattleScript : MonoBehaviour {

    public static Dictionary<ClanEnum, ClanBoatSpawner> dicClan = new Dictionary<ClanEnum, ClanBoatSpawner>();
    public static BoatInfo playerBoat;
    public static Dictionary<ClanEnum, List<BoatInfo>> dicEnemyBoat = new Dictionary<ClanEnum, List<BoatInfo>>();

    private bool LoadBoat() {
        playerBoat = Resources.Load<BoatInfo>("Boat/Friendly Medium Fighter");

        BoatInfo terraLF = Resources.Load<BoatInfo>("Boat/Friendly Light Fighter");
        List<BoatInfo> listBoatTerra = new List<BoatInfo> {
            terraLF,
            terraLF,
            terraLF,
        };
        dicClan.Add(ClanEnum.Terra, new ClanBoatSpawner(ClanEnum.Terra, -Vector3.forward, listBoatTerra));

        BoatInfo pirateLF = Resources.Load<BoatInfo>("Boat/Pirate Light Fighter");
        BoatInfo pirateMF = Resources.Load<BoatInfo>("Boat/Pirate Medium Fighter");
        List<BoatInfo> listBoatThief = new List<BoatInfo> {
            pirateLF,
            pirateLF,
            pirateLF,
            pirateMF
        };
        dicClan.Add(ClanEnum.Thief, new ClanBoatSpawner(ClanEnum.Thief, Vector3.forward, listBoatThief));
        return true;
    }


    private void AddToEnemy(ClanEnum myClan, BoatInfo myBoat) {
        List<ClanEnum> listEnemyClan = ClanRelation.GetHater(myClan);
        foreach (ClanEnum clanEnum in listEnemyClan) {
            if (!dicEnemyBoat.ContainsKey(clanEnum)) {
                dicEnemyBoat.Add(clanEnum, new List<BoatInfo>());
            }
            dicEnemyBoat[clanEnum].Add(myBoat);
        }
    }

    private void Awake() {
        LoadBoat();

        playerBoat = Instantiate(playerBoat, dicClan[ClanEnum.Terra].spawnLocation + new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), Random.Range(-200, 200)), dicClan[ClanEnum.Terra].spawnRotation).SetUp(ClanEnum.Terra);
        Camera.main.transform.parent = playerBoat.transform;
        Camera.main.transform.localPosition = Camera.main.transform.position;

        playerBoat.GetComponent<BoatEngine>().SetUp(playerBoat.gameObject.AddComponent<BoatInputHuman2>().SetUp(playerBoat), true);
        //    IBoatInput inputHuman = FindObjectOfType<BoatInputHuman>().SetUp(playerBoat);
        //    playerBoat.GetComponent<BoatController>().SetUp(inputHuman);


        playerBoat.GetComponent<BoatHp>().SetUp(FindObjectOfType<DamageDisplayPlayer>());
        AddToEnemy(ClanEnum.Terra, playerBoat);


        DamageDisplayAI hpAIPfab = Resources.Load<DamageDisplayAI>("HullPoint/HullPointDisplay");
        foreach (ClanBoatSpawner clan in dicClan.Values) {
            foreach (BoatInfo boatPfab in clan.listBoatPfab) {
                BoatInfo boatIns = Instantiate(boatPfab, clan.spawnLocation + new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), Random.Range(-200, 200)), clan.spawnRotation).SetUp(clan.clanEnum);
                boatIns.GetComponent<BoatHp>().SetUp(Instantiate(hpAIPfab, boatIns.transform.position, boatIns.transform.rotation, boatIns.transform));
                boatIns.GetComponent<BoatEngine>().SetUp(boatIns.gameObject.AddComponent<BoatInputAI2>().SetUp(boatIns), false);
                //boatIns.GetComponent<BoatController>().SetUp(boatIns.gameObject.AddComponent<BoatInputAI>().SetUp(boatIns));
                boatIns.name = clan.clanEnum.ToString() + Random.Range(100, 999);
                AddToEnemy(clan.clanEnum, boatIns);
            }
        }


    }

    public static BoatInfo FindEnemy(ClanEnum factionEnum) {
        List<BoatInfo> listEnemy = dicEnemyBoat[factionEnum];
        listEnemy.RemoveAll(item => !item.IsTargetable());
        return listEnemy.Count > 0 ? listEnemy[Random.Range(0, listEnemy.Count)] : null;
    }
}
