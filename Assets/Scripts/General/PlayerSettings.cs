﻿[System.Serializable]
public class PlayerSettings
{
    public int selectedDiffculty;
    public int selectedPuzzle;

    public bool clockEnabled;
    public bool autofillEnabled;
    public bool soundEnabled;

    public (float x, float y)[] levelSelectScreenPositions;

    public PlayerSettings()
    {
        selectedDiffculty = 0;

        clockEnabled = true;
        autofillEnabled = true;
        soundEnabled = true;
    }

    public void UpdateSettings(PlayerSettings ps)
    {
        selectedDiffculty = ps.selectedDiffculty;

        clockEnabled = ps.clockEnabled;
        autofillEnabled = ps.autofillEnabled;
        soundEnabled = ps.soundEnabled;
    }

    public void SaveLevelSelectScreenPositions((float, float)[] positions)
    {
        levelSelectScreenPositions = positions;
    }
}