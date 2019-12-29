using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum TypeField
{
    FIELD,
    BACK
}
public enum TypeOwner
{
    OU,
    GYOKU
}
public enum TypeKomaClass
{
    FU,
    HISYA,
    KAKU,
    KYOSHA,
    KEIMA,
    GIN,
    KIN,
    OU
}

public enum TypeNari
{
    NARI,
    NARAZU
}

public class Koma
{
    public TypeOwner owner;
    public TypeField field;
    public TypeNari nari;
    public TypeKomaClass klass;
    public pos position;

    //駒自体の動ける範囲
    public pos[] Movables
    {
        get
        {
          TypeOwner own=this.owner;
            return (
              this.klass switch
              {
                  TypeKomaClass.OU => this.MoveOu,
                  TypeKomaClass.KIN => this.MoveKin,
                  TypeKomaClass.FU =>
            this.nari switch
          {
              TypeNari.NARAZU => this.MoveFu,
              TypeNari.NARI => this.MoveKin,
              _ => null
          },
                  TypeKomaClass.GIN =>
            this.nari switch
          {
              TypeNari.NARAZU => this.MoveGin,
              TypeNari.NARI => this.MoveKin,
              _ => null
          },
                  TypeKomaClass.KEIMA =>
            this.nari switch
          {
              TypeNari.NARAZU => this.MoveKeima,
              TypeNari.NARI => this.MoveKin,
              _ => null
          },
                  TypeKomaClass.KYOSHA =>
            this.nari switch
          {
              TypeNari.NARAZU => this.MoveKyosha,
              TypeNari.NARI => this.MoveKin,
              _ => null
          },
                  TypeKomaClass.HISYA =>
            this.nari switch
          {
              TypeNari.NARAZU => this.MoveHisya,
              TypeNari.NARI => this.MoveKin,
              _ => null
          },
                  TypeKomaClass.KAKU =>
            this.nari switch
          {
              TypeNari.NARAZU => this.MoveKaku,
              TypeNari.NARI => this.MoveKin,
              _ => null
          },
                  _ => this.MoveFu
          }).Select(
            a=>a* own switch{
              TypeOwner.GYOKU=>1,
              TypeOwner.OU=>-1,
              _ =>0
            }
          ).ToArray()   
          ;
        }
    }

    public pos[] CalcMovable=>Movables.Select(k=>this.position+k).ToArray();
    //場合により反対

    private pos[] MoveKin => new pos[]{
    pos.From(-1,0),
    pos.From(1,0),

    pos.From(-1,1),
    pos.From(0,1),
    pos.From(1,1),

    pos.From(0,-1)
  };
    private pos[] MoveGin => new pos[]{
    pos.From(-1,1),
    pos.From(1,1),
    pos.From(0,1),

    pos.From(-1,-1),
    pos.From(1,-1),
  };

    private pos[] MoveFu => new pos[]{
    pos.From(0,1),
  };

    private pos[] MoveKyosha => new pos[]{
    pos.From(0,1)
  };
    private pos[] MoveKeima => new pos[]{
    pos.From(1,2),
    pos.From(-1,2),
  };
    private pos[] MoveHisya => new pos[]{
    pos.From(1,0),
    pos.From(-1,0),
    pos.From(0,1),
    pos.From(0,1),
  };
    private pos[] MoveKaku => new pos[]{
    pos.From(1,1),
    pos.From(-1,1),
    pos.From(1,-1),
    pos.From(-1,-1),
  };
    private pos[] MoveOu => new pos[]{
    pos.From(1,1),
    pos.From(-1,1),
    pos.From(1,-1),
    pos.From(-1,-1),
    pos.From(1,0),
    pos.From(-1,0),
    pos.From(0,1),
    pos.From(0,1),
  };

    public static Koma N(TypeOwner own, TypeKomaClass koma, int index = 0)
      => new Koma()
      {
          owner = own,
          klass = koma,
          field = TypeField.FIELD,
          nari = TypeNari.NARAZU,
          position = koma switch
          {
              TypeKomaClass.FU => pos.From(index, 2).OwnPos(own),
              TypeKomaClass.HISYA => pos.From(1, 1).OwnPos(own),
              TypeKomaClass.KAKU => pos.From(7, 1).OwnPos(own),
              TypeKomaClass.KYOSHA => pos.From(index == 0 ? 0 : 8, 0).OwnPos(own),
              TypeKomaClass.KEIMA => pos.From(index == 0 ? 1 : 7, 0).OwnPos(own),
              TypeKomaClass.GIN => pos.From(index == 0 ? 2 : 6, 0).OwnPos(own),
              TypeKomaClass.KIN => pos.From(index == 0 ? 3 : 5, 0).OwnPos(own),
              TypeKomaClass.OU => pos.From(4, 0).OwnPos(own),

              _ => pos.From(1, 1)
          }
      };
    public string ToLogo() => this.klass switch
    {
        TypeKomaClass.FU =>
        this.nari switch
        {
            TypeNari.NARAZU => "歩",
            TypeNari.NARI => "と",
            _ => "  ",
        },
        TypeKomaClass.HISYA =>
        this.nari switch
        {
            TypeNari.NARAZU => "飛",
            TypeNari.NARI => "竜",
            _ => "  ",
        },
        TypeKomaClass.KAKU =>
        this.nari switch
        {
            TypeNari.NARAZU => "角",
            TypeNari.NARI => "馬",
            _ => "  ",
        },
        TypeKomaClass.KYOSHA =>
        this.nari switch
        {
            TypeNari.NARAZU => "香",
            TypeNari.NARI => "杏",
            _ => "  ",
        },
        TypeKomaClass.KEIMA =>
        this.nari switch
        {
            TypeNari.NARAZU => "桂",
            TypeNari.NARI => "圭",
            _ => "  ",
        },
        TypeKomaClass.GIN =>
        this.nari switch
        {
            TypeNari.NARAZU => "銀",
            TypeNari.NARI => "全",
            _ => "  ",
        },
        TypeKomaClass.KIN => "金",
        TypeKomaClass.OU => this.owner switch
        {
            TypeOwner.OU => "王",
            TypeOwner.GYOKU => "玉",
            _ => "  "
        },
        _ => "？"
    };

    public override string ToString() => $@"
  owner {owner}
  field {field}
  nari {nari}
  klass {klass}
  position {position}
    ";

    public static Koma[] DefaultSet => new Koma[]{
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

