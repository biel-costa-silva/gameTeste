using Assets.Scripts.Nucleo.Interfaces;
using Assets.Scripts.Visoes.Animacoes;
using Assets.Scripts.Visual.Animacoes.Personagens;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Personagens.Guerreiro
{
	public class Guerreiro : Jogador
	{
        private void Awake()
        {
            //ControladorAnim e IComandosGerais
			if(GetComponent<AnimGuerreiro>() == null) gameObject.AddComponent<AnimGuerreiro>();
            if (GetComponent<ComandosGuerreiro>() == null) gameObject.AddComponent<ComandosGuerreiro>();          
            base.Awake();

			//forca a buscar de forma especifica -> ComandosGuerreiro e AnimGuerreiro

			
            
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