using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//using DangerMap = Dictionary<pos, Dictionary<TypeOwner, int>>;

public delegate void DrawCell(pos p);


/**
盤上の表示はこれで行う
*/
public class KomasView : Renderer
{
    public bool isMoveRender = true;
    public Koma? lastSelectKoma = null;

    private Dictionary<pos, Dictionary<TypeOwner, int>> areas;

    //特定座標に書き込む。
    private void BlitRaw(pos p, string[] lines, ConsoleColor color, bool clear = false)
    {

        int x = p.x * 5 + 1;
        int y = p.y * 2 + 2;
        Console.BackgroundColor = color;
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            char[] chars = line.ToCharArray();
            for (int off = 0; off < chars.Length; off++)
            {
                //スペースを上書きしない
                if (clear && chars[off] == ' ')
                {
                    continue;
                }
                Console.SetCursorPosition(
                    x + off,
                    y + i
                );
                Console.Write(chars[off]);

            }

        }
        Console.ResetColor();
    }

    private void BlitOwn(pos p, string[] lines, TypeOwner own)
    {
        //ownに応じて反転
        lines = own switch
        {
            TypeOwner.OU => lines,
            TypeOwner.GYOKU => lines.Reverse().ToArray(),
            _ => lines
        };
        for (int i = 0; i < lines.Length; i++)
        {
            pos pin = pos.From(p.x * 5 + 1, p.y * 2 + 2);
            Console.SetCursorPosition(
                pin.x,
                pin.y + i
            );
            Console.Write(lines[i]);
        }
    }

    public void ClearMovable()
    {
        Debug.Log("クリアする");
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                pos pos = new pos() { x = x, y = y };
                if (komas.Exists(k => k.position == pos))
                {
                    continue;
                }
                //Debug.Log("Blit");
                this.BlitOwn(pos, new string[]{
                    "    ",
                    "    "
                }, TypeOwner.OU);
            }
        }
        this.isMoveRender=false;
    }

    private List<Koma> komas;

    public KomasView(List<Koma> k)
    {
        this.komas = k;
    }

    private void BoardWalk(DrawCell d)
    {
        for (int y = 0; y < 9; y++)
            for (int x = 0; x < 9; x++)
                d(pos.From(x, y));
    }

    public override void Render(Transform t)
    {
        if(!this.isMoveRender)return;
        //まず座標をコレクトする
        Dictionary<pos, Dictionary<TypeOwner, int>> area = collectArea();

        BoardWalk(p =>
        {
            Koma k = komas.Find(k => k.position == p);
            if (k != null)
            {
                if (this.lastSelectKoma != null && k.Equals(this.lastSelectKoma))
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    RenderKoma(k);
                    Console.ResetColor();
                }
                else
                {
                    RenderKoma(k);
                }
            }
            else{
                this.BlitOwn(p, new string[]{
                    "    ",
                    "    "
                }, TypeOwner.OU);
            }


            //危険範囲
            if (area.ContainsKey(p))
            {
                //上段
                if (0 < area[p][TypeOwner.GYOKU])
                {
                    this.BlitRaw(p, new string[]{
                        "",
                        $"{area[p][TypeOwner.GYOKU],4}"
                    }, ConsoleColor.DarkRed,
                        komas.Exists(k=>k.position==p)
                    );

                }
                else
                {
                    this.BlitRaw(p, new string[]{
                        "",
                        $"    "
                    }, ConsoleColor.Black,
                    komas.Exists(k=>k.position==p)
                    );

                }
                if (0 < area[p][TypeOwner.OU])
                {
                    this.BlitRaw(p, new string[]{
                    $"{area[p][TypeOwner.OU],4}",
                    ""
                    }, ConsoleColor.DarkBlue,
                    komas.Exists(k=>k.position==p)
                    );
                }
                else
                {
                    this.BlitRaw(p, new string[]{
                        "    ",
                        $""
                    }, ConsoleColor.Black,
                    komas.Exists(k=>k.position==p)
                    );

                }
                //                return;
            }

        });
    }

    private Dictionary<pos, Dictionary<TypeOwner, int>> collectArea()
    {
        Dictionary<pos, Dictionary<TypeOwner, int>> result = new Dictionary<pos, Dictionary<TypeOwner, int>>();
        foreach (Koma koma in komas)
        {
            koma.CalcMovable.Where(b => this.inPutable(koma, b)).ToList()
            .ForEach(pos =>
            {
                //座標は存在するか
                if (!result.ContainsKey(pos))
                {
                    result.Add(pos, new Dictionary<TypeOwner, int>());
                }
                //キーが存在するか
                if (!result[pos].ContainsKey(koma.owner))
                {
                    result[pos].Add(TypeOwner.GYOKU, 0);
                    result[pos].Add(TypeOwner.OU, 0);
                }
                result[pos][koma.owner]++;
            });
        }
        return result;
    }

    private void RenderKoma(Koma k)
    {
        this.BlitOwn(k.position, new string[]{
                " -- ",
                $"|{k.ToLogo()}|"
            }, k.owner);
        //移動範囲を描画する
        Console.BackgroundColor = k.owner switch
        {
            TypeOwner.OU => ConsoleColor.DarkBlue,
            TypeOwner.GYOKU => ConsoleColor.DarkRed,
            _ => ConsoleColor.Black
        };

        Console.ResetColor();

    }

    private bool inArea(pos p) => ((0 <= p.x && p.x < 9) && (0 <= p.y && p.y < 9));
    private bool inPutable(Koma k, pos p)
    {
        if (!inArea(p)) return false;
        //味方の一にはおけない
        if (komas.Exists(vs => vs.owner == k.owner && vs.position == p))
        {
            return false;
        }
        //２歩チェック
        //置ける
        return true;
    }


    public override string ToString()
    {
        return base.ToString();
    }
}

