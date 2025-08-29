using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicOS : MonoBehaviour
{

    [SerializeField] TMP_FontAsset[] mFontAssets;
    [SerializeField] TMP_Text mText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (TMP_Settings.defaultFontAsset.fallbackFontAssetTable == null)
        {
            TMP_Settings.defaultFontAsset.fallbackFontAssetTable = new List<TMP_FontAsset>();
        }
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                TMP_Settings.defaultFontAsset.fallbackFontAssetTable.Add(mFontAssets[0]);
                break;
            case RuntimePlatform.Android:
                TMP_Settings.defaultFontAsset.fallbackFontAssetTable.Add(mFontAssets[1]);
                break;
            case RuntimePlatform.IPhonePlayer:
                TMP_Settings.defaultFontAsset.fallbackFontAssetTable.Add(mFontAssets[2]);
                break;
            default:
                Debug.LogWarning("Unsupported platform.");
                break;
        }


        var name = TMP_Settings.defaultFontAsset.fallbackFontAssetTable[0].faceInfo.familyName + "-" + TMP_Settings.defaultFontAsset.fallbackFontAssetTable[0].faceInfo.styleName;
        mText.text = name;
    }

    private void OnDestroy()
    {
        TMP_Settings.defaultFontAsset.fallbackFontAssetTable.Clear();
    }
}
