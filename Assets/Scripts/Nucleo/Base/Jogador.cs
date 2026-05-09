using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using Assets.Scripts.Visoes.Animacoes;
using System.Collections;
using Assets.Scripts.Entidades.Enums;
using UnityEngine.UIElements;
using Assets.Scripts.Nucleo.Interfaces;

public class Jogador : Personagem
{

    //variaveis internas Unity    
    public Camera cameraController;

    protected EstadoJogador estadoAtual = EstadoJogador.Parado;
    private IInteracoes interagivelAtual;


    //Controle de ESTADOS
    public void Update()// -------------------------------------------------------------------------------------------------------------- 1
    {
        
        if (estadoAtual == EstadoJogador.Ocupado) return; //Garante consistência, não roda o Update completo enquanto alguma coisa não ecerrar


        // ------------------------------ POSSIBILIDADES ENQUANTO PARADO ------------------------------- //

        if (estadoAtual == EstadoJogador.Parado)
        {
            animacao.AnimacaoParado();// looping fixo, sem inputs 

            if (sofreuDano)
            {
                estadoAtual = EstadoJogador.Ocupado;
                StartCoroutine(RotinaSofrendoAtaqueDesarm());
                sofreuDano = false;
                return;
            }

            if (controle.ComandoMovimento() != 0)
            {
                estadoAtual = EstadoJogador.Andando;
                return;//necessita para mudança de estados em looping
            }
            //
            if (controle.ComandoSaqueArma())
            {
                estadoAtual = EstadoJogador.Ocupado;

                StartCoroutine(RotinaSacanadoArma());
                return;
            }
            //
            if (controle.ComandoInteracao())
            {
                if (interagivelAtual != null)
                {
                    estadoAtual = EstadoJogador.Ocupado;

                    StartCoroutine(RotinaInteracao());
                    interagivelAtual.SofrerInteracao(this);
                    return;
                }// outros if para cada obejto interagivel                
            }
            /*
            if sofrer ataque, hitbox, troque para essa ainmação
            */
        }
        // --------------------------------------------------------------------------------------------- //

        // ------------------------------ POSSIBILIDADES ENQUANTO ANDANDO ------------------------------ //
        if (estadoAtual == EstadoJogador.Andando)
        {
            if (sofreuDano)
            {
                estadoAtual = EstadoJogador.Ocupado;
                StartCoroutine(RotinaSofrendoAtaqueDesarm());
                sofreuDano = false;
                return;
            }

            if (controle.ComandoMovimento() != 0)
            {
                Andar(controle.ComandoMovimento());
                animacao.AnimacaoAndando(estadoAtual == EstadoJogador.Andando);//looping dinâmico, necessário mesclar animator com Estado real do jogador     
            }
            else
            {
                estadoAtual = EstadoJogador.Parado;
            }

            if (controle.ComandoSaqueArma())
            {
                estadoAtual = EstadoJogador.Ocupado;
                StartCoroutine(RotinaSacanadoArma());
                return;
            }

        }
        // --------------------------------------------------------------------------------------------- //

        // ------------------------ POSSIBILIDADES ENQUANTO EM MODO DE ATAQUE -------------------------- //
        if (estadoAtual == EstadoJogador.ModoAtaque)
        {
            animacao.AnimacaoParadoArmado();

            if (sofreuDano)
            {
                estadoAtual = EstadoJogador.Ocupado;
                StartCoroutine(RotinaSofrendoAtaqueArm());
                sofreuDano = false;
                return;
            }

            if (controle.ComandoMovimento() != 0)
            {
                estadoAtual = EstadoJogador.AndandoArmado;
                return;
            }

            if (controle.ComandoSaqueArma())
            {
                estadoAtual = EstadoJogador.Ocupado;
                StartCoroutine(RotinaGuardandoArma());
                return;
            }

            if (controle.ComandoAtaque() != 0)
            {
                estadoAtual = EstadoJogador.Ocupado;
                StartCoroutine(RotinaAtacando());
                return;
            }

        }
        // --------------------------------------------------------------------------------------------- //

        // ----------------------- POSSIBILIDADES ENQUANTO ANDA EM MODO DE ATAQUE ---------------------- //
        if (estadoAtual == EstadoJogador.AndandoArmado)
        {
            if (sofreuDano)
            {
                estadoAtual = EstadoJogador.Ocupado;
                StartCoroutine(RotinaSofrendoAtaqueArm());
                sofreuDano = false;
                return;
            }

            if (controle.ComandoMovimento() != 0)
            {
                Andar(controle.ComandoMovimento());
                animacao.AnimacaoAndandoArmado(estadoAtual == EstadoJogador.AndandoArmado);
            }
            else
            {
                estadoAtual = EstadoJogador.ModoAtaque;
            }

            if (controle.ComandoSaqueArma())
            {
                estadoAtual = EstadoJogador.Ocupado;
                StartCoroutine(RotinaGuardandoArma());
                return;
            }

            if (controle.ComandoAtaque() != 0)
            {
                estadoAtual = EstadoJogador.Ocupado;
                StartCoroutine(RotinaAtacando());
                return;
            }

        }
        // --------------------------------------------------------------------------------------------- //


    }



