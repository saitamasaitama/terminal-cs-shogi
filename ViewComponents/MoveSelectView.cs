using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/**
コンソール描画はこのクラスでのみ行う
*/
public class MoveSelectView : Renderer
{
    public Koma koma;
    public Shogi shogi;

    private List<Koma> komas;

    public MoveSelectView(Shogi shogi,Koma k)
    {
        this.koma = k;
        this.shogi=shogi;
    }


    public override void Render(Transform t)
    {
        shogi.GetMovablePositions(koma)
        .ToList().ForEach(pos=>{

            //その座標にいるかどうか
            if(null!=shogi[pos]){
                //存在する。
                Debug.Log($"おるやん{pos}");
            }
            else{
                Debug.Log($"おらん{pos}");
                Blit(pos,new string[]{
                    "    ","    "
                });
            }
        });
    }

    public override string ToString()
    {
        return base.ToString();
    }

    private void Blit(pos p,string[] lines){
        int x = p.x * 5 + 1;
        int y = p.y * 2 + 2;
        for(int i=0;i<lines.Length;i++){
            string line=lines[i];
            Console.SetCursorPosition(
                    x,
                    y + i
            );
            Console.BackgroundColor=ConsoleColor.DarkBlue;
            Console.Write(line);
        }
    }
}

