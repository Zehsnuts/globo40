using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpandableMenu : MonoBehaviour
{
    /*Menu add text according to what was clicked, but instead of making retractable boxes the text is added one after the other and the text aligment is used as the "boxes" */

    public Transform childContentBox;

    private List<GameObject> list2nd = new List<GameObject>();
    private List<GameObject> list3rd = new List<GameObject>();

    private string[] hashString = new string[] {"1","2","3"};

    private static string YEARMENUHEADER = "          ED. - ANO";

    MenuElement lastMenuElement;

    //Pressed button
    public void InterpretButton(MenuElement mEle)
    {
        SwitchWithName(mEle);
    }

    //Text that has been clicked is passed here. hashString works to find videos/thumbnails but also as history of clicked buttons.
    private void SwitchWithName(MenuElement tr)
    {
        if (lastMenuElement != null && lastMenuElement != tr)
            lastMenuElement.isOpen = false;

        string[] str = tr.name.Split("_"[0]);

        switch (str[0])
        {
            case "1st":                
                hashString[0] = "";
                ClearList(list2nd);
                ClearList(list3rd);                
                Fill2nd(tr);
                hashString[0]= str[1];
                break;

            case "2nd":
                hashString[1] = "";
                ClearList(list3rd);
                Fill3rd(tr);
                hashString[1] = str[1];
                break;

            case "3rd":
                hashString[2] = "";
                Debug.Log("str[1] = " + str[1] + " / str[2] = " + str[2]);
                hashString[2] = str[2];
                FindObjectOfType<VideoInfoScreen>().VideoInfoByHash(hashString);
                break;

            default:
                break;
        }

        lastMenuElement = tr;
    }

    private void Fill2nd(MenuElement mEle)
    {
        if (mEle.isOpen)
        {
            mEle.isOpen = false;
            return;
        }

        int nextIndex = mEle.transform.GetSiblingIndex() + 1;

        string[] nm = new string[] { }; //{ "   > INSTITUCIONAL", "   > REGIONAL"};

        if (mEle.name.Contains("Categorie"))
            nm = FindObjectOfType<VideoSelector>().ReturnCategories().ToArray();

        else if(mEle.name.Contains("Edition"))
            nm = FindObjectOfType<VideoSelector>().ReturnYears().ToArray();

        for (int i = 0; i < nm.Length; i++)
            {
            CreatePreFab("Prefabs/2nd_text", "     > " + nm[i], ref n, list2nd, true, "2nd_" + nm[i]);
            /*
                var b = Instantiate(Resources.Load("Prefabs/2nd_text"), childContentBox) as GameObject;
                b.GetComponent<Text>().text = "     > " + nm[i];
                b.transform.SetSiblingIndex(nextIndex);
                b.name = "2nd_" + nm[i];
                list2nd.Add(b);
                */
            }
        mEle.isOpen = true;
    }

    private void Fill3rd(MenuElement mEle)
    {
        if (mEle.isOpen)
        {
            mEle.isOpen = false;
            return;
        }

        int nextIndex = mEle.transform.GetSiblingIndex() + 1;

        if (mEle.name.Contains("2nd"))
        { 
            FillYearsByCategorie(mEle.name.Split("_"[0])[1], mEle);
            mEle.isOpen = true;
            return;
        }

        for (int i = 1; i < 40; i++)
            {
                var b = Instantiate(Resources.Load("Prefabs/3rd_text"), childContentBox) as GameObject;
                b.name = b.GetComponent<Text>().text = (1978 + i).ToString();
                b.GetComponent<Text>().text = "         " + i.ToString("00") +" - " + b.GetComponent<Text>().text;
                b.transform.SetSiblingIndex(nextIndex);
                nextIndex++;
                list3rd.Add(b);
            }

        mEle.isOpen = true;
    }

    private void FillYearsByCategorie(string year, MenuElement mEle)
    {
        List<VidUnit> ls = FindObjectOfType<VideoSelector>().ReturnByCategorie(year);

        int nextIndex = mEle.transform.GetSiblingIndex() + 1;

        CreatePreFab("Prefabs/3rd_text", YEARMENUHEADER, ref nextIndex, list3rd, false); 

        for (int i = 0; i < ls.Count ; i++)        
            CreatePreFab("Prefabs/3rd_text", "          " + (ls[i].ED) + "ª - " + ls[i].ANO, ref nextIndex, list3rd, true, ("_" + ls[i].ANO));        

        CreatePreFab("Prefabs/3rd_text", "", ref nextIndex, list3rd, false);
    }

    void CreatePreFab(string path, string text, ref int nextIndex, List<GameObject> list, bool interactable = true, string name = "")
    {
        var c = Instantiate(Resources.Load(path), childContentBox) as GameObject;
        c.name = c.name + name;
        nextIndex++;
        c.GetComponent<Text>().text = text;
        c.GetComponent<Button>().interactable = interactable;
        c.transform.SetSiblingIndex(nextIndex);
        list.Add(c);
    }

    private void ClearList(List<GameObject> gOL)
    {
        for (int i = gOL.Count; i > 0; i--)
        {
            var c = gOL[i - 1];
            gOL.Remove(c);
            Destroy(c);
        }
    }
}
