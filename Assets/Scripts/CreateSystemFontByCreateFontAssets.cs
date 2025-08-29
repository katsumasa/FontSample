using TMPro;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.TextCore.Text;


// This script creates a TMP_FontAsset from a system font using the CreateFontAsset method.

public class CreateSystemFontByCreateFontAssets : MonoBehaviour
{
    [SerializeField] TMP_Dropdown mDropdown;
    [SerializeField] TMP_Text mText;

    string[] mSystemFontNames;
    TMP_FontAsset mFontAsset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        mFontAsset = null;
        mSystemFontNames = FontEngine.GetSystemFontNames();
        mDropdown.captionText.SetText("System Font List");
        mDropdown.ClearOptions();
        foreach (var fontName in mSystemFontNames)
        {
            mDropdown.options.Add(new TMP_Dropdown.OptionData(fontName));
        }
        mDropdown.value = 0;
        mDropdown.RefreshShownValue();        
    }
   

public void OnDropdownValueChanged(int index)
    {

        if (mFontAsset != null)
        {
            TMP_FontAsset.Destroy(mFontAsset);
            mFontAsset = null;
        }
        string[] fontFamilyAndStyleName = mSystemFontNames[index].Split(" - ");
        mFontAsset = TMP_FontAsset.CreateFontAsset(fontFamilyAndStyleName[0], fontFamilyAndStyleName[1]);
#if false
        mText.font = mFontAsset;
#else
        mText.font.fallbackFontAssetTable.Clear();
        mText.font.fallbackFontAssetTable.Add(mFontAsset);
#endif

    }
}
