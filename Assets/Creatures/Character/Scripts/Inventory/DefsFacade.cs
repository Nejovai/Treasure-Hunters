using UnityEngine;
using PixelCrew.Model.Definitions.Repositories;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemsDef _items;
        [SerializeField] private PlayerDef _player;
        [SerializeField] private ThrowableItemDef _throwableItems;
        [SerializeField] private PotionalItemDef _potionalItems;
        [SerializeField] private PerkRepository _perks;

        public InventoryItemsDef Items => _items;
        public ThrowableItemDef Throwable => _throwableItems;
        public PotionalItemDef Potional => _potionalItems;
        public PlayerDef Player => _player;

        public PerkRepository Perks => _perks;


        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
           return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }

}
