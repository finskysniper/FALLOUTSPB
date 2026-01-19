using UnityEngine;

public class HexCell
{
    public int q;
    public int r;

    public bool walkable = true;
    public int height = 0;
    public int cover = 0;

    public HexCell(int q, int r)
    {
        this.q = q;
        this.r = r;
    }
}
