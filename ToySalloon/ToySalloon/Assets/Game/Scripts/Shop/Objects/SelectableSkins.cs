using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ObjectSkinLock
{
    public int levelRequirement = 0;
    public int price = 0;
}

[Serializable]
public class ObjectSkin
{
    public GameObject skin;
    public Image icon;
    public ObjectSkinLock locked;
    public bool isSelected;
}

[Serializable]
public class SelectableSkins
{
    public ObjectSkin[] mySkins;
}
