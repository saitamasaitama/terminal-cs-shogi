using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/**
コンソール描画はこのクラスでのみ行う
*/
public class ShogiRenderer{
  public pos offset=pos.From(0,2);
  private void Blit(pos p,string[] lines){
    Console.SetCursorPosition(p.x,p.y);
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

  public void RenderKoma(pos p){
    Console.Write("A");
  }

  public static void CreateFromShogi(Shogi s){
    ShogiRenderer renderer=new ShogiRenderer();

    s.renderBoardEvent+=renderer.RenderBoard;


  }
}

