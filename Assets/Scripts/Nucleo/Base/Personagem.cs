using Assets.Scripts.Animacoes.VFXs;
using Assets.Scripts.Entidades.Enums;
using Assets.Scripts.Jogabilidade.Mundo;
using Assets.Scripts.Nucleo.Interfaces;
using Assets.Scripts.Visoes.Animacoes;
using System.Collections;
using Unity;
using UnityEditor.SceneManagement;
using UnityEngine;

public abstract class Personagem : MonoBehaviour
{
    //variaveis componentes (do unity)------------
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;   
    public BoxCollider2D boxCollider;
    [SerializeField] private Transform pontoDoAtaque;

    //variaveis objetos/classes    
    [SerializeField] private GameObject[] ataques;
    [SerializeField] protected IComandosGerais controle;
    [SerializeField] protected ControladorAnim animacao;

    //variaveis de controle
    protected bool sofreuAtaque;
    protected float offsetXBase;

    //atributos da classe
    protected string nome { get; set; }
    protected int vida { get; set; }
    protected int dano { get; set; }
    protected int defesa { get; set; }
    protected float velocidade { get; set; }
    protected float velocidadeBase { get; set; }

    //Métodos de fluxo da unity
    public void Awake() // -------------------------------------------------------------------------------- 0
    {
        offsetXBase = -0.01f;

        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();

        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null) sprite = gameObject.AddComponent<SpriteRenderer>();

        animacao = GetComponent<ControladorAnim>(); // procura componente que herda ControleAnim     
        controle = GetComponent<IComandosGerais>(); // procura componente que implementa IComandosGerais      
        
    }

    //Métodos de correcao/sincronizacao - [Sprite - BoxCollider]

    //Flipar o BoxCollider junto com o Sprite por frame --> chamar no método update
    public void CorrigirColliderFlip()
    {
        if (sprite.flipX) boxCollider.offset = new Vector2(-offsetXBase, boxCollider.offset.y);
        else boxCollider.offset = new Vector2(offsetXBase, boxCollider.offset.y);
    }

    //Mudar BoxCollider em animacoes especificas - chamar poo AnimationEvent
    public void TrocarOffsetX(float novoValor)
    {
        offsetXBase = novoValor;
    }

    //Métodos da classe - Personagem
    public virtual void Locomover(float direcao)
    {
        rb.linearVelocity = new Vector2(direcao * velocidade, rb.linearVelocity.y);
        if (direcao < 0) sprite.flipX = true;
        else sprite.flipX = false;
    }

    //--------------------------------------------------------------------------------
    public virtual void Atacar(int forca)
    {
        float direcao = sprite.flipX ? -1f : 1f;
        rb.AddForce(new Vector2(direcao * (forca + 8) * 2, 0), ForceMode2D.Impulse);//impulso do ataque  
    }

    public void AplicarGolpe(int indice)
    {
        GameObject atk = Instantiate(ataques[indice], pontoDoAtaque.position, pontoDoAtaque.rotation);

        float direcao = sprite.flipX ? -1f : 1f;       

        Vector3 scale = atk.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direcao;
        atk.transform.localScale = scale;

        BoxCollider2D col = atk.GetComponent<BoxCollider2D>();

        if (col != null) col.offset = new Vector2(Mathf.Abs(col.offset.x) * direcao, col.offset.y);

        VFXController vfx = atk.GetComponent<VFXController>();
        vfx.origem = this;

        HitBox hitbox = atk.GetComponent<HitBox>();
        hitbox.dano = this.dano;
        hitbox.direcao = direcao;
        hitbox.Inicializar(this);

        Debug.Log("Aplicou: " + hitbox.dano + " de dano");
    }
    //-------------------------------------------------------------------------------    


    public virtual void SofrerAtaque(HitBox golpe) //Se "is Trigger" (componente Collider) acontece, retorne true e faça;
    {
        sofreuAtaque = true;

        rb.AddForce(new Vector2(golpe.direcao * golpe.dano * 12, 0), ForceMode2D.Impulse);
        if (golpe.direcao > 0) sprite.flipX = true;
        else sprite.flipX = false;

        vida -= golpe.dano;
        Debug.Log("Sofreu: " + golpe.dano + " vida atual: " + vida);

    }


    public virtual bool Morrer(bool morreu) //Se vida for = 0, retone true e faça;
    {
        return false; //envia sinal true se for chamado
    }




    private void LateUpdate()// -------------------------------------------------------------------------------- 3
    {
        CorrigirColliderFlip();
    }


}