using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using TMPro;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;

    [SerializeField] Toggle fullScreenToggle;

    [SerializeField] TMP_Dropdown resDropdown;
    Resolution[] resolutions;

    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        GetResolutionOpt();
    }

    // Update is called once per frame
    void Update()
    {
        SetMasterVolume();
    }
    
    float ConvertToDec(float SliderValue){
        return Mathf.Log10(Mathf.Max(SliderValue,.0001f))*20;
    }

    public void SetMasterVolume(){
        audioMixer.SetFloat("MasterVolume",ConvertToDec(masterVolumeSlider.value));
    }

    public void OpenOptions(){
        canvas.enabled = true;

    }
    public void CloseOptions(){
        canvas.enabled = false;
    }

    void GetResolutionOpt(){
        resDropdown.ClearOptions();
        resolutions = Screen.resolutions;

        for(int i = 0; i < resolutions.Length;i++){
            TMP_Dropdown.OptionData newOption;
            newOption = new TMP_Dropdown.OptionData(resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString());
            resDropdown.options.Add(newOption);
        }
    }
    public void chooseResolution(){
        Screen.SetResolution(resolutions[resDropdown.value].width,resolutions[resDropdown.value].height,fullScreenToggle.isOn);
    }
}