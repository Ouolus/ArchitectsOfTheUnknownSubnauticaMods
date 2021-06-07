using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    class RelicCaseDecoration : MonoBehaviour
    {
        public Transform spawnPosition;
        
        // add whatever other item thats allowed to be displayed
        List<TechType> _allowedTechTypes = new()
        {
            TechType.Titanium,
            TechType.Lithium
        };

        GameObject _spawnedObj;

        StorageContainer _storageContainer;
        
        bool _initialized;

        void Awake() => _storageContainer = GetComponent<StorageContainer>();

        void OnEnable()
        {
            if (!_initialized)
            {
                _storageContainer.enabled = true;
                _storageContainer.container.onAddItem += AddItem;
                _storageContainer.container.onRemoveItem += RemoveItem;
                _storageContainer.container.isAllowedToAdd += IsAllowedToAdd;
                _initialized = true;
            }
        }

        void OnDisable()
        {
            if (_initialized)
            {
                _storageContainer.container.onAddItem -= AddItem;
                _storageContainer.container.onRemoveItem -= RemoveItem;
                _storageContainer.container.isAllowedToAdd = null;
                _storageContainer.enabled = false;
                _initialized = false;
            }
        }

        bool IsAllowedToAdd(Pickupable pickupable, bool verbose)
        {
            var tt = pickupable.GetTechType();
            return _allowedTechTypes.Contains(tt);
        }
        void AddItem(InventoryItem item) => Spawn(item.item.gameObject);

        void RemoveItem(InventoryItem item) => DeSpawn();

        void Spawn(GameObject obj)
        {
            _spawnedObj = Instantiate(obj, spawnPosition, false);
            Destroy(_spawnedObj.GetComponent<Rigidbody>());
            Destroy(_spawnedObj.GetComponent<WorldForces>());
            _spawnedObj.SetActive(true);
        }

        void DeSpawn()
        {
            if (_spawnedObj == null)
                return;
            
            Destroy(_spawnedObj);
        }
    }
}
