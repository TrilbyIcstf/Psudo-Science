using UnityEngine;

public class Board_Tile_Interact : MonoBehaviour
{
    private Color baseColor;
    private Renderer tileRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileRenderer = GetComponent<Renderer>();
        baseColor = tileRenderer.material.color;
    }

    private void OnMouseEnter()
    {
        tileRenderer.material.color = Color.yellowNice;
    }

    private void OnMouseExit()
    {
        tileRenderer.material.color = baseColor;
    }
}
