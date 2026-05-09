using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Nucleo.Interfaces
{
	public interface IComandosGerais
	{
		// Jogadores e inimigos
		public float ComandoMovimento();//para o método Andar
		public float ComandoAtaque();//para o método Atacar
		public bool ComandoInteracao();//para o método Interagir

		//Jogadores
		public bool ComandoSaqueArma();
		
		//Guerreiro

		//Arqueiro
	}
}