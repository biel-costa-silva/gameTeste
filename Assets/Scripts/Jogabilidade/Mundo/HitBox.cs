using Assets.Scripts.Animacoes.VFXs;
using Assets.Scripts.Nucleo.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Jogabilidade.Mundo
{
    public class HitBox : MonoBehaviour, ICausamDano
    {
        public Personagem origem;
        private BoxCollider2D boxColl;

        //variaveis de controle       
        bool jaResolveu;
        bool podeDarDano = false;
        bool detectouAtaque = false;
        Personagem alvoGuardado;

        //variaveis de jogabilidade
        public int dano { get; set; }
        public float direcao { get; set; }


        private void Awake()
        {
            boxColl = GetComponent<BoxCollider2D>();
            boxColl.enabled = false;
        }

        public void Inicializar(Personagem dono)
        {
            origem = dono;

            Collider2D meu = GetComponent<Collider2D>();

            foreach (Collider2D col in origem.GetComponentsInChildren<Collider2D>())
            {
                Physics2D.IgnoreCollision(meu, col);
            }
        }


        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!boxColl.enabled) return;

            VFXController vfx = other.GetComponentInParent<VFXController>();
            Personagem alvo = other.GetComponentInParent<Personagem>();

            if (vfx != null && vfx.origem != origem)
            {
                detectouAtaque = true;
            }

            if (alvo != null && alvo != origem)
            {
                podeDarDano = true;
                alvoGuardado = alvo;
            }
        }
        void LateUpdate()
        {
            if (jaResolveu) return;

            if (detectouAtaque)
            {
                jaResolveu = true;                
                ColisaoEntreAtaques();
                Resetar();
                return;
            }

            if (podeDarDano && alvoGuardado != null)
            {
                jaResolveu = true;
                alvoGuardado.SofrerAtaque(this);// !!!
                Resetar();
            }
        }

        void Resetar()
        {
            detectouAtaque = false;
            podeDarDano = false;
            alvoGuardado = null;
        }

        //Evento de Controle para Frame De Dano
        public void AtivarHitbox()
        {
            boxColl.enabled = true;
        }
        public void DesativarHitBox()
        {
            boxColl.enabled = false;
        }


        private void ColisaoEntreAtaques()
        {
            Debug.Log("Choque Entre Ataques");

            Destroy(gameObject);
            //mudar para choque entre ataques 
        }
    }
}