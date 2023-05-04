using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankVisuals : MonoBehaviour
{
    public float flashDuration = 0.2f;

    private Dictionary<MeshRenderer, Color[]> OriginalColors;

    private void Start(){
        OriginalColors = GetMaterials();
    }

    private Dictionary<MeshRenderer, Color[]> GetMaterials(){
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(true);
        Dictionary<MeshRenderer, Color[]> originalColors = new Dictionary<MeshRenderer, Color[]>();

        foreach (MeshRenderer renderer in renderers)
        {
            Color[] colors = new Color[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                colors[i] = renderer.materials[i].color;
            }
            originalColors.Add(renderer, colors);
        }
        return originalColors;
    }

    public IEnumerator FlashRed(){
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(true);
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration){
            elapsedTime += Time.deltaTime;

            float lerpFactor = elapsedTime / flashDuration;

            foreach (MeshRenderer renderer in renderers){
                if (OriginalColors.ContainsKey(renderer)){
                    for (int i = 0; i < renderer.materials.Length; i++){
                        renderer.materials[i].color = Color.Lerp(Color.red, OriginalColors[renderer][i], lerpFactor);
                    }
                }
            }

            yield return null;
        }

        foreach (MeshRenderer renderer in renderers){
            if (OriginalColors.ContainsKey(renderer)){
                for (int i = 0; i < renderer.materials.Length; i++) {
                    renderer.materials[i].color = OriginalColors[renderer][i];
                }
            }
        }
    }
}
