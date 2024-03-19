using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameSettings;

public class HintHandler : MonoBehaviour
{
    int hintsRemaining;     //número de pistas permitidas por rompecabezas
    int fillCount;          //número de celdas para completar por pista

    List<Vector2Int> hintCellCoordinates;
    public event System.Action<List<Vector2Int>> PerformHintAction;
    public event System.Action<string> CannotPerformHintAction;

    Button hintButton;
    TextMeshProUGUI hintsRemainingText;

    void Awake()
    {
        hintButton = GetComponent<Button>();
        hintsRemainingText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        ResetHintsRemaining();
        fillCount = hintsRemaining;
        hintCellCoordinates = new List<Vector2Int>(fillCount);
    }

    // establecer el número inicial de pistas según el tamaño del tablero
    // también llamado al presionar el botón Reiniciar
    public void ResetHintsRemaining()
    {
        SetHintsRemaining(playerSettings.selectedDiffculty + 1);
    }

    void SetHintsRemaining(int amount)
    {
        hintsRemaining = amount;
        hintsRemainingText.text = $"Pistas:\n{amount}";
    }

    public void OnSelectHint()
    {
        if (hintsRemaining > 0)
        {
            if (CanPerformHint())
            {
                PerformHintAction?.Invoke(hintCellCoordinates);
                SetHintsRemaining(hintsRemaining - 1);
            }
            else
            {
                CannotPerformHintAction?.Invoke("El puzzle está casi completo..¡No más pistas!");
            }
        }
        else
        {
            CannotPerformHintAction?.Invoke("No quedan más pistas.");
        }
    }

    //check if there are enough unfilled cells to perform a hint
    bool CanPerformHint()
    {
        hintCellCoordinates.Clear();

        List<Vector2Int> unfilledCells = new();

        //get list of unfilled cells' coordinates
        for (int row = 0; row < targetPuzzleData.RowCount; row++)
        {
            for (int col = 0; col < targetPuzzleData.ColCount; col++)
            {
                if (targetPuzzleData.cellData.Cells[row, col] == CellType.Filled &&
                    currentPuzzleData.cellData.Cells[row, col] != CellType.Filled)
                {
                    unfilledCells.Add(new Vector2Int(row, col));
                }
            }
        }

        //if there are enough unfilled cells, get <fillCount> random elements
        if (unfilledCells.Count > fillCount * 2)
        {
            hintCellCoordinates = unfilledCells.OrderBy(i => Random.value).Take(fillCount).ToList();
            return true;
        }
        else return false;
    }
}