using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockableHolder : MonoBehaviour {

    public GameObject unlocPref;

    public enum UnlockableName
    {
        none,
        dragonClaw,
        alivePlantHeart,
        waterPlant,
        windPlant,
        firePlant,
        earthPlant,
        niebieskiKluczyk,
        mindRead,
        curseShare,
        gigant,
        crime,
        prisoner,
        miksturaLeczenia,
        miksturaZłagodzeniaKlątwy,
        miksturaWIedzy
    }

    [System.Serializable]
    public struct Unlockable
    {
        public UnlockableName name;
        public Sprite sprite;
        public Image body;

        public Unlockable(Image body, Unlockable unloc)
        {
            this.body = body;
            sprite = unloc.sprite;
            name = unloc.name;
        }
    }

    public Sprite GetUnlocaBleSprite(UnlockableName name)
    {
        return unlocables[GetUnlockableIndex(name)].sprite;
    }

    public List<Unlockable> unlocables;

    int GetUnlockableIndex(UnlockableName unlocName)
    {
        for (int i = 0; i < unlocables.Count; i++)
        {
            Unlockable unlock = unlocables[i];
            if (unlock.name == unlocName)
                return i;
        }
        return 0;
    }

    public bool DoesItContain(UnlockableName unlockable)
    {
        foreach (Unlockable unlock in unlocables)
            if (unlock.name == unlockable && unlock.body)
            {
                print(unlock.ToString());
                return true;
            }
        return false;
    }

    public void Add(UnlockableName unlocName)
    {
        int index = GetUnlockableIndex(unlocName);
        if (unlocables[index].body)
            return;

        GameObject imgBody = Instantiate(unlocPref, transform.position, Quaternion.identity);
        imgBody.GetComponent<Image>().sprite = unlocables[index].sprite;
        imgBody.transform.parent = transform;
        imgBody.transform.localScale = new Vector3(1, 1, 1);
        unlocables[index] = new Unlockable(imgBody.GetComponent<Image>(), unlocables[index]);
    }

    public void Remove(UnlockableName unlocName)
    {
        int index = GetUnlockableIndex(unlocName);
        Destroy(unlocables[index].body.gameObject);
        unlocables[index] = new Unlockable(null, unlocables[index]);
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
