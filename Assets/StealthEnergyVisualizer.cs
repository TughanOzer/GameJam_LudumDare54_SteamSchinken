using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthEnergyVisualizer : MonoBehaviour
{
    #region Fields and Properties

    [SerializeField] private List<GameObject> _energyMarkers = new();
    [SerializeField] private GameObject _energyMarkerPrefab;
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
        if (_energyMarkers.Count > Player.Instance.LeftOverStealthUses)
        {
            var energyMarker = _energyMarkers[0];
            _energyMarkers.RemoveAt(0);
            StartCoroutine(DestroyMarker(energyMarker));
        }
    }

    private void InstantiateEnergyMarker()
    {
        if (_energyMarkers.Count < Player.Instance.LeftOverStealthUses)
        {
            var energyMarker = Instantiate(_energyMarkerPrefab, _layoutGroup.transform).GetComponent<Image>();
            energyMarker.DOFade(0, 0.0001f);
            energyMarker.DOFade(1, 1f).SetEase(Ease.OutBounce);
            _energyMarkers.Add(energyMarker.gameObject);
        }
    }

    private IEnumerator DestroyMarker(GameObject energyMarker)
    {
        var markerImage = energyMarker.GetComponent<Image>();
        markerImage.DOFade(0, 1).SetEase(Ease.InBounce);
        yield return new WaitForSeconds(1.1f);
        Destroy(energyMarker);
    }

    #endregion
}
