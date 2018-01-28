using UnityEngine;

public class GeneratorBehaviour: MonoBehaviour
{
    public Color signalColor;
    [SerializeField]
    private Renderer pulsingRenderer;

    void Update()
    {
        float emission = Mathf.PingPong(Time.time, 1.0f);
        Color finalColor = signalColor * Mathf.LinearToGammaSpace(emission);
        pulsingRenderer.material.SetColor("_EmissionColor", finalColor);
    }

}
