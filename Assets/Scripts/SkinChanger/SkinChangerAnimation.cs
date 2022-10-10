using UnityEngine;

public class SkinChangerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private SkinChanger _skinChanger;

    private void Awake()
    {
        _skinChanger = GetComponent<SkinChanger>();
    }

    private void OnEnable()
    {
        _skinChanger.ItemPressed += OnItemPressed;
    }

    private void OnDisable()
    {
        _skinChanger.ItemPressed -= OnItemPressed;
    }

    private void OnItemPressed()
    {
        _animator.SetTrigger(AnimatorSkinChangerController.States.JumpOut);
    }
}