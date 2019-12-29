using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


/**
 * 駒単体の動かせる範囲を描画する
 * */

 public delegate void OnEnterSelectMasu(Koma koma,pos p);
public class KomaMoveMenuController : SingleMenu<pos>
{
    public Koma koma;
    private Shogi shogi;

    public KomaMoveMenuController(
        Shogi shogi,
        Koma k,OnEnterSelectMasu selected)
    {
        this.shogi=shogi;
        this.koma=k;


        //自分の位置を取得
        foreach(pos pos in shogi.GetMovablePositions(k)) {            
            this.Add(Menu<pos>.From("_",pos,()=>selected(k,pos)));
        }
    }

    public override void OnArrow(TypeControlArrow a){
        //this.komas.lastSelectKoma=this.Current.obj;

        

        base.OnArrow(a);
        Debug.Log(Current.obj);
    }
}

