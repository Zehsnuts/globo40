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
        List<VidUnit> lst = lista.Where(x => x.CATEGORIA == categorie).Select(x => x).ToList();

        lst = lst.GroupBy(x => x.ANO).Select(y => y.First()).ToList();

        //foreach (VidUnit item in lista)        
            //if (item.CATEGORIA == categorie)            
                //ls.Add(item);

        return (lst);
    }

    public List<VidUnit> ReturnDirecaoByTitle(string title)
    {
        //List<VidUnit> lst = lista.GroupBy(x => x.DIRECAO).Select(y => y.First()).Where(y => y.TITULO == title).ToList();

        List<VidUnit> lst = lista.Where(x => x.TITULO == title).Select(x => x).ToList();

        lst = lst.GroupBy(x => x.DIRECAO).Select(y => y.First()).ToList();

        return lst;
    }

    public List<VidUnit> ReturnCriacaoByTitle(string title)
    {
        //List<VidUnit> lst = lista.GroupBy(x => x.CRIACAO).Select(y => y.First()).Where(y => y.TITULO == title).ToList();

        List<VidUnit> lst = lista.Where(x => x.TITULO == title).Select(x => x).ToList();

        lst = lst.GroupBy(x => x.CRIACAO).Select(y => y.First()).ToList();

        return lst;
    }

    public List<VidUnit> ReturnAgenciaByTitle(string title)
    {
        //List<VidUnit> lst = lista.GroupBy(x => x.CRIACAO).Select(y => y.First()).Where(y => y.TITULO == title).ToList();

        List<VidUnit> lst = lista.Where(x => x.TITULO == title).Select(x => x).ToList();

        lst = lst.GroupBy(x => x.AGENCIA).Select(y => y.First()).ToList();

        return lst;
    }

    public List<VidUnit> ReturnByTitle(string title)
    {
        List<VidUnit> lst = lista.Where(x => x.CATEGORIA == title).Select(x => x).ToList();

        lst = lst.GroupBy(x => x.ANO).Select(y => y.First()).ToList();

        return (lst);
    }
    public List<VidUnit> ReturnByYear(string year)
    {
        List<VidUnit> lst = lista.Where(x => x.ANO == year).Select(x=>x).ToList();

        lst = lst.GroupBy(x => x.CATEGORIA).Select(y => y.First()).ToList();
     
        return (lst);
    }

    public void FindVideoByHash(string[] hash)
    {
        Debug.Log(hash[0] + "_" + hash[1] + "_" + hash[2]);

        List<VidUnit> ls = new List<VidUnit>();

        ls = ReturnByCategorie(hash[1]);

        foreach (VidUnit item in ls)
        {
            Debug.Log(item.TITULO);
            if (item.CATEGORIA == hash[1] && item.ANO == hash[2])
            {
                string hashText = item.ED + "_" + item.CATEGORIA + "_" + item.TITULO;

                FindObjectOfType<VideoInfoScreen>().RefreshTitleScreen(item, hashText);
                FindObjectOfType<StreamVideo>().ChangeVideo(hashText);
            }
            else
            {   if(!FindObjectOfType<StreamVideo>().nextVideo.Contains(hash[0] + "_" + item.CATEGORIA + "_" + item.ANO))
                    FindObjectOfType<StreamVideo>().nextVideo.Add(hash[0] + "_" + item.CATEGORIA + "_" + item.ANO);
            }
        }

        if (imageBlocker != null)
            Destroy(imageBlocker);
    }



}