    // #------------------------------ Rotinas - Disparo de Animações Diretas sem Interrupção -----------------------------#

    IEnumerator RotinaSacanadoArma()
    {
        animacao.AnimacaoSacandoArma();
                
        yield return StartCoroutine(animacao.EsperarAnimacao());

        estadoAtual = EstadoJogador.ModoAtaque;
    }
    //
    IEnumerator RotinaGuardandoArma()
    {
        animacao.AnimacaoGuardandoArma();
        yield return StartCoroutine(animacao.EsperarAnimacao());

        estadoAtual = EstadoJogador.Parado;
    }
    //
    IEnumerator RotinaInteracao()
    {
        Interagir();

        yield return StartCoroutine(animacao.EsperarAnimacao());

        estadoAtual = EstadoJogador.Parado;
    }
    //
    IEnumerator RotinaAtacando()
    {
        animacao.ResetarAnimacao();
        Atacar(controle.ComandoAtaque());
        animacao.AnimacaoAtacando();

        //pode sofrer dano durante o ataque (quem acerta outro antes)
        while (!animacao.animacaoTerminou) 
        {
            if (sofreuDano)
            {
                sofreuDano = false;
                estadoAtual = EstadoJogador.Ocupado;
                StartCoroutine(RotinaSofrendoAtaqueArm());
                yield break;
            }
            yield return null;
        }

        estadoAtual = EstadoJogador.ModoAtaque;
    }

    //------------------- Sofrendo Ataques: possibilidades --------------------
    IEnumerator RotinaSofrendoAtaqueDesarm()
    {
        animacao.AnimacaoSofrendoAtqDesarm();
        yield return StartCoroutine(animacao.EsperarAnimacao());
        estadoAtual = EstadoJogador.Parado;
    }
    IEnumerator RotinaSofrendoAtaqueArm()
    {
        animacao.AnimacaoSofrendoAtqArm();
        yield return StartCoroutine(animacao.EsperarAnimacao());
        estadoAtual = EstadoJogador.ModoAtaque;
    }    
    //-------------------------------------------------------------------------




    // #-------------------------------------------------------------------------------------------------------------------#


    public void OnTriggerEnter2D(Collider2D other) //aciona se colidiu com algo
    {
        IInteracoes interagivel = other.GetComponent<IInteracoes>();
        if (interagivel != null)
        {
            interagivelAtual = interagivel;
            Debug.Log("possivel interagir com algo");
        }
    }

    public void OnTriggerExit2D(Collider2D other)//aciona quando saiu da colisao com algo
    {
        IInteracoes interagivel = other.GetComponent<IInteracoes>();
        if (interagivel != null)
        {
            interagivelAtual = null;

            Debug.Log("Fora da area de interacao");
        }
    }
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 




    //métodos da classe - Jogador
    public virtual bool Interagir() // Se comando for acionado, personagem fica invulneravel e imóvel.
    {
        animacao.AnimacaoInteragindo();
        return true;
    }
    public virtual void Reviver(bool podeReviver) // Se Morrer(método herdado) acontece recebe true e faça
    {

    }
    public virtual void SubirNivel(bool podeSubir)// Se troca acontece, retorne true e faça
    {

    }
}
