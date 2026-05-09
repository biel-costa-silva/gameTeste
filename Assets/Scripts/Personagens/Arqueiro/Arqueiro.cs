using Assets.Scripts.Personagens.Guerreiro;
using Assets.Scripts.Visual.Animacoes.Personagens;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Personagens.Arqueiro
{
	public class Arqueiro : Jogador
	{
        private void Awake()
        {
            //força a adição dos componentes comandosArqueiro e AnimArqueiro no gameObject
            base.Awake();

            if (controle == null)
            {
                controle = gameObject.AddComponent<ComandosArqueiro>();
            }
            if (animacao == null)
            {
                animacao = gameObject.AddComponent<AnimArqueiro>();
            }
        }
        void Start()
		{
            nome = "Arqueiro";
            vida = 4;
            dano = 3;
            defesa = 0; // quando acionada aumenta para a quantidade do nivel atual
            velocidade = 5; // muda se estiver em modo de ataque 
        }


		// precisa chamar o funcionamento base.Update da classe jogador.
		// precisa existir para implementar funcinalidade especifica da classe.
		void Update()
		{
			base.Update();
		}
	}
}