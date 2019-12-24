using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public struct Koma{
  public int Owner;
  public int KomaClass;
  public pos position;
}
public class Shogi{
  Dictionary<pos,Koma> board=new Dictionary<pos,Koma>();
  public delegate void renderEventHandler(pos offset);
  public delegate void ShogiRenderEventHandler(Object sender);
  public delegate void GrobalEventHandler();
  public event renderEventHandler renderEvent;
  public event GrobalEventHandler renderBoardEvent;
  public event renderEventHandler komaRenderEvent;

  private pos cursor=pos.From(5,1);
  public pos size = pos.From(9,9);


  public void initialize(){ 
//board initialize
  }

  public void Run(){
    Console.CancelKeyPress+=(object sender, ConsoleCancelEventArgs e)=>{
      Console.ResetColor();
    };
    Console.Clear();
    Console.ResetColor();
    renderBoardEvent();
    //renderEvent(pos.From(0,0));
    //event sets all
    this.keyLoop();
  }

  private void keyLoop(){
    while(true){
      var k = Console.ReadKey(true);
      //      Console.Write(k.Key);

      var r=AllowControll(k);
      UpdateStatus();  //ステータス表示更新
    }
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
    return k.Modifiers switch{
      0 =>this.SimpleAllowControl(k.Key),
      _ =>false
    };
  }
  private bool SimpleAllowControl(ConsoleKey key){
    return true;
  } 

  public void UpdateStatus(){
    Console.SetCursorPosition(
        Console.BufferWidth-10,
        Console.BufferHeight-1
        );
    Console.Write($"{cursor.x,3}:{cursor.y,3}");
  }

  public static Shogi Create(){

    return new Shogi();
  }
}

