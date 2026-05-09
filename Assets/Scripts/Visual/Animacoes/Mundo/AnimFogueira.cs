using UnityEngine;
using System.Collections;
using Assets.Scripts.Entidades;

namespace Assets.Scripts.Visual.Animacoes.Mundo
{
    public class AnimFogueira : MonoBehaviour
    {
        Animator animator;        
        void Awake()
        {
            animator = GetComponent<Animator>();
        }
        public void animacaoAcendendo()
        {
            animator.SetTrigger("acender");
        }
    }
}