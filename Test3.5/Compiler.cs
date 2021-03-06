﻿using System;
using System.Collections.Generic;
using System.Text;
using StencilORM.Compilers;
using StencilORM.Metadata;
using StencilORM.Queries;

namespace Test3
{
    public class Compiler : SimpleSQLCompiler<object>
    {
        public Compiler()
        {
        }

        public override bool CreateOrAlter<R>()
        {
            return false;
        }

        public override void EscapeName(StringBuilder builder, string name, bool isTableName)
        {
            builder.AppendFormat("[{0}]", name);
        }

        public override IEnumerable<R> Execute<R>(CompiledQuery<object> query, params Value[] parameters)
        {
            Console.WriteLine(query.Query);
            return new R[1] { default(R)};
        }

        public override object Execute(Type type, CompiledQuery<object> query, params Value[] parameters)
        {
             Console.WriteLine(query.Query);
            return null;
        }

        public override IEnumerable<string[]> Execute(CompiledQuery<object> query, params Value[] parameters)
        {
            Console.WriteLine(query.Query);
            return null;
        }

        public override bool Execute(CompiledQuery<object> query, out int rowsAltered, params Value[] parameters)
        {
            Console.WriteLine(query.Query);
            rowsAltered = 0;
            return true;
        }

        public override IPreparedStatement Prepare(CompiledQuery<object> query)
        {
            return null;
        }

        public override void Process(object state, StringBuilder builder, Param param)
        {
            builder.Append("?");
        }

        protected override object NewState()
        {
            return null;
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, decimal value)
        {
            builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, DateTimeOffset value)
        {
           builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, DateTime value)
        {
            builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, double value)
        {
           builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, float value)
        {
            builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, int value)
        {
            builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, uint value)
        {
             builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, long value)
        {
            builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, ulong value)
        {
            builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, short value)
        {
             builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, ushort value)
        {
             builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, sbyte value)
        {
             builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, byte value)
        {
             builder.AppendFormat("{0}", value);
        }

        protected override void Process(object state, StringBuilder builder, DataType dataType, bool value)
        {
             builder.AppendFormat("{0}", value);
        }

        protected override void ProcessAfterQueryEnd(object state, StringBuilder builder, Query query)
        {

        }

        protected override void ProcessBeforeSelectedColumns(object state, StringBuilder builder, Query query)
        {

        }

        protected override void ProcessCast(object state, StringBuilder builder, IExpr expr, string typename)
        {
            builder.Append("CAST(");
            expr?.Visit(state, builder, this);
            builder.Append(" AS ");
            builder.Append(typename ?? "");
            builder.Append(")");
            
        }

        protected override void ProcessConcat(object state, StringBuilder builder, IExpr left, IExpr right)
        {

        }

        protected override void ProcessOtherLiteral(object state, StringBuilder builder, DataType dataType, object value)
        {
           builder.AppendFormat("'{0}'", value?.ToString()?.Replace("'", "\\'"));
        }
    }
}
