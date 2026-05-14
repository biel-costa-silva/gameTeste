using Assets.Scripts.Entidades.Enums;
using Assets.Scripts.Jogabilidade.Mundo;
using Assets.Scripts.Nucleo.Interfaces;
using Assets.Scripts.Visoes.Animacoes;
using Assets.Scripts.Visual.Animacoes.Personagens;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Personagens.Guerreiro
{
    public class Guerreiro : Jogador
    {
        private AnimGuerreiro animacaoGuerreiro;
        private ComandosGuerreiro controleGuerreiro;


        private void Awake()
        {
            base.Awake();
            animacaoGuerreiro = animacao as AnimGuerreiro;
            controleGuerreiro = controle as ComandosGuerreiro;
            //forca a buscar de forma especifica -> ComandosGuerreiro e AnimGuerreiro


        }
        void Start()
        {
            estadoAtual = base.estadoAtual;

            nome = "Guerreiro";
            vida = 6;
            dano = 2;
            defesa = 2; // quando acionada aumenta para a quantidade do nivel atual
            velocidade = 5;// muda se estiver em modo de ataque 
            velocidadeBase = velocidade;
        }

        // precisa chamar o funcionamento base.Update da classe jogador.
        // precisa existir para implementar funcinalidade especifica da classe.
        void Update()
        {
            base.Update();

            if (estadoAtual == EstadoJogador.ModoAtaque)
            {
                if (controleGuerreiro.Defender())
                {
                    estadoAtual = EstadoJogador.Ocupado;
                    StartCoroutine(RotinaDefendendo());
                    return;
                }
            }
            if (estadoAtual == EstadoJogador.AndandoArmado)
            {
                if (controleGuerreiro.Defender())
                {
                    estadoAtual = EstadoJogador.Ocupado;
                    StartCoroutine(RotinaDefendendo());
                    return;
                }
            }

        }

        IEnumerator RotinaDefendendo()
        {
            animacaoGuerreiro.AnimacaoDefendo();

            while (!animacaoGuerreiro.animacaoTerminou)
            {
                if (animacaoGuerreiro.estaRepelindo && sofreuAtaque)//janela para parry
                {
                    sofreuAtaque = false;
                    yield return StartCoroutine(RotinaRepelindo());
                    yield break;
                }

                else if (animacaoGuerreiro.estaDefendendo && sofreuAtaque)//janela para defesas
                {
                    sofreuAtaque = false;
                    yield return StartCoroutine(RotinaSofrendoAtqDefendendo());
                    yield break;
                }
                yield return null;

            }
            yield return StartCoroutine(animacaoGuerreiro.EsperarAnimacao());
            estadoAtual = EstadoJogador.ModoAtaque;
        }

        IEnumerator RotinaRepelindo()
        {
            animacaoGuerreiro.AnimacaoRepelindo();
            yield return StartCoroutine(animacaoGuerreiro.EsperarAnimacao());
            estadoAtual = EstadoJogador.ModoAtaque;
        }
        IEnumerator RotinaSofrendoAtqDefendendo()
        {
            animacaoGuerreiro.AnimacaoSofrendoAtqDef();
            yield return StartCoroutine(animacaoGuerreiro.EsperarAnimacao());
            estadoAtual = EstadoJogador.ModoAtaque;
        }



        //métodos da classe
        public void Agachar(KeyCode comando)
        {
            //animação
        }



        public override void SofrerAtaque(HitBox golpe)
        {
            if (animacaoGuerreiro.estaRepelindo)
            {
                Destroy(golpe.gameObject);
                //mudanças futuras
            }
            else if (animacaoGuerreiro.estaDefendendo)
            {
                golpe.dano -= defesa;
                base.SofrerAtaque(golpe);
            }
            else
            {
                base.SofrerAtaque(golpe);
            }
        }
    }
}