using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSelector : MonoBehaviour
{
    List<VidUnit> lista;
    public GameObject imageBlocker;

    private void Start()
    {
         lista = JSONReader.VideosList();
        //ReturnCategories();
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

    public List<VidUnit> ReturnByCategorie(string categorie)
    {
        List<VidUnit> ls = new List<VidUnit>();

        foreach (VidUnit item in lista)
        {
            if (item.CATEGORIA == categorie)            
                ls.Add(item);
                //Debug.Log(item.TITULO + " / " + item.ANO);
                       
        }
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

        //Debug.Log("Hash: " + hash[0] + " / " + hash[1] + " / " + hash[2]);

        switch (hash[0])
        {
            case "Categorie":
                ls = ReturnByCategorie(hash[1]);
                break;

            case "Edition":
                ls = ReturnByYear(hash[1]);
                break;
            default:
                break;
        }

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
