using System.Collections.Generic;
using ClickerQuest.Characters;
using UnityEngine;
using UnityEngine.EventChannels;
using UnityEngine.Utils;
namespace ClickerQuest.Combat
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private CharactersInBattle _charactersInBattle;
        [SerializeField] private GameObjectEventChannel _characterDespawn;
        [SerializeField] private CharacterEventChannel _darkPassageUsed;
        [SerializeField] private VoidEventChannel _allSpawnsOccupied;
        [SerializeField] private EnemyLevelUpStats _enemyLevelUpStats;
        [SerializeField] private CharacterInCombat _characterInCombatPrefab;
        [SerializeField] private List<Transform> _spawnsTransforms = new List<Transform>();
        [SerializeField] private List<Character> _enemiesToSpawn = new List<Character>();
        [SerializeField] private Character _boss;
        
        [SerializeField] private int _enemiesToBoss;
        private int _enemiesCount;

        private void OnEnable()
        {
            _characterDespawn.GameEvent += SpawnEnemy;
            _darkPassageUsed.GameEvent += EnemySpawnedByBoss;
            _enemiesCount = 0;
        }

        private void OnDisable()
        {
            _characterDespawn.GameEvent -= SpawnEnemy;
            _darkPassageUsed.GameEvent -= EnemySpawnedByBoss;
        }
        
        private void Start()
        {
            _charactersInBattle.ClearEnemies();
            RestoreAllEnemiesLevels();
            SpawnRandomEnemy(_spawnsTransforms[0]);
        }
        
        private void RestoreAllEnemiesLevels()
        {
            foreach (Character enemy in _enemiesToSpawn)
                enemy.RestoreLevels();
        }

        private void SpawnEnemy(GameObject characterInCombat)
        {
            if(characterInCombat.GetComponent<CharacterInCombat>().Character is not Enemy) 
                return;
            
            _enemiesCount++;

            if (_enemiesCount == _enemiesToBoss)
            {
                DestroyEnemiesinSpawns();
                SpawnBoss(_spawnsTransforms[2]);
                return;
            }
            
            if (_enemiesCount == _enemiesToBoss / 3)
                SpawnRandomEnemy(_spawnsTransforms[1]);
            else if (_enemiesCount == _enemiesToBoss * 2 / 3)
                SpawnRandomEnemy(_spawnsTransforms[2]);

            SpawnRandomEnemy(characterInCombat.transform.parent);
        }
        
        private void DestroyEnemiesinSpawns()
        {
            foreach (Transform spawnsTransform in _spawnsTransforms)
            {
                CharacterInCombat enemyInSpawns = spawnsTransform.GetComponentInChildren<CharacterInCombat>(true);
                Destroy(enemyInSpawns.gameObject);
            }
            
            _charactersInBattle.ClearEnemies();
        }

        private void SpawnBoss(Transform spawn)
        {
            Character enemyToAdd = Instantiate(_boss);
            enemyToAdd.SetStats(_boss.Stats);

            Spawner spawner = spawn.GetComponentInChildren<Spawner>(true);
            spawner.Spawned = () => 
            {
                CharacterInCombat characterInCombat = Instantiate(_characterInCombatPrefab, spawn);
                characterInCombat.Initialize(enemyToAdd);
                _charactersInBattle.AddEnemy(characterInCombat);
            };
            spawner.gameObject.SetActive(true);
        }
        
        private void SpawnRandomEnemy(Transform spawn)
        {
            Character randomEnemy = _enemiesToSpawn.RandomItem();

            if (_enemiesCount >= 1)
                _enemyLevelUpStats.LevelUp(randomEnemy);
            
            Character enemyToAdd = Instantiate(randomEnemy);
            enemyToAdd.SetStats(randomEnemy.Stats);

            Spawner spawner = spawn.GetComponentInChildren<Spawner>(true);
            spawner.Spawned = () => 
            {
                CharacterInCombat characterInCombat = Instantiate(_characterInCombatPrefab, spawn);
                characterInCombat.Initialize(enemyToAdd);
                _charactersInBattle.AddEnemy(characterInCombat);
            };
            spawner.gameObject.SetActive(true);
        }
        
        private void EnemySpawnedByBoss(Character enemy)
        {
            Transform spawn = DetermineAvailableSpawn();
            if(!spawn)
            {
                _allSpawnsOccupied.RaiseEvent();
                return;
            }
            
            Character enemyToAdd = Instantiate(enemy);
            enemyToAdd.RestoreLevels();
            for (int i = 0; i < 9; i++)
                _enemyLevelUpStats.LevelUp(enemyToAdd);
            
            enemyToAdd.SetStats(enemyToAdd.Stats);
            
            Spawner spawner = spawn.GetComponentInChildren<Spawner>(true);
            spawner.Spawned = () => 
            {
                CharacterInCombat characterInCombat = Instantiate(_characterInCombatPrefab, spawn);
                characterInCombat.Initialize(enemyToAdd);
                _charactersInBattle.AddEnemy(characterInCombat);
            };
            spawner.gameObject.SetActive(true);
        }
        
        private Transform DetermineAvailableSpawn()
        {
            foreach (Transform spawnTransform in _spawnsTransforms)
                if (!IsCharacterInCombat(spawnTransform))
                    return spawnTransform;

            _allSpawnsOccupied.RaiseEvent();
            return null;
        }
        
        private bool IsCharacterInCombat(Transform spawnTransform)
        {
            return spawnTransform.GetComponentInChildren<CharacterInCombat>(true);
        }
    }
}