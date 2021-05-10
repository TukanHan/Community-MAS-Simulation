using UnityEngine;

public class MapTextInfo : MonoBehaviour
{
    public TextMesh text;
    public SpriteRenderer colorFilter;
    public GameObject frame;
    public GameObject resourceSupply;
    public SpriteRenderer resourceRenderer;

    public Color minColor;
    public Color maxColor;

    public void EnableColorFilter(string str, float valuePercent)
    {
        colorFilter.gameObject.SetActive(true);

        colorFilter.color = Color.Lerp(minColor, maxColor, valuePercent);
        text.GetComponent<MeshRenderer>().sortingLayerName = "Units";
        text.text = str;       
    }

    public void EnableFrame()
    {
        frame.SetActive(true);
    }
    public void EnableResourceSuplyerInfo(Resource resource)
    {
        resourceSupply.SetActive(true);
        resourceRenderer.sprite = resource.image;
    }

    private void DisableColorInfo()
    {
        colorFilter.gameObject.SetActive(false);
    }

    private void DisableFrame()
    {
        frame.SetActive(false);
    }

    private void DisableResourceSuplyerInfo()
    {
        resourceSupply.SetActive(false);
    }

    public void Disable()
    {
        DisableFrame();
        DisableColorInfo();
        DisableResourceSuplyerInfo();
    }
}
