using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthEnergyVisualizer : MonoBehaviour
{
    #region Fields and Properties

    [SerializeField] private List<GameObject> _fullEnergyMarkers = new();
    [SerializeField] private List<GameObject> _emptyEnergyMarkers = new();
    [SerializeField] private GameObject _fullMarkerPrefab;
    [SerializeField] private GameObject _emptyMarkerPrefab;
    [SerializeField] private HorizontalLayoutGroup _layoutGroup;

    #endregion

    #region Methods

    private void OnEnable()
    {
        Player.OnStealthEnergyGained += StealthEnergyGained;
        Player.OnStealthEnergyLost += StealthEnergyLost;
    }

    private void OnDisable()
    {
        Player.OnStealthEnergyGained -= StealthEnergyGained;
        Player.OnStealthEnergyLost -= StealthEnergyLost;
    }

    private void Start()
    {
        for (int i = 0; i < Player.Instance.LeftOverStealthUses; i++)
            InstantiateEnergyMarker();
    }

    private void StealthEnergyGained(int amount)
    {
        for (int i = 0; i < amount; i++)
            InstantiateEnergyMarker();
    }

    private void StealthEnergyLost()
    {
        if (_fullEnergyMarkers.Count > Player.Instance.LeftOverStealthUses)
        {
            var energyMarker = _fullEnergyMarkers[0];
            _fullEnergyMarkers.RemoveAt(0);
            StartCoroutine(DestroyMarker(energyMarker));
        }
    }

    private void InstantiateEnergyMarker()
    {
        if (_fullEnergyMarkers.Count < Player.Instance.LeftOverStealthUses)
        {
            if (_emptyEnergyMarkers.Count > 0)
            {
                var emptyMarker = _emptyEnergyMarkers[0];
                _emptyEnergyMarkers.RemoveAt(0);
                Destroy(emptyMarker);
            }

            var energyMarker = Instantiate(_fullMarkerPrefab, _layoutGroup.transform).GetComponent<Image>();
            energyMarker.DOFade(0, 0.0001f);
            energyMarker.DOFade(1, 1f).SetEase(Ease.OutBounce);
            _fullEnergyMarkers.Add(energyMarker.gameObject);
        }
    }

    private IEnumerator DestroyMarker(GameObject energyMarker)
    {
        var markerImage = energyMarker.GetComponent<Image>();
        markerImage.DOFade(0, 1).SetEase(Ease.InBounce);
        yield return new WaitForSeconds(1.1f);
        Destroy(energyMarker);
        var emptyMarker = Instantiate(_emptyMarkerPrefab, _layoutGroup.transform);
        _emptyEnergyMarkers.Add(emptyMarker);
    }

    #endregion
}
