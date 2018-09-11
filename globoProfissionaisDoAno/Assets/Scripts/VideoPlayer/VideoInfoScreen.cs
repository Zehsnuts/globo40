using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoInfoScreen : MonoBehaviour
{
    public RectTransform thumbNail;

    public Transform titleContentBox;
    public RectTransform titleRect;
    public RectTransform yearRect;
    public RectTransform direcaoRect;
    public RectTransform criacaoRect;

    public Transform infoContentBox;

    [HideInInspector]
    public string anunciante;
    [HideInInspector]
    public string criacao;
    [HideInInspector]
    public string direcao;
    [HideInInspector]
    public string agencia;
    [HideInInspector]
    public string produtora;
    [HideInInspector]
    public string produtoraSom;
    [HideInInspector]
    public string posProducao;
    [HideInInspector]
    public string vidTitle;
    [HideInInspector]
    public string year;
    [HideInInspector]
    public string categorie;

    public List<GameObject> infoBoxesList = new List<GameObject>();

    public List<GameObject> extraInfo = new List<GameObject>();

    public void RefreshTitleScreen(VidUnit vid, string hashText)
    {
        if (infoBoxesList.Count > 0)
            for (int i = infoBoxesList.Count - 1; i >= 0; i--)
                Destroy(infoBoxesList[i]);

        if (extraInfo.Count > 0)
            for (int i = extraInfo.Count - 1; i >= 0; i--)
                Destroy(extraInfo[i]);

        this.vidTitle = vid.TITULO;
        this.categorie = vid.CATEGORIA;
        this.year = vid.ANO;

        this.anunciante = vid.ANUNCIANTE;
        this.agencia = vid.AGENCIA;
        this.produtora = vid.PRODUTORA;
        this.produtoraSom = vid.PRODUTORA_DE_SOM;
        this.posProducao = vid.POS_PRODUCAO;

        //title.text = "   " +this.vidTitle;
        //yearCategorie.text = "  " + this.year + " - " + this.categorie;

        if (!string.IsNullOrEmpty(this.anunciante))
            CreateInfoPrefabs(this.anunciante, "ANUNCIANTE");

        if (!string.IsNullOrEmpty(this.agencia))
            CreateInfoPrefabs(this.agencia, "AGÊNCIA");

        if (!string.IsNullOrEmpty(this.produtora))
            CreateInfoPrefabs(this.produtora, "PRODUTORA");

        if (!string.IsNullOrEmpty(this.produtoraSom))
            CreateInfoPrefabs(this.produtoraSom, "PRODUTORA DE SOM");

        if (!string.IsNullOrEmpty(this.posProducao))
            CreateInfoPrefabs(this.posProducao, "PÓS-PRODUÇÃO");

        ChangeThumbNail(hashText);

        CreateExtraInfo();
    }

    void CreateExtraInfo()
    {
        List<VidUnit> lst = FindObjectOfType<VideoSelector>().ReturnDirecaoByTitle(vidTitle);

        for (int i = 0; i < lst.Count; i++)        
            ExtraInfoPrefab(lst[i].DIRECAO, direcaoRect);

        lst = FindObjectOfType<VideoSelector>().ReturnCriacaoByTitle(vidTitle);
        for (int i = 0; i < lst.Count; i++)
            ExtraInfoPrefab(lst[i].CRIACAO, criacaoRect);

        ExtraInfoPrefab(vidTitle, titleRect);
        ExtraInfoPrefab(year + " - " + categorie, yearRect);
    }

    GameObject ExtraInfoPrefab(string text, Transform parent)
    {
        GameObject b = Instantiate(Resources.Load("Prefabs/Name"), titleContentBox) as GameObject;
        b.GetComponentInChildren<Text>().text = text ;
        b.transform.SetSiblingIndex(parent.GetSiblingIndex()+1);
        extraInfo.Add(b);
        //Debug.Log(text);
        return b;
    }

    void CreateInfoPrefabs(string st, string title)
    {
        var b = Instantiate(Resources.Load("Prefabs/InfoBox"), infoContentBox) as GameObject;
        var c = b.GetComponent<InfoBox>();

        c.title.text = title;
        c.names.text = "  "+st;

        infoBoxesList.Add(b);
    }

    public void VideoInfoByHash(string[] hash)
    {
        FindObjectOfType<VideoSelector>().FindVideoByHash(hash);
    }

    public void ChangeThumbNail(string hash)
    {
        thumbNail.GetComponent<Image>().sprite = Resources.Load<Sprite>("Thumbnails/" + hash) as Sprite;
        ShowThumbnail();
    }

    public void ShowThumbnail()
    {
        thumbNail.gameObject.SetActive(true);
    }
    public void HideThumbnail()
    {
        thumbNail.gameObject.SetActive(false);
    }
}
