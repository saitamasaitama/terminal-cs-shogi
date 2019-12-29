using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/**
コンソール描画はこのクラスでのみ行う
*/
public class BoardView : Renderer
{
    private bool rend=false;
    public pos offset = pos.From(0, 2);

    public override void Render(Transform t)
    {
      //一回だけ描画
      if(rend)return;

        //上段
        Console.SetCursorPosition(t.x + 1, t.y - 2);
        for (int i = 0; i < 9; i++)
        {
            Console.Write($"..{i + 1}..");
        }
        Console.SetCursorPosition(t.x + 1, t.y - 1);
        Console.Write(new string('_', 5 * 9 - 1));
        //上段　ここまで

        //右列　ここから
        for (int i = 0; i < 9; i++)
        {
            Console.SetCursorPosition(t.x + 46, t.y + i * 2);
            Console.Write($" {i + 1}");
            Console.SetCursorPosition(t.x + 46, t.y + 1 + i * 2);
            Console.Write($"__");
        }

        //右列　ここまで

        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                Console.SetCursorPosition(t.x + x * 5, t.y + y * 2);
                Console.Write("|    |");
                Console.SetCursorPosition(t.x + x * 5, t.y + 1 + y * 2);
                Console.Write("|____|");
            }
        }
        rend=true;
    }
}

