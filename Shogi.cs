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

  public void Update(){
    this.board=board.Values.ToDictionary(k=>k.position,v=>v);    
  }

  public Koma this[pos p]=>board.ContainsKey(p)?board[p]:null;

  public pos[] GetMovablePositions(Koma k){
    return 
    k.CalcMovable
    .Where(p=>(0<=p.x && p.x < 9)&&(0<=p.y && p.y < 9))//エリア範囲
    .Where(p=>!board.ContainsKey(p)||board[p].owner!=k.owner)
    .ToArray();
  }


  //ShogiControllerインスタンスが生成される
  public static ShogiController Create(){

    return ShogiController.FromShogi(new Shogi());
  }
}

