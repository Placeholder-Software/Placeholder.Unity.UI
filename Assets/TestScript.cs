using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.PropertyDrawers;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.Validators;
using UnityEngine;

public class TestScript
    : MonoBehaviour
{
    public enum SomeOptions
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
    }

    public bool Section2Collapsed;
    public bool Section3Collapsed;
    public bool Section4Enabled;
    public bool MuteSection;
    public bool SoloSection;
    public bool BypassSection;

    [IsOdd]
    [Range(0, 10)]
    public float A;

    [IsEven]
    public float B;

    [RequireNotEmpty(Highlight.Error)]
    public string Str;

    [RequiredNotNull(Highlight.Error)]
    public GameObject C;

    [RequiredNotNull(Highlight.Warn)]
    public GameObject D;

    [RequiredNotNull(Highlight.Warn, Highlight.OkGreen)]
    public GameObject E1;

    [RequiredNotNull(Highlight.Warn, Highlight.OkGreen)]
    public GameObject E2;

    [RequiredNotNull(Highlight.Warn, Highlight.OkGreen)]
    public GameObject E3;

    [SerializeField, RequiredNotNull(Highlight.Warn)]
    public GameObject[] F;

    [MinMax(5, 10, true)]
    public Vector2 G;

    [MinMax(5, 10, false)]
    public Vector2 H;

    [IsEven]
    public float I;

    [IsEven]
    public float J;

    [SerializeField]
    public SomeOptions K;

    [SerializeField, IsEven] public int OptionFirst;
    [SerializeField] public float OptionSecond;
    [SerializeField, RequiredNotNull] public GameObject OptionThird;
    [SerializeField, RequiredNotNull] public Transform OptionFourth;
}

