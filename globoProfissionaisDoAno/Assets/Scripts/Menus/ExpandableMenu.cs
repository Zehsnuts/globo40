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

    //MenuElement lastMenuElement;
    MenuElement last1stMenuElement;
    MenuElement last2ndMenuElement;
    MenuElement last3rdMenuElement;

    //Pressed button
    public void InterpretButton(MenuElement mEle)
    {
        SwitchWithName(mEle);
    }

    //Text that has been clicked is passed here. hashString works to find videos/thumbnails but also as history of clicked buttons.
    private void SwitchWithName(MenuElement tr)
    {
        string[] str = tr.name.Split("_"[0]);

        switch (str[0])
        {
            case "1st":
                if (last1stMenuElement == null)
                    last1stMenuElement = tr;
                else if (last1stMenuElement != tr)
                {
                    last1stMenuElement.CloseButton();
                    last1stMenuElement = tr;
                }
                hashString[0] = "";
                hashString[1] = "";
                hashString[2] = "";
                ClearList(list2nd);
                ClearList(list3rd);
                if (tr.isOpen)
                {
                    tr.CloseButton();
                    return;
                }
                Fill2nd(tr);
                hashString[0]= str[1];
                break;

            case "2nd":
                if (last2ndMenuElement == null)
                    last2ndMenuElement = tr;
                else if (last2ndMenuElement != tr)
                {
                    last2ndMenuElement.CloseButton();
                    last2ndMenuElement = tr;
                }

                ClearList(list3rd);
                if (tr.isOpen)
                {
                    tr.CloseButton();
                    return;
                }
                Fill3rd(tr);
                if (hashString[0] == "Categorie")                
                    hashString[1] = str[1];                
                else
                    hashString[2] = str[1];

                break;

            case "3rd":
                if (hashString[0] == "Categorie")
                    hashString[2] = str[1];                
                else                
                    hashString[1] = str[1];
                FindObjectOfType<StreamVideo>().nextVideo.Clear();
                FindObjectOfType<VideoSelector>().FindVideoByHash(hashString);
                break;

            default:
                break;
        }

        tr.OpenButton();
    }

    private void Fill2nd(MenuElement mEle)
    {

        int nextIndex = mEle.transform.GetSiblingIndex() + 1;

        List<VidUnit> ls = new List<VidUnit>();

        if (mEle.name.Contains("Categorie"))
        {
            ls = FindObjectOfType<VideoSelector>().ReturnCategoriesNR();

            for (int i = 0; i < ls.Count; i++)
                CreatePreFab("Prefabs/2nd_", ""+ ls[i].CATEGORIA, nextIndex, list2nd, true, "2nd_" + ls[i].CATEGORIA);
        }
        else if (mEle.name.Contains("Edition"))
        {
            CreatePreFab("Prefabs/(OLD)2nd_", YEARMENUHEADER, ref nextIndex, list2nd, true);
            ls = FindObjectOfType<VideoSelector>().ReturnYearsNR();
            for (int i = 0; i < ls.Count; i++)
                CreatePreFab("Prefabs/2nd_", "" + (ls[i].ED) + "ª - " + ls[i].ANO, nextIndex, list2nd, true, "2nd_" + ls[i].ANO);
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

        //int nextIndex = mEle.transform.GetSiblingIndex();

        if (mEle.name.Contains("20") || mEle.name.Contains("19"))
        {
            FillCategorieByYears(mEle.name.Split("_"[0])[1], mEle);
            mEle.isOpen = true;
            return;
        }
        else
        {
            FillYearsByCategorie(mEle.name.Split("_"[0])[1], mEle);
            mEle.isOpen = true;
            return;
        }
    }

    private void FillYears(string year, MenuElement mEle)
    {
        List<VidUnit> ls = FindObjectOfType<VideoSelector>().ReturnByCategorie(year);

        int nextIndex = mEle.transform.GetSiblingIndex() + 1;

        CreatePreFab("Prefabs/(OLD)2nd_", YEARMENUHEADER, ref nextIndex, list2nd, false);

        for (int i = 0; i < ls.Count; i++)
            CreatePreFab("Prefabs/2rd_", "      " + (ls[i].ED) + "ª - " + ls[i].ANO, ref nextIndex, list2nd, true, ("2nd_" + ls[i].ANO));

        CreatePreFab("Prefabs/(OLD)2nd_", "", ref nextIndex, list2nd, false);
    }

    private void FillYearsByCategorie(string categorie, MenuElement mEle)
    {
        List<VidUnit> ls = FindObjectOfType<VideoSelector>().ReturnByCategorie(categorie);

        int nextIndex = mEle.transform.GetSiblingIndex() + 1;

        CreatePreFab("Prefabs/3rd_", YEARMENUHEADER, ref nextIndex, list3rd, false); 

        for (int i = 0; i < ls.Count ; i++)        
            CreatePreFab("Prefabs/3rd_", "          " + (ls[i].ED) + "ª - " + ls[i].ANO, ref nextIndex, list3rd, true, ("3rd_"+ls[i].ANO));        

        CreatePreFab("Prefabs/3rd_", "", ref nextIndex, list3rd, false);
    }

    private void FillCategorieByYears(string year, MenuElement mEle)
    {
        List<VidUnit> ls = FindObjectOfType<VideoSelector>().ReturnByYear(year);

        int nextIndex = mEle.transform.GetSiblingIndex() + 1;       

        for (int i = 0; i < ls.Count; i++)
            CreatePreFab("Prefabs/3rd_", "          " + (ls[i].CATEGORIA), ref nextIndex, list3rd, true, ("3rd_" + ls[i].CATEGORIA));

        CreatePreFab("Prefabs/3rd_", "", ref nextIndex, list3rd, false);
    }

    void CreatePreFab(string path, string text, int nextIndex, List<GameObject> list, bool interactable = true, string name = "")
    {
        var c = Instantiate(Resources.Load(path), childContentBox) as GameObject;
        c.name = name;
        c.GetComponentInChildren<Text>().text = text;
        c.GetComponent<Button>().interactable = interactable;
        c.transform.SetSiblingIndex(nextIndex);
        list.Add(c);
    }

    void CreatePreFab(string path, string text, ref int nextIndex, List<GameObject> list, bool interactable = true, string name = "")
    {
        var c = Instantiate(Resources.Load(path), childContentBox) as GameObject;
        c.name = name;
        c.GetComponentInChildren<Text>().text = text;
        c.GetComponent<Button>().interactable = interactable;
        c.transform.SetSiblingIndex(nextIndex);
        nextIndex++;
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
