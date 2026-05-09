using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entidades.Enums
{
    public enum EstadoJogador 
    {
        Ocupado,

        Parado,
        Andando,        
        Interagindo,
        SacandoArma,
        GuardandoArma,

        ModoAtaque,
        Atacando,
        AndandoArmado,
    }
}