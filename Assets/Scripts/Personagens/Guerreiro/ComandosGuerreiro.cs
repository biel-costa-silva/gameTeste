using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using Assets.Scripts.Nucleo.Interfaces;

namespace Assets.Scripts.Personagens.Guerreiro
{
	public class ComandosGuerreiro : MonoBehaviour, IComandosGerais, IHabilidade
	{
        //controle do guerreiro
        public float ComandoMovimento()
        {            
            if (Input.GetKey(KeyCode.A)) return -1f;
            if (Input.GetKey(KeyCode.D)) return 1f;
            return 0;
        }
        public int ComandoAtaque()
        {
            if(Input.GetKeyDown(KeyCode.J)) return 12;
            return 0;
        }

        public bool ComandoInteracao()
        {
            if (Input.GetKeyDown(KeyCode.E)) return true;
            return false;
        }

        public void UsarHabilidade()
        {
            if (Input.GetKey(KeyCode.S));

        }
        public bool ComandoSaqueArma()
        {
            if (Input.GetKeyDown(KeyCode.Q)) return true;
            return false;
        }
    }
}