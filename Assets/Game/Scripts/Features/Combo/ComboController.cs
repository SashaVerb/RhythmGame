public class ComboController
{
    ComboView view;

    HitType lastHitTypeCombo = HitType.Good;
    private int comboCount = 0;

    int maxPerfectCombo = 0;

    public ComboController(ComboView view)
    {
        this.view = view;
    }

    public int MaxPerfectCombo => maxPerfectCombo;

    public int CurrentCombo => comboCount;

    public HitType ComboType => lastHitTypeCombo;

    public void CalculateHit(HitType hitType)
    {
        if (hitType == lastHitTypeCombo)
        {
            comboCount++;
        }
        else if(hitType == HitType.Perfect)
        {
            comboCount = 1;
        }

        lastHitTypeCombo = hitType;

        if (lastHitTypeCombo == HitType.Perfect && comboCount > maxPerfectCombo)
        {
            maxPerfectCombo = comboCount;
        }

        view.SetCombo(comboCount, hitType);
    }

    public void RestartCombo()
    {
        comboCount = 0;
        lastHitTypeCombo = HitType.Good;
        view.ResetCombo();
    }
}
