using UnityEngine;
using YG;

public class MoveTutorial : MonoBehaviour
{
    [SerializeField] private bool _isMobile;

    private void Awake()
    {
        if (Application.isEditor && _isMobile || YG2.envir.isMobile || YG2.envir.isTablet)
            gameObject.SetActive(false);
    }
}