using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/**
コンソール描画はこのクラスでのみ行う
*/
public class MessageView : Renderer
{
    private bool rendered= false;
    private string message="";
    public string Message=>message;
    public override void Render(Transform t)
    {
        if(rendered)return;
        //最後の行から1つ上に書き込む
        Console.SetCursorPosition(0,Console.BufferHeight-2);
        Console.Write($"[{Message}]");
        rendered=true;

    }

    public static MessageView operator + (MessageView view,string message){
        view.rendered=false;
        view.message=message;
        return view;
    }
}

