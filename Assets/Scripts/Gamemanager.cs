using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] int m_rows = 10;
    [SerializeField] int m_columns = 10;
    [SerializeField] GameObject m_cellPrefab = default;
    [SerializeField] Transform m_cellPanel = default;
    [SerializeField] float m_interval = 0.1f;

    Cell[,] m_cells = default;
    bool m_stoping = false;
    float m_timer = 0;

    private void OnValidate()
    {
        m_cellPanel.GetComponent<GridLayoutGroup>().constraintCount = m_columns;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_cells = new Cell[m_rows, m_columns];
        for (int i = 0; i < m_rows; i++)
        {
            for (int k = 0; k < m_columns; k++)
            {
                GameObject go = Instantiate(m_cellPrefab, m_cellPanel);
                m_cells[i, k] = go.GetComponent<Cell>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_stoping) return;

        m_timer += Time.deltaTime;
        if (m_timer > m_interval)
        {
            m_timer = 0;

            for (int i = 0; i < m_rows; i++)
            {
                for (int k = 0; k < m_columns; k++)
                {
                    Cell cell = m_cells[i, k];
                    int neighborNum = GetNeighborNumber(i, k);
                    if (!cell.Alive && neighborNum == 3) cell.NextAlive = true;
                    else if (cell.Alive && (neighborNum == 2 || neighborNum == 3)) cell.NextAlive = true;
                    else if (cell.Alive && neighborNum <= 1) cell.NextAlive = false;
                    else if (cell.Alive && neighborNum >= 4) cell.NextAlive = false;
                }
            }
            foreach (var cell in m_cells) cell.NextStep();
        }
    }

    int GetNeighborNumber(int indexR, int indexC)
    {
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int k = -1; k <= 1; k++)
            {
                int dirR = indexR + i;
                int dirC = indexC + k;
                if (0 > dirR || dirR >= m_rows || 0 > dirC || dirC >= m_columns) continue;
                if (m_cells[dirR, dirC] == m_cells[indexR, indexC]) continue;
                if (m_cells[dirR, dirC].Alive) count++;
            }
        }
        //Debug.Log($"{indexR},{indexC} : {count}");
        return count;
    }

    public void ResetCell()
    {
        foreach (var cell in m_cells) Destroy(cell.gameObject);
        Start();
    }

    public void StopCell()
    {
        m_stoping = !m_stoping;
    }
}
