using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAnimator : CharacterAnimator
{
    //public SkeletonAnimation body;
    //public PlayerSkeletons bodyData;

    //Can be replaced with skin lenght if I figure out how.
    private int minSkinId = 1;
    private int maxSkinId = 6;
    private Gender gender;

    protected override void Start()
    {
        CreateNewSkin();
        base.Start();
    }

    private void CreateNewSkin()
    {
        int id = Random.Range(minSkinId, maxSkinId);
        string newSkin = id < 10 ? "skin0" + id : "skin" + id;
        bodySkinName = PickGender() + newSkin;
    }

    private string PickGender()
    {
        Gender randomGender = (Gender)Random.Range(0, System.Enum.GetValues(typeof(Gender)).Length);
        switch (randomGender)
        {
            case Gender.FEMALE:
                return "f_";
            case Gender.MALE:
                return "m_";
            default:
                return "null_";
        }
    }
}
