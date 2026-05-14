using Assets.Scripts.Visoes.Animacoes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Assets.Scripts.Visual.Animacoes.Personagens
{
    public class AnimGuerreiro : ControladorAnim
    {

        //variaveis de controle
        public bool estaDefendendo = false;
        public bool estaRepelindo = false;

        // ----------------  Métodos de acionamento ------------------
        public override void AnimacaoParado()
        {
            animator.SetBool("isAndando", false);
        }

        public override void AnimacaoAndando(bool parametro)
        {
            animator.SetBool("isAndando", parametro);
        }
        //
        public override void AnimacaoSacandoArma()
        {
            animacaoTerminou = false;
            animator.SetTrigger("sacarArma");
        }
        //
        public override void AnimacaoGuardandoArma()
        {
            animacaoTerminou = false;
            animator.SetTrigger("guardarArma");
        }
        //
        public override void AnimacaoParadoArmado()
        {
            animator.SetBool("isAndandoArm", false);
        }

        public override void AnimacaoAndandoArmado(bool parametro)
        {
            animator.SetBool("isAndandoArm", parametro);
        }
        //
        public override void AnimacaoInteragindo()
        {
            animacaoTerminou = false;
            animator.SetTrigger("interagir");
        }
        //
        public override void AnimacaoAtacando()
        {
            animacaoTerminou = false;
            animator.SetTrigger("atacar");
        }
        //
        public override void AnimacaoSofrendoAtqArm()
        {
            animacaoTerminou = false;
            animator.SetTrigger("sofrerAtqArm");
        }
        public override void AnimacaoSofrendoAtqDesarm()
        {
            animacaoTerminou = false;
            animator.SetTrigger("sofrerAtqDesarm");
        }        

        // ----------------------- Método de animações especificas do guerreiro -------------------- #

        public void AnimacaoDefendo()
        {
            animacaoTerminou = false;
            animator.SetTrigger("defender");
        }
        public void AnimacaoSofrendoAtqDef()
        {
            animacaoTerminou = false;
            animator.SetTrigger("sofrerAtqDefendendo");
        }
        public void AnimacaoRepelindo()
        {
            animacaoTerminou = false;
            animator.SetTrigger("repelir");
        }

        // ------------------------------------------------------------------------------------ #

        // EVENTOS ESPECIFICOS DO GUERREIRO!! --------------------------------------------------

        public void EventoRepelir()
        {
            estaRepelindo = true;
        }
        public void EventoDefesa()
        {
            estaRepelindo = false;
            estaDefendendo = true;
        }
        public void FimDefesa()
        {
            estaDefendendo = false;
        }

    }
}
