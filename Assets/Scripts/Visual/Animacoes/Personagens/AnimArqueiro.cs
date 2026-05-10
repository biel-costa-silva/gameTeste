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
    public class AnimArqueiro : ControladorAnim
    {
        // ----------------  Métodos de acionamento ------------------
        public override void AnimacaoParado()
        {
            animator.SetBool("isAndando", false);
        }
        //
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
        //
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
        


        // ----------------------- Método de controle de tempo de animação -------------------- #
        
        // ------------------------------------------------------------------------------------ #

        // EVENTOS ESPECIFICOS DO ARQUEIRO!! --------------------------------------------------
        

    }
}
