using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public enum TypeControlArrow{
  UP,
  DOWN,
  LEFT,
  RIGHT
}

public struct bound{
  public int left;
  public int right;
  public int top;
  public int bottom;

  public static bound From(int w,int h){
    return new bound(){
      left=0,right=w,
      top=h,bottom=0
    };
  }

}

public struct VirtualCursol{
  public pos current;

  public bound bound;
}

/**
 * 実質コントロールクラスになっている
 *
 * */
public class ShogiController{
  public Shogi shogi;
  //バーチャルカーソル
  private pos cursor=pos.From(0,0);

  public delegate void renderEventHandler(pos offset);
  public delegate void ShogiRenderEventHandler(Object sender);
  public delegate void KomaRenderEventHandler(Koma k);
  public delegate void KomaSelectEventHandler(Koma k);
  public delegate void GrobalEventHandler();

  public event renderEventHandler renderEvent;
  public event GrobalEventHandler renderBoardEvent;
  public event KomaRenderEventHandler renderKomaEvent;
  public event KomaSelectEventHandler selectKomaEvent;
  //メニュー
  private SingleMenu<Koma> menu;
  public pos size = pos.From(9,9);
  public void Run(){
    Console.CancelKeyPress+=(object sender, ConsoleCancelEventArgs e)=>{
      Console.Clear();
      Console.ResetColor();
    };
    Console.Clear();
    Console.ResetColor();
    //描画開始
    renderBoardEvent();
    foreach(Koma k in shogi.Komas){
      renderKomaEvent(k);
    }
    //メニュー初期化
    menu=new SingleMenu<Koma>();
    foreach(Koma k in shogi.Komas.Where(k=>k.owner==TypeOwner.GYOKU)){
      menu.Add(Menu<Koma>.From(
        $"[{k.ToLogo()}] {k.position}",
        k,()=>
      {
        Console.Write("-OK-");
      }));
    }

    //キー入力開始
    this.keyLoop();
  }

  //本ツールはarrow + enter + esc以外は動作しない
  private void keyLoop(){
    while(true){
      var k = Console.ReadKey(true);

      var r=AllowControll(k);
      UpdateStatus();  //ステータス表示更新
    }
  }

  private bool AllowControll(ConsoleKeyInfo k){
    return k.Key switch{
      ConsoleKey.UpArrow=>SimpleAllowControl(TypeControlArrow.UP) ,
      ConsoleKey.DownArrow=>SimpleAllowControl(TypeControlArrow.DOWN) ,
      ConsoleKey.LeftArrow=>SimpleAllowControl(TypeControlArrow.LEFT) ,
      ConsoleKey.RightArrow=>SimpleAllowControl(TypeControlArrow.RIGHT) ,
      _ => false
    };

  }

  private bool SimpleAllowControl(TypeControlArrow a){
    //スクロールは一次元
    menu.Apply(a);
    return true;
  } 

  public void UpdateStatus(){

    //現在選択している駒を表示させる
    Console.SetCursorPosition(
        Console.BufferWidth-10,
        Console.BufferHeight-1
     );
    Console.Write($"{cursor.x,3}:{cursor.y,3}");


    //実質renderer
    Console.SetCursorPosition(2,Console.BufferHeight-1);
    Console.Write(menu.Current.label);
    //コマ描画
    selectKomaEvent(menu.Current.Item);
  }

  private ShogiController(Shogi s){
    this.shogi = s;
  }

  public static ShogiController FromShogi(Shogi s){
    var result= new ShogiController(s);
    return result;
  }
}

