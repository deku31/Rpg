using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class NotifManager : MonoBehaviour
{
    [SerializeField]private Animator NotifAnim;
    [Space]
    public bool Horizontal=false;
    private bool verttical=true;
    [Space]
    public bool _play=false;
    public float timeHide;
    [Space]
    [Header("UI")]
    public TextMeshProUGUI text;
    public string _text;

    public Image LogoImg;
    public Sprite _Logo;

    private void Start()
    {
        _play = true;
        text.text = _text;
        LogoImg.sprite = _Logo;
        if (_play==true)
        {
            NotifAnim.SetBool("Play", true);
        }
    }
    private void Update()
    {
        if (_play==true)
        {
            NotifAnim.SetBool("Play", true);
            if (Horizontal==true)
            {
                verttical = false;
            }
            else
            {
                verttical = true;
            }
            StartCoroutine(HideNotif(timeHide));
        }
        text.text = _text;

        NotifAnim.SetBool("horizontal", Horizontal);
        NotifAnim.SetBool("vertical", verttical);
    }
    private IEnumerator HideNotif(float time)
    {
        yield return new WaitForSeconds(time);
        NotifAnim.SetBool("Play", false);
        _play = false;
        StopCoroutine(HideNotif(0));
    } 
}
