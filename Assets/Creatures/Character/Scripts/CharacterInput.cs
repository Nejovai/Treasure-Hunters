using PixelCrew.Creatures;
using PixelCrew.UI.GameMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


namespace PixelCrew.Components
{
    public class CharacterInput : MonoBehaviour
    {
        [SerializeField] private Character _character;

        private void OnMovement(InputValue vector)
        {
            var direction = vector.Get<Vector2>();
            _character.SetDirection(direction);
        }

        private void OnAttack(InputValue attack)
        {
            _character.Attack();
        }

        private void OnInteract(InputValue interact)
        {
            _character.Interact();
        }

        private void OnDash(InputValue dash)
        {
            _character.Dash();
        }

        private void OnUseInventory(InputValue use)
        {
            _character.UseFromInventory();
        }

        private void OnMenu(InputValue menu)
        {
            var gameMenu = FindObjectOfType<GameMenuWindow>();
            if (gameMenu == null)
            {
                var window = Resources.Load<GameObject>("UI/GameMenuWindow");
                var canvas = FindObjectOfType<Canvas>();
                Instantiate(window, canvas.transform);
            }
            else
                gameMenu.Close();

        }

        private void OnNextItem()
        {
            _character.NextItem();
        }
    }
}

