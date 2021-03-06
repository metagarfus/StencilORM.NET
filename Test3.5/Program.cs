﻿using System;
using System.Collections.Generic;
using StencilORM.Parsers;
using StencilORM.Queries;
using Test;

namespace Test3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var expr = ExprParser.Instance.Parse("a + 'ola mundo \n \t \\'mundo\\' ola' * (6 + -9)");
            var expr2 = ExprParser.Instance.Parse("-a");
            var expr3 = ExprParser.Instance.Parse("-a + -(4 + 6)");
            var expr4 = ExprParser.Instance.Parse("6");
            var expr5 = ExprParser.Instance.Parse("6.0");
            var expr6 = ExprParser.Instance.Parse("a + $a");
            var expr7 = ExprParser.Instance.Parse("Articles.ArticleDescription == 'RTUY /(89)' ");
            var expr8 = ExprParser.Instance.Parse(" FF1(a+b, 89, (9+1) * 2)");
            var expr9 = ExprParser.Instance.Parse(" FF2(a+b )");
            var expr10 = ExprParser.Instance.Parse("  a+ b >= 3 && 1");
            var expr11 = ExprParser.Instance.Parse("  a+ b >= 3 && 1 ? (3 + 9) * 5:  a <= 3");
            var expr12 = ExprParser.Instance.Parse("-(  a+ b >= 3 && 1 ? (3 + 9) * 5:  a <= 3)");
            var expr13 = Expr.Parse("true");
            var expr14 = Expr.Parse("false");
            var expr15 = Expr.Parse("!true");
            Guid guid = Guid.NewGuid();
            var literal = (Literal)guid;
            var update = new Update<ExampleTable>(new ExampleTable { Key = guid });
            update.Execute(new Compiler(), out int n1);
            var update2 = new Update<ExampleTable>(new ExampleTable { Key = guid }, "Description");
            var select = new Query<ExampleTable>().InnerJoin(new Query("SomeTable"),
                                                             new string[] { "ForeignKey" },
                                                             new string[] { "SomeKey" });
            select.Execute(new Compiler());
            var select2 = new Query<ExampleTable>().LeftJoin(new Query("SomeTable"),
                                                             new string[] { "ForeignKey" },
                                                             new string[] { "SomeKey" });
            select2.Execute(new Compiler());

            var select3 = new Query<ExampleTable>().LeftJoin("SomeTable", "SomeTableAlias",
                                                            new string[] { "ForeignKey" },
                                                            new string[] { "SomeKey" });
            select3.Execute(new Compiler());
            var select4 = new Query<ExampleTable>()
            .Select("a", "b", "c")
            .Where(
                Expr.Eq("a", "ola")
                .And(Expr.Eq("b", 3))
                );
            select4.Execute(new Compiler());
            var insertOrUpdate = new Update<ExampleTable>(new ExampleTable { Key = guid }).InsertOrUpdate();
            insertOrUpdate.Execute(new Compiler(), out n1);

            var select5 = new Query<ExampleTable>().Where(Expr.NotNull("ForeignKey"));
            select5.Execute(new Compiler());

            var select6 = new Query<ExampleTable>().Where(Expr.NotIn("ForeignKey", new int[] { 8, 9, 10 }));
            select6.Execute(new Compiler());

            var select7 = new Query<ExampleTable>().Where(Expr.NotIn("ForeignKey", new List<string> { "8", "9", "10" }));
            select7.Execute(new Compiler());

            var select8 = new Query<ExampleTable>().Select(Expr.Count());
            select8.Execute(new Compiler());

            var select9 = new Query<ExampleTable>().Select(new Function("TTS", (Literal)1, (Literal)4));
            select9.Execute(new Compiler());

            var select10 = new Query<ExampleTable>().Select(Expr.Parse("MAX(ForeignKey)"));
            select10.Execute(new Compiler());

            var select11 = new Query<ExampleTable>().Select(Expr.Parse("MAX(ForeignKey)")).Select(" A ")
                                                    .OrderBy(true, "A", "C")
                                                    .OrderBy(false, "B")
                                                    .OrderBy(false, "D");

            select11.Execute(new Compiler());
            var select12 = new Query<ExampleTable>()
            .Select("a", "b", "c")
            .Where(
                Expr.EqVar("a", "ola")
                .And(Expr.EqVar("b", "3"))
                );
            select4.Execute(new Compiler());
            select12.Execute(new Compiler());
            Console.WriteLine(new Compiler().CompileToString(select12));
            
            var select13 = new Query<ExampleTable>().InnerJoin<ExampleTable>(Expr.EqParam("olaColumn", "ola"));
            select13.Execute(new Compiler());
            var select14 = new Query().Select(Expr.Exists(select13));
            select14.Execute(new Compiler());
            var select15 = new Query().Select(Expr.Parse("a.b == c.d && g.h == $a"));
            select15.Execute(new Compiler());
            var select16 = new Query().Select(Expr.Cast((Variable)"a.b", "decimal"));
            select16.Execute(new Compiler());
            var select17 = new Query().Select(Expr.Parse("Cast(a.b, 'decimal')"));
            select17.Execute(new Compiler());
            var select18 = new Query().Select(Expr.Parse("Cast(a.b, decimal)"));
            select18.Execute(new Compiler());
            Console.WriteLine("Hello World!");
        }
    }
}
