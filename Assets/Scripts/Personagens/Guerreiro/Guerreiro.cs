using Assets.Scripts.Visual.Animacoes.Personagens;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Personagens.Guerreiro
{
	public class Guerreiro : Jogador
	{
        private void Awake()
        {
            //força a adição dos componentes controlesGuerreiro e AnimGuerreiro no gameObject
            base.Awake();

			if (controle == null)
			{
				controle = gameObject.AddComponent<ComandosGuerreiro>();
			}
            if (animacao == null)
            {
                animacao = gameObject.AddComponent<AnimGuerreiro>();
            }
        }
        void Start()
		{
			nome = "Guerreiro";
			vida = 6;
			dano = 2;
			defesa = 0; // quando acionada aumenta para a quantidade do nivel atual
			velocidade = 5;// muda se estiver em modo de ataque 
		}

        // precisa chamar o funcionamento base.Update da classe jogador.
        // precisa existir para implementar funcinalidade especifica da classe.
        void Update()
		{
			base.Update();		

		}


		//métodos da classe
		public void Agachar(KeyCode comando)
		{
			//animação
		}
    }
}