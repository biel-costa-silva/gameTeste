using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Visoes.Animacoes
{
    abstract public class ControladorAnim : MonoBehaviour
    {
        [SerializeField] private Personagem personagem;
        protected Animator animator;

        //variaveis de controle
        public bool animacaoTerminou = false;
        public bool novoAtaque = true;
        public int indiceAtaque { get; set; }
        public bool ComboRegistrado { get; set; }
        void Awake()
        {
            animator = GetComponent<Animator>();
        }
        // Jogadores e inimigos
        public abstract void AnimacaoAndando(bool parametro);
        public abstract void AnimacaoParado();
        public abstract void AnimacaoAtacando();

        public IEnumerator EsperarAnimacao()
        {
            while (!animacaoTerminou) yield return null;            
        }
        
        // Jogadores

        public abstract void AnimacaoParadoArmado();
        public abstract void AnimacaoAndandoArmado(bool parametro);

        public abstract void AnimacaoInteragindo(); // só deve funcionar em campos de interações
       
        public abstract void AnimacaoSacandoArma();
        public abstract void AnimacaoGuardandoArma();


        public abstract void AnimacaoSofrendoAtqArm();
        public abstract void AnimacaoSofrendoAtqDesarm();

        //EVENTOS NO ANIMATION EVENTS
        
        // Animation Event no último frame
        public void OnAnimacaoTerminou() 
        {
            animacaoTerminou = true;
        }

        // Resetar antes de cada animação
        public void ResetarAnimacao()
        {
            animacaoTerminou = false;
            novoAtaque = true;
        }

        //COMBOS!
        public void JanelaNovoAtq()
        {
            novoAtaque = false;         
        }

        //Define ataque e spawna na cena
        public void EventoAtaque()//frame do ataque
        {
            personagem.AplicarGolpe(indiceAtaque);
        }

    }
}