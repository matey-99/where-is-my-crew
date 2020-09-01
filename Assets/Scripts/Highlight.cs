using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private Color highlightColor = default;

    private MeshRenderer meshRenderer = default;
    private Material baseMaterial = default;
    private Material highlightMaterial = default;

    public void Init(MeshRenderer meshRenderer)
    {
        this.meshRenderer = meshRenderer;
        baseMaterial = meshRenderer.material;

        highlightMaterial = new Material(baseMaterial);
        highlightMaterial.color = highlightColor;
    }

    public void HighlightOn()
    {
        meshRenderer.material = highlightMaterial;
    }

    public void HighlightOff()
    {
        meshRenderer.material = baseMaterial;
    }
}
