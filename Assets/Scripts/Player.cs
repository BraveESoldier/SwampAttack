using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _currentWeaponIndex = 0;
    private int _currentHealth;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanched;
    public event UnityAction<int> MoneyChanched;

    private void Start()
    {
        ChangeWeapon(_weaponList[_currentWeaponIndex]);
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    public void ApllyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanched?.Invoke(_currentHealth, _health);

        if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanched?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanched?.Invoke(Money);
        _weaponList.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_currentWeaponIndex == _weaponList.Count - 1)
            _currentWeaponIndex = 0;
        else _currentWeaponIndex++;

        ChangeWeapon(_weaponList[_currentWeaponIndex]);
    }

    public void PreviousWeapon()
    {
        if(_currentWeaponIndex == 0) 
            _currentWeaponIndex = _weaponList.Count - 1;
        else _currentWeaponIndex--;

        ChangeWeapon(_weaponList[_currentWeaponIndex]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}
