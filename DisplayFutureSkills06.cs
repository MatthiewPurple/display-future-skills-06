using MelonLoader;
using HarmonyLib;
using Il2Cpp;
using Il2Cppnewdata_H;
using Il2Cppresult2_H;
using display_future_skills_06;

[assembly: MelonInfo(typeof(DisplayFutureSkills06), "Display Future Skills NO SPOILERS (0.6 ver.)", "1.0.0", "Matthiew Purple")]
[assembly: MelonGame("アトラス", "smt3hd")]

namespace display_future_skills_06;
public class DisplayFutureSkills06 : MelonMod
{
    [HarmonyPatch(typeof(cmpDrawStatus), nameof(cmpDrawStatus.cmpDrawSkill))]
    private class Patch
    {
        public static void Postfix(datUnitWork_t pStock, rstSkillInfo_t pSkillInfo)
        {
            for (int i = 0; i < pSkillInfo.SkillID.Length; i++)
            {
                ushort skillID = pSkillInfo.SkillID[i];
                if (skillID == 0) break;
                if (skillID == 357 && pStock.id == 0)
                {
                    // If you can get Pierce without TDE (mod) but aren't high level level enough
                    if (EventBit.evtBitCheck(2241) && tblHearts.fclHeartsTbl[1].Skill[5].TargetLevel > pStock.level + 1)
                    {
                        cmpStatus._statusUIScr.awaitText[i].text = "<material=\"TMC14\">?"; // Displays a "?"
                    }
                    continue; //Skip Pierce on Demi-fiend
                }

                string name = datSkillName.Get(skillID, pStock.id);
                cmpStatus._statusUIScr.awaitText[i].text = "<material=\"TMC14\">" + name;
            }
        }
    }
}
