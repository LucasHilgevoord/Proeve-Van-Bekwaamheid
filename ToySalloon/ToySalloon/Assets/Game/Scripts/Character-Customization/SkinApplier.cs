using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Gender
{
    FEMALE,
    MALE
}

public class SkinApplier : MonoBehaviour
{
    [SerializeField] private CharacterAnimator character;
    [SerializeField] private SkeletonAnimation body;
    [SerializeField] private SkeletonAnimation hair;

    private int hairId;
    private int skinColorId;
    private Gender gender;
    string skinName;

    private void Start()
    {
        LoadCurrentSkin();
    }

    public void LoadCurrentSkin()
    {
        SetHair(GameManager.Instance.hairSkin);
        SetSkinColor(GameManager.Instance.bodySkin);
    }

    public void SetGender(bool isMale)
    {
        gender = isMale ? Gender.MALE : Gender.FEMALE;
        SetSkinColor(skinColorId);
        GameManager.Instance.gender = gender;
    }

    public void SetHair(int id)
    {
        hairId = id;
        skinName = id < 10 ? "skin0" + id : "skin" + id;
        hair.skeleton.SetSkin(skinName);
        GameManager.Instance.hairSkin = skinName;
    }

    public void SetHair(string _skinName)
    {
        SetHair(StringToInt(_skinName));
    }

    public void SetSkinColor(int id)
    {
        skinColorId = id;
        skinName = gender == Gender.MALE ? "m_" : "f_";
        skinName += id < 10 ? "skin0" + id : "skin" + id;
        body.skeleton.SetSkin(skinName);
        GameManager.Instance.bodySkin = skinName;
    }

    public void SetSkinColor(string _skinName)
    {
        SetSkinColor(StringToInt(_skinName));
    }

    private int StringToInt(string s)
    {
        string[] splitArray = s.Split(char.Parse("n"));
        string id = splitArray[1];
        bool success = System.Int32.TryParse(id, out int number);
        if (success)
        {
            return number;
        }
        else
        {
            return 0;
        }
    }
}
