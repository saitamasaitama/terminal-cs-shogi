using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Editor{
  Dictionary<pos,color> data=new Dictionary<pos,color>();

  private pos cursor=pos.From(8,8);
  public pos size = pos.From(16,16);


  public Editor(){ 
    for(int x=0;x<16;x++){
      for(int y=0;y<16;y++){
        data.Add(pos.From(x,y),color.White);
      }
    }
  }

  public void Run(){
    Console.CancelKeyPress+=(object sender, ConsoleCancelEventArgs e)=>{

      Console.ResetColor();
    };
    Console.Clear();
    Console.ResetColor();
    this.RenderEditor();
    this.keyLoop();
  }

  private void keyLoop(){
    while(true){
      var k = Console.ReadKey(true);
      //      Console.Write(k.Key);

      var r=AllowControll(k)||DrawCommandControll(k);
      UpdateStatus();  //ステータス表示更新
      UpdateCursor();
    }
  }



  private void UpdateCursor(){
    Console.SetCursorPosition(
        5 + this.cursor.x * 2 ,
        1 + this.cursor.y
        );
  }

  private bool AllowControll(ConsoleKeyInfo k){
    if(
        !( 
          new List<ConsoleKey>(){
            ConsoleKey.UpArrow,
            ConsoleKey.DownArrow,
            ConsoleKey.LeftArrow,
            ConsoleKey.RightArrow
          }
         ).Exists(a=> a==k.Key )
      )
    {
      return false;
    }
    var v=k.Modifiers switch{
      0 =>this.SimpleAllowControl(k.Key),
        _ =>true
    };
    return true;
  }

  //キャンバスに描いたりなんだり
  private bool DrawCommandControll(ConsoleKeyInfo k){
    switch(k.Key) {
      case ConsoleKey.A:
        Console.Write("*");
        break;
      default:break;
    };
    return true;
  }


  

  //単に矢印操作する系
  private bool SimpleAllowControl(ConsoleKey k){

    pos diff=k switch{
      ConsoleKey.UpArrow => pos.From(0,-1),
        ConsoleKey.DownArrow => pos.From(0,1),
        ConsoleKey.LeftArrow => pos.From(-1,0),
        ConsoleKey.RightArrow => pos.From(1,0),
        _ => pos.From(0,0),
    };

    this.cursor+=diff;
    return true;
  }

  public void UpdateStatus(){
    Console.SetCursorPosition(
        Console.BufferWidth-10,
        Console.BufferHeight-1
        );
    Console.Write($"{cursor.x,3}:{cursor.y,3}");
  }

  public void RenderEditor(){
    //まずXYを書き込み
    //
    for(int y=0;y < this.size.y;y++){
      Console.SetCursorPosition(1,1+y);
      Console.Write($"{y,0:00} :");
    }

    for(int x=0;x < this.size.x;x++){
      Console.SetCursorPosition(5+x*2,0);
      Console.Write($"_|");
    }

    //フィールドを埋める
    for(int y=0;y<this.size.y;y++){
      for(int x=0;x<this.size.x;x++){
        Console.SetCursorPosition(5+x*2,1+y);
        Console.BackgroundColor=ConsoleColor.DarkGray;
        Console.Write("  ");
      }
    }
    //カラーパレット
    // 7 色 x 4
    for(int c=0;c<6;c++){
      Console.SetCursorPosition(2+c*2,Console.BufferHeight-4);
      Console.ForegroundColor=ConsoleColor.White;
      Console.Write("■");
      Console.ResetColor();

      Console.SetCursorPosition(2+c*2,Console.BufferHeight-3);
      Console.ForegroundColor = c switch{
        0=>ConsoleColor.Red,
          1=>ConsoleColor.Yellow,
          2=>ConsoleColor.Green,
          3=>ConsoleColor.Cyan,
          4=>ConsoleColor.Blue,
          5=>ConsoleColor.Magenta,
          _=>ConsoleColor.Black
      };      
      Console.Write("■");
      Console.ResetColor();

      Console.SetCursorPosition(2+c*2,Console.BufferHeight-2);
      Console.ForegroundColor = c switch{
        0=>ConsoleColor.DarkRed,
          1=>ConsoleColor.DarkYellow,
          2=>ConsoleColor.DarkGreen,
          3=>ConsoleColor.DarkCyan,
          4=>ConsoleColor.DarkBlue,
          5=>ConsoleColor.DarkMagenta,
          _=>ConsoleColor.Black
      };      
      Console.Write("■");
      Console.ResetColor();

      Console.SetCursorPosition(2+c*2,Console.BufferHeight-2);
      Console.ForegroundColor = c switch{
        0=>ConsoleColor.DarkRed,
          1=>ConsoleColor.DarkYellow,
          2=>ConsoleColor.DarkGreen,
          3=>ConsoleColor.DarkCyan,
          4=>ConsoleColor.DarkBlue,
          5=>ConsoleColor.DarkMagenta,
          _=>ConsoleColor.Black
      };      
      Console.Write("■");
      Console.ResetColor();

      Console.SetCursorPosition(2+c*2,Console.BufferHeight-1);
      Console.ForegroundColor = ConsoleColor.Black;
      Console.Write("■");
      Console.ResetColor();


    }

    Console.ResetColor();
  }

  public static Editor Create(){

    return new Editor();
  }
}

