using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Visoes.Animacoes
{
    abstract public class ControladorAnim : MonoBehaviour
    {
        protected Animator animator;
        public bool animacaoTerminou = false;
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
        public void OnAnimacaoTerminou() // Animation Event no último frame
        {
            animacaoTerminou = true;
        }
        // importante -- resetar antes de cada animação
        public void ResetarAnimacao()
        {
            animacaoTerminou = false;
        }

    }
}