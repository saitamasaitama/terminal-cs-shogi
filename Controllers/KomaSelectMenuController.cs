using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


/**
 * 実質コントロールクラスになっている
 *
 * */
public class KomaSelectMenuController : SingleMenu<Koma>
{
    private OnUpdateSelectKoma selectEvent;
    public KomasView komas;
    
    

    public KomaSelectMenuController(KomasView komas)
    {
        this.komas=komas;
        //駒

    }

    public override void OnArrow(TypeControlArrow a){
        
        base.OnArrow(a);
        this.komas.lastSelectKoma=this.Current.obj;        

    }

}

