using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ClanRelation {

    // Sometime love/hate isn't mutual
    private static readonly Dictionary<ClanEnum, List<ClanEnum>> dicValueHateKey = DefaultDicValueHateKey();

    private static Dictionary<ClanEnum, List<ClanEnum>> DefaultDicValueHateKey() {
        return new Dictionary<ClanEnum, List<ClanEnum>> {
            { ClanEnum.Terra, new List<ClanEnum> { ClanEnum.Thief, ClanEnum.Rebel } },
            { ClanEnum.Xanas, new List<ClanEnum> { ClanEnum.Thief, ClanEnum.Rebel } },
            { ClanEnum.Thief, new List<ClanEnum> { ClanEnum.Terra, ClanEnum.Xanas, ClanEnum.Rebel } },
            { ClanEnum.Rebel, new List<ClanEnum> { ClanEnum.Terra, ClanEnum.Xanas, ClanEnum.Thief } }
        };
    }

    public static List<ClanEnum> GetHater(ClanEnum myClan) {
        return dicValueHateKey[myClan];
    }

    public static bool IsEnemy(ClanEnum myClan, ClanEnum otherClan) {
        return dicValueHateKey[otherClan].Contains(myClan);
    }
}

