using Assets.Scripts.Nucleo.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Animacoes.VFXs
{
    public class VFXController : MonoBehaviour
    {
        public Personagem origem;
        public bool jaResolveu = false;

        public void Destruir()
        {
            Destroy(gameObject);
        }

    }
}