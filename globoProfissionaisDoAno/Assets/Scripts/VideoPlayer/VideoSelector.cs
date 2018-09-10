using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VideoSelector : MonoBehaviour
{
    public List<VidUnit> lista;
    public GameObject imageBlocker;

    private void Awake()
    {
        lista = JSONReader.VideosList();
    }

    public List<string> ReturnCategories()
    {
        List<string> ls = new List<string>();
        foreach (VidUnit item in lista)        
            if (!ls.Contains(item.CATEGORIA) &&  !string.IsNullOrEmpty(item.CATEGORIA))
                ls.Add(item.CATEGORIA);
        
        ls.Sort();
        ls.Reverse();
        return ls;
    }
    public List<VidUnit> ReturnCategoriesNR()
    {
        List<VidUnit> lst = lista.GroupBy(x => x.CATEGORIA).Select(y => y.First()).Where(y => !string.IsNullOrEmpty(y.CATEGORIA)).ToList();
        lst.Reverse();
        return lst;
    }

    public List<string> ReturnYears()
    {
        List<string> ls = new List<string>();
        foreach (VidUnit item in lista)
            if (!ls.Contains(item.ANO) && !string.IsNullOrEmpty(item.ANO))
                ls.Add(item.ANO);
        ls.Sort();
        ls.Reverse();
        return ls;
    }

    public List<VidUnit> ReturnYearsNR()
    {
        List<VidUnit> lst = lista.GroupBy(x => x.ANO).Select(y => y.First()).Where(y=>!string.IsNullOrEmpty(y.ANO)).ToList(); 
        lst.Reverse();
        return lst;
    }

    public List<VidUnit> ReturnByCategorie(string categorie)
    {
        List<VidUnit> ls = new List<VidUnit>();

        foreach (VidUnit item in lista)        
            if (item.CATEGORIA == categorie)            
                ls.Add(item);

        return (ls);
    }

    public List<VidUnit> ReturnByYear(string year)
    {
        List<VidUnit> ls = new List<VidUnit>();

        foreach (VidUnit item in lista)        
            if (item.ANO == year)            
                ls.Add(item); 
        
        return (ls);
    }

    public void FindVideoByHash(string[] hash)
    {
        List<VidUnit> ls = new List<VidUnit>();

        ls = ReturnByCategorie(hash[1]);

        foreach (VidUnit item in ls)        
            if (item.CATEGORIA == hash[1] && item.ANO == hash[2])
            {
                string hashText = item.ED + "_" + item.CATEGORIA + "_" + item.TITULO;

                FindObjectOfType<VideoInfoScreen>().RefreshTitleScreen(item, hashText);
                FindObjectOfType<StreamVideo>().ChangeVideo(hashText);
                break;
            }

        if (imageBlocker != null)
            Destroy(imageBlocker);
    }



}
