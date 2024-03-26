using Fusion;
using UnityEngine;

namespace Capstone.Models
{
    public class GameModel
    {
        
    }

    public enum Buttons
    {
        JUMP = 0,
    }
    
    public struct NetworkPlayerInput: INetworkInput
    {
        public NetworkButtons ActionButtons;
        public Vector3 MoveDirection;
        public Vector2 LookVector;
    }
}
