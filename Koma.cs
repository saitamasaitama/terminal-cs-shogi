using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum TypeField{
  FIELD,
  BACK
}
public enum TypeOwner{
  OU,
  GYOKU
}
public enum TypeKomaClass{
  FU,
  HISYA,
  KAKU,
  KYOSHA,
  KEIMA,
  GIN,
  KIN,
  OU
}

public enum TypeNari{
  NARI,
  NARAZU
}

public struct Koma{
  public TypeOwner owner;
  public TypeField field;
  public TypeNari nari;
  public TypeKomaClass klass;
  public pos position;


  public static Koma N(TypeOwner own,TypeKomaClass koma,int index=0)
    =>new Koma(){
      owner=own,
        klass=koma,
        field=TypeField.FIELD,
        nari=TypeNari.NARAZU,
        position = koma switch {
          TypeKomaClass.FU => pos.From(index,2).OwnPos(own),
          TypeKomaClass.HISYA => pos.From(1,1).OwnPos(own),
          TypeKomaClass.KAKU => pos.From(7,1).OwnPos(own),
          TypeKomaClass.KYOSHA =>  pos.From(index==0?0:8,0).OwnPos(own),
          TypeKomaClass.KEIMA =>  pos.From(index==0?1:7,0).OwnPos(own),
          TypeKomaClass.GIN =>  pos.From(index==0?2:6,0).OwnPos(own),
          TypeKomaClass.KIN =>  pos.From(index==0?3:5,0).OwnPos(own),
          TypeKomaClass.OU =>  pos.From(4,0).OwnPos(own),
          
          _ => pos.From(1,1)
        }
    };
  public string ToLogo()=>this.klass switch{
    TypeKomaClass.FU => 
    this.nari switch{
      TypeNari.NARAZU=>"歩",
      TypeNari.NARI=>"と"
    },
    TypeKomaClass.HISYA => 
    this.nari switch{
      TypeNari.NARAZU=>"飛",
      TypeNari.NARI=>"竜"
    },
    TypeKomaClass.KAKU => 
    this.nari switch{
      TypeNari.NARAZU=>"角",
      TypeNari.NARI=>"馬"
    },
    TypeKomaClass.KYOSHA =>
    this.nari switch{
      TypeNari.NARAZU=>"香",
      TypeNari.NARI=>"杏"
    },
    TypeKomaClass.KEIMA =>
    this.nari switch{
      TypeNari.NARAZU=>"桂",
      TypeNari.NARI=>"圭"
    },
    TypeKomaClass.GIN =>
    this.nari switch{
      TypeNari.NARAZU=>"銀",
      TypeNari.NARI=>"全"
    },
    TypeKomaClass.KIN => "金",
    TypeKomaClass.OU => this.owner switch{
      TypeOwner.OU=>"王",
      TypeOwner.GYOKU=>"玉",
    },
    _ => "？"
  };

  public override string ToString()=>$@"
  owner {owner}
  field {field}
  nari {nari}
  klass {klass}
  position {position}
    ";
  
  public static Koma[] DefaultSet=>new Koma[]{
    //自分
    Koma.N(TypeOwner.OU,TypeKomaClass.FU,0),
      Koma.N(TypeOwner.OU,TypeKomaClass.FU,1),
      Koma.N(TypeOwner.OU,TypeKomaClass.FU,2),
      Koma.N(TypeOwner.OU,TypeKomaClass.FU,3),
      Koma.N(TypeOwner.OU,TypeKomaClass.FU,4),
      Koma.N(TypeOwner.OU,TypeKomaClass.FU,5),
      Koma.N(TypeOwner.OU,TypeKomaClass.FU,6),
      Koma.N(TypeOwner.OU,TypeKomaClass.FU,7),
      Koma.N(TypeOwner.OU,TypeKomaClass.FU,8),

      Koma.N(TypeOwner.OU,TypeKomaClass.HISYA,0),
      Koma.N(TypeOwner.OU,TypeKomaClass.KAKU,0),

      Koma.N(TypeOwner.OU,TypeKomaClass.KYOSHA,0),
      Koma.N(TypeOwner.OU,TypeKomaClass.KEIMA,0),
      Koma.N(TypeOwner.OU,TypeKomaClass.GIN,0),
      Koma.N(TypeOwner.OU,TypeKomaClass.KIN,0),
      Koma.N(TypeOwner.OU,TypeKomaClass.OU,0),
      Koma.N(TypeOwner.OU,TypeKomaClass.KIN,1),
      Koma.N(TypeOwner.OU,TypeKomaClass.GIN,1),
      Koma.N(TypeOwner.OU,TypeKomaClass.KEIMA,1),
      Koma.N(TypeOwner.OU,TypeKomaClass.KYOSHA,1),


      //相手
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.FU,0),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.FU,1),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.FU,2),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.FU,3),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.FU,4),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.FU,5),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.FU,6),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.FU,7),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.FU,8),

      Koma.N(TypeOwner.GYOKU,TypeKomaClass.HISYA,0),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.KAKU,0),

      Koma.N(TypeOwner.GYOKU,TypeKomaClass.KYOSHA,0),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.KEIMA,0),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.GIN,0),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.KIN,0),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.OU,0),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.KIN,1),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.GIN,1),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.KEIMA,1),
      Koma.N(TypeOwner.GYOKU,TypeKomaClass.KYOSHA,1)

  };
}

