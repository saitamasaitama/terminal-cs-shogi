using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum TypePlayer{
  CPU,
  PLAYER
}


public class Player{
  public TypeOwner owner;
  public TypePlayer player;
  public List<Koma> Komas=new List<Koma>();

}

/**
 * 実質コントロールクラスになっている
 *
 * */
public class Shogi{
  Dictionary<pos,Koma> board=new Dictionary<pos,Koma>();

  public Koma[] Komas=>board.Values.ToArray();

  private Shogi(){
    //駒追加
    var komas=Koma.DefaultSet;
    foreach(Koma k in komas){
      board.Add(k.position,k);
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
      //次に
      return false;
    }

    return k.Modifiers switch{
      0 =>this.SimpleAllowControl(k.Key),
        _ =>false
    };
  }


  private bool SimpleAllowControl(ConsoleKey key){
    //スクロールは一次元
    return true;
  } 

  /*
   * これは必要ない
   * */
  public void UpdateStatus(){
    Console.SetCursorPosition(
        Console.BufferWidth-10,
        Console.BufferHeight-1
        );
//    Console.Write($"{cursor.x,3}:{cursor.y,3}");
  }

  //ShogiControllerインスタンスが生成される
  public static ShogiController Create(){

    return ShogiController.FromShogi(new Shogi());
  }
}

