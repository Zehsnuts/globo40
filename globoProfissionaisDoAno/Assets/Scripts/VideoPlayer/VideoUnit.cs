using System.Collections;
using System.Collections.Generic;

    [System.Serializable]
    public class VidList
    {
        public List<VidUnit> Vids;
    }

    [System.Serializable]
    public class VidUnit
    {
        public string ED;
        public string ANO;
        public string CATEGORIA;
        public string TITULO;
        public string ANUNCIANTE;
        public string CRIACAO;
        public string DIRECAO;
        public string AGENCIA;
        public string PRODUTORA;
        public string PRODUTORA_DE_SOM;
        public string POS_PRODUCAO;

    public void VidInfo(VidUnit vdu)
    {
        this.ED = vdu.ED;
        this.ANO = vdu.ANO;
        this.CATEGORIA = vdu.CATEGORIA;
        this.TITULO = vdu.TITULO;
        this.ANUNCIANTE = vdu.ANUNCIANTE;
        this.CRIACAO = vdu.CRIACAO;
        this.DIRECAO = vdu.DIRECAO;
        this.AGENCIA = vdu.AGENCIA;
        this.PRODUTORA = vdu.PRODUTORA;
        this.PRODUTORA_DE_SOM = vdu.PRODUTORA_DE_SOM;
        this.POS_PRODUCAO = vdu.POS_PRODUCAO;
    }
    }

