
using UnityEngine;
using System.Collections.Generic;

namespace InspectorEnhancements.Preview {
    public class CollectionDropdownExample2 : MonoBehaviour {
        [CollectionDropdown("GetSpawnPoints")]
        [SerializeField]
        private Vector3 spawnPoint;

        private List<Vector3> GetSpawnPoints() {
            return new List<Vector3> { new Vector3(0, 4, 2), new Vector3(5, 2, 27), new Vector3(8, 2, 5) };
        }
    }
}
