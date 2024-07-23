using Cinemachine;
using PixelCrew.Creatures;
using UnityEngine;

namespace PixelCrew.Components.LevelManegement
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class SetFollowComponent : MonoBehaviour
    {
        private void Start()
        {
            var vCamera = GetComponent<CinemachineVirtualCamera>();
            vCamera.Follow = FindObjectOfType<Character>().transform;
        }
    }
}
