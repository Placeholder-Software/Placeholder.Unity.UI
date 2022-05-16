using PropertyDrawers;
using UnityEngine;
using Validators;

public class TestScript
    : MonoBehaviour
{
    public enum SomeOptions
    {
        TheFirst,
        TheSecond,
        NotTheFifth,
        NotTheFifthEither,
        Five,
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
}

