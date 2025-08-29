using NUnit.Framework;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using System.Collections.Generic;
using TMPro;
using UnityEngine.TextCore.Text;

public class SearchSystemFonts : MonoBehaviour
{
    [SerializeField] TMP_Dropdown mDropdown;
    [SerializeField] TMP_Text mText;
    TMP_FontAsset mFontAsset;
    List<string> mSystemFontNames;


    private void Awake()
    {
        mSystemFontNames = new List<string>();
        mFontAsset = null;

        string[] systemFontNames = FontEngine.GetSystemFontNames();
        mDropdown.ClearOptions();

        for (int i = 0; i < systemFontNames.Length; i++)
        {
            var familyNamesAndStyles = systemFontNames[i].Split(" - ".ToCharArray());
            FontEngine.LoadFontFace(familyNamesAndStyles[0], familyNamesAndStyles[1]);
            if (FontEngine.TryGetGlyphIndex('あ', out uint glyphIndex))
            {
                mDropdown.options.Add(new TMP_Dropdown.OptionData(systemFontNames[i]));
                mSystemFontNames.Add(systemFontNames[i]);
            }
            FontEngine.UnloadFontFace();
        }
        mDropdown.value = 0;
        mDropdown.RefreshShownValue();
        OnDropdownValueChanged(mDropdown.value);
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
        // Fontのフォールバックを更新した場合、SetAllDirty()を呼ぶ必要がある
        mText.SetAllDirty();
#endif

    }
}
