using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/**
コンソール描画はこのクラスでのみ行う
*/
public class ShogiRenderer{
  public pos offset=pos.From(0,2);
  //
  private void Blit(pos p,string[] lines){
    Console.SetCursorPosition(p.x,p.y);
  }
  //反転する場合もあり
  private void BlitOwn(pos p,string[] lines,TypeOwner own){
    //ownに応じて反転
    lines=own switch{
      TypeOwner.GYOKU   => lines,
      TypeOwner.OU =>lines.Reverse().ToArray()
    }; 
    for(int i=0;i<lines.Length;i++){
      pos pin = pos.From(p.x*5+1,p.y*2+2 );
      Console.SetCursorPosition(pin.x,pin.y+i);
      Console.Write(lines[i]);
    }
  }
  public void RenderBoard(){
    //上段
    Console.SetCursorPosition(offset.x+1,offset.y-2);
    for(int i=0;i<9;i++){
      Console.Write($"..{i+1}..");
    }
    Console.SetCursorPosition(offset.x+1,offset.y-1);
    Console.Write(new string('_',5*9-1));
    //上段　ここまで

    //右列　ここから
    for(int i=0;i<9;i++){
      Console.SetCursorPosition(offset.x+46,offset.y+ i*2);
      Console.Write($" {i+1}");
      Console.SetCursorPosition(offset.x+46,offset.y+1+ i*2);
      Console.Write($"__");
    }

    //右列　ここまで

    for(int y=0;y<9;y++){
      for(int x=0;x<9;x++){
        Console.SetCursorPosition(offset.x + x*5,offset.y +y*2);
        Console.Write("|    |");
        Console.SetCursorPosition(offset.x + x*5,offset.y+1+y*2);
        Console.Write("|____|");
    }}
  }

  //
  //  --  |歩|
  // |歩|  --

  public void RenderKoma(Koma k){
    this.BlitOwn(k.position,new string[]{
      " -- ",
     $"|{k.ToLogo()}|"
    },
    k.owner);
  }

  public static void CreateFromShogi(ShogiController s){
    ShogiRenderer renderer=new ShogiRenderer();

    s.renderBoardEvent+=renderer.RenderBoard;
    s.renderKomaEvent+=renderer.RenderKoma;
  }
}

