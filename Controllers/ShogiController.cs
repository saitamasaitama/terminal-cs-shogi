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

public enum TypeSelectMode{
  KOMASELECT,
  MOVESELECT,
  NARISELECT
}

/**
 * 実質コントロールクラスになっている
 *
 * */
public class ShogiController{
  public Shogi shogi;

  public View view=new View();
//ビュー一覧　ここから
  private BoardView board=new BoardView();
  private KomasView komas;
  
  private MessageView message=new MessageView();
  //ビュー一覧　ここまで

  //コントローラ類　ここから
  private MenuListener menues=new MenuListener();

  //コントローラ類　ここまで

  public pos size = pos.From(9,9);

  
  private ShogiController(Shogi s){
    this.shogi = s;
  }

  public void Run(){
    Console.CancelKeyPress+=(object sender, ConsoleCancelEventArgs e)=>{
      Console.Clear();
      Console.ResetColor();
    };
    Console.Clear();
    Console.ResetColor();
    //ビューの初期化
    this.view.Add(board);
    komas=new KomasView(shogi.Komas.ToList());
    this.view.Add(komas);
    message+="頑張れ！";
    this.view+=message;
    this.view.Render(Transform.From(0,2,0));
    
    //ビューの初期化　ここまで


    //メニュー初期化
    KomaSelectMenuController  menu=new KomaSelectMenuController(komas);
    foreach(Koma k in shogi.Komas.Where(
          k=>k.owner==TypeOwner.OU
          )){
        menu.Add(Menu<Koma>.From(
        $"[{k.ToLogo()}] {k.position}",
        k,()=>
      {
        
        ModeChangeMoveSelect(k);
        //モードを変更する
      }));
    }
    menues+=menu;
    //キー入力開始
    this.keyLoop();
  }

  private void ModeChangeMoveSelect(Koma k){
    komas.ClearMovable();
    this.view+=new MoveSelectView(shogi,k);

    this.menues+=new KomaMoveMenuController(shogi,k,(koma,pos)=>{
      //駒をその位置に移動させる
      Debug.Log($"駒移動完了{pos}");
      //komas.UpdatePosition(koma,pos);
      koma.position=pos;
      //成れるかどうか判定
      //メニュー一覧をPOP
      this.menues.Pop();
      this.view.Pop();
      ModeChangeKomaSelect();
    });

    //komas.isMoveRender = false;
    message+="駒の選択先を選択してください";
  }

  private void ModeChangeKomaSelect(){
    this.komas.isMoveRender=true;
    message+="駒選択モードに戻る";
  }


  //本ツールはarrow + enter + esc以外は動作しない
  private void keyLoop(){
    while(true){
      var k = Console.ReadKey(true);
      KeyControll(k);
      UpdateStatus();  //ステータス表示更新
    }
  }
  private void KeyControll(ConsoleKeyInfo k){
    switch(k.Key){
      //case ConsoleKey.Enter:OnEnter();break;
      case ConsoleKey.Enter:menues.Enter();break;
      case ConsoleKey.Escape:menues.Escape();break;
      case ConsoleKey.UpArrow:menues.OnArrow(TypeControlArrow.UP);break;
      case ConsoleKey.DownArrow:menues.OnArrow(TypeControlArrow.DOWN);break;
      case ConsoleKey.LeftArrow:menues.OnArrow(TypeControlArrow.LEFT);break;
      case ConsoleKey.RightArrow:menues.OnArrow(TypeControlArrow.RIGHT);break;
      default:return;
    };
  }

  public void UpdateStatus(){
    this.view.Render(Transform.From(0,2,0));
    Console.SetCursorPosition(Console.BufferWidth-1,Console.BufferHeight-1);
  }


  public static ShogiController FromShogi(Shogi s){
    var result= new ShogiController(s);
    return result;
  }
}

