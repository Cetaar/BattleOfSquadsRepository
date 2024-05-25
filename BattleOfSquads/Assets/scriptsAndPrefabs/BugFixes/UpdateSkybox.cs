using UnityEngine;

public class UpdateSkybox : MonoBehaviour
{
    //[SerializeField] private string _Cube;
    [SerializeField] private Cubemap cubeMapTexture;
    private void Start()
    {
        UpdateReflectionWithNewSkybox();
    }
    private void UpdateReflectionWithNewSkybox()
    {
        Renderer[] renderers = Resources.FindObjectsOfTypeAll<Renderer>();

        //Cubemap currentSkybox = RenderSettings.skybox.GetTexture(_Cube) as Cubemap;
        Cubemap currentSkybox = cubeMapTexture;

        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.sharedMaterials;

            foreach (Material material in materials)
            {
                if (material.HasProperty("_Metallic") && material.HasProperty("_Smoothness"))
                {
                    material.SetFloat("_Metallic", 0f);
                    material.SetFloat("_Smoothness", 0.5f);

                    material.SetTexture("_Cube", currentSkybox);

                    material.SetFloat("_Metallic", 1f);
                    material.SetFloat("_Smoothness", 1f);
                }
            }
        }
    }
}
